using Microsoft.Data.Sqlite;
using System.Data;

namespace Scooter_Kiralama_Sistemi.Helpers
{
    public class DatabaseHelper
    {
        // Veritabanı dosyasının yolu
        private static readonly string DbPath = Path.Combine(
           AppDomain.CurrentDomain.BaseDirectory, "scooter.db");

        // SQLCipher için şifre (Bunu ileride daha güvenli bir yerde saklayabilirsin)
        // Sql düz text olmak yerine şifreleniyor. Şifrelenme sonra da yapılabilir
        // BURASI SİFRELEME İCİN VAR, SİFRELEME YAPMASAK DAHA İYİ KARMASIK OLMAZ
        // private static readonly string ConnectionString = $"Data Source={DbPath};Password={DbPassword};";
        // private static readonly string DbPassword = "@StrongDatabasePassword!123#";

        private static readonly string ConnectionString = $"Data Source={DbPath}";

        // Merkezi bağlantı oluşturucu
        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection(ConnectionString);
        }

        // initalize tablo
        // farklı tablolar oluşturulacaktır, şuan için Users tablosu tek oluşturuluyor
        public static void InitializeDatabase()
        {
            // Şifreli veritabanı motorunu başlatır
            SQLitePCL.Batteries_V2.Init();

            using SqliteConnection con = GetConnection();
            con.Open();
            using SqliteCommand cmd = con.CreateCommand();

            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Users (
                    id            INTEGER PRIMARY KEY AUTOINCREMENT,
                    name          TEXT    NOT NULL,
                    surname       TEXT    NOT NULL,
                    email         TEXT    NOT NULL UNIQUE,
                    password_hash TEXT    NOT NULL,
                    balance       REAL    NOT NULL DEFAULT 0,
                    role          INTEGER NOT NULL DEFAULT 0, -- 0=Admin, 1=User
                    created_at    TEXT    NOT NULL DEFAULT (datetime('now'))
                );

                CREATE TABLE IF NOT EXISTS Locations (
                    id         INTEGER PRIMARY KEY AUTOINCREMENT,
                    name       TEXT    NOT NULL,
                    lat        REAL    NOT NULL,
                    lng        REAL    NOT NULL
                );
                
                CREATE TABLE IF NOT EXISTS Scooters (
                    id         INTEGER PRIMARY KEY AUTOINCREMENT,
                    name       TEXT    NOT NULL,
                    status     TEXT    NOT NULL DEFAULT 'available',
                    battery    INTEGER NOT NULL DEFAULT 100,
                    qr_code    TEXT    NOT NULL UNIQUE,
                    lat        REAL    NOT NULL,
                    lng        REAL    NOT NULL,
                    created_at TEXT    NOT NULL DEFAULT (datetime('now'))
                );


                CREATE TABLE IF NOT EXISTS Rentals (
                    id          INTEGER PRIMARY KEY AUTOINCREMENT,
                    user_id     INTEGER NOT NULL,
                    scooter_id  INTEGER NOT NULL,
                    start_date  TEXT    NOT NULL DEFAULT (datetime('now')),
                    end_date    TEXT,
                    days        INTEGER NOT NULL,
                    total_price REAL    NOT NULL,
                    status      TEXT    NOT NULL DEFAULT 'active',
                    created_at  TEXT    NOT NULL DEFAULT (datetime('now')),
                    FOREIGN KEY (user_id)    REFERENCES Users(id),
                    FOREIGN KEY (scooter_id) REFERENCES Scooters(id)
                );


                CREATE TABLE IF NOT EXISTS Payments (
                    id         INTEGER PRIMARY KEY AUTOINCREMENT,
                    user_id    INTEGER NOT NULL,
                    rental_id  INTEGER NOT NULL,
                    amount     REAL    NOT NULL,
                    card_last4 TEXT    NOT NULL,
                    created_at TEXT    NOT NULL DEFAULT (datetime('now')),
                    FOREIGN KEY (user_id)   REFERENCES Users(id),
                    FOREIGN KEY (rental_id) REFERENCES Rentals(id)
                );
            ";
            cmd.ExecuteNonQuery();
            SeedDefaultAdmins(); // default adminleri database de user tablosuna ekliyoruz.
        }

        // kullanici ekler
        // AddUser("semih","özdemir","semihozdmirr@gmail.com","mypassword", UserRole.User)
        public static bool AddUser(string name, string surname, string email, string password, UserRole role = UserRole.User)
        {
            try
            {
                // Aynı e-posta kontrolü
                if (UserExists(email))
                {
                    return false;
                }

                // Şifreyi Hash'leme
                string hash = BCrypt.Net.BCrypt.HashPassword(password);

                using var con = GetConnection();
                con.Open();

                using var cmd = con.CreateCommand();
                cmd.CommandText = @"
                    INSERT INTO Users (name, surname, email, password_hash,role)
                    VALUES ($name, $surname, $email, $password_hash,$role)
                ";
                cmd.Parameters.AddWithValue("$name", name);
                cmd.Parameters.AddWithValue("$surname", surname);
                cmd.Parameters.AddWithValue("$email", email);
                cmd.Parameters.AddWithValue("$password_hash", hash);
                cmd.Parameters.AddWithValue("$role", (int)role);


                cmd.ExecuteNonQuery();
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        // eposta ile user exists mi bakar
        private static bool UserExists(string email)
        {
            using var con = GetConnection();
            con.Open();
            using var cmd = con.CreateCommand();

            cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE email = $email";
            cmd.Parameters.AddWithValue("$email", email);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }

        public static bool DeleteUser(int userId)
        {
            try
            {
                using var con = GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();

                cmd.CommandText = "DELETE FROM Users WHERE id = $id";
                cmd.Parameters.AddWithValue("$id", userId);

                // Etkilenen satır sayısı 0'dan büyükse başarılıdır
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                // Eğer kiralama kaydı varsa Foreign Key hatası dönecektir
                Console.WriteLine("Kullanıcı silme hatası: " + ex.Message);
                return false;
            }
        }




        // userlogin kontrolü
        public static Users? UserLogin(string email, string password)
        {
            try
            {
                using var con = GetConnection();
                con.Open();

                using var cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Users WHERE email = $email";
                cmd.Parameters.AddWithValue("$email", email);

                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Sütun sıralaması değişse bile bozulmaması için GetOrdinal kullanıyoruz
                    string hash = reader.GetString(reader.GetOrdinal("password_hash"));

                    // Şifre eşleşiyorsa kullanıcı nesnesini döndür
                    if (BCrypt.Net.BCrypt.Verify(password, hash))
                    {
                        return new Users
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            name = reader.GetString(reader.GetOrdinal("name")),
                            surname = reader.GetString(reader.GetOrdinal("surname")),
                            email = reader.GetString(reader.GetOrdinal("email")),
                            balance = reader.GetDouble(reader.GetOrdinal("balance")),
                            role = (UserRole)reader.GetInt32(reader.GetOrdinal("role"))

                        };
                    }
                }
                return null; // Şifre yanlışsa veya kullanıcı yoksa
            }
            catch
            {
                return null; // Veritabanı hatası durumunda çökmesini engeller
            }
        }


        private static void SeedDefaultAdmins()
        {
            // 1. Admin
            if (!UserExists("032390072@ogr.uludag.edu.tr"))
            {
                // Dikkat: Parametre olarak UserRole.Admin gönderiyoruz
                AddUser("Semih", "Özdemir", "032390072@ogr.uludag.edu.tr", "password", UserRole.Admin);
            }

            // 2. Admin
            if (!UserExists("032390035@ogr.uludag.edu.tr"))
            {
                AddUser("Muhsin", "Yılmaz", "032390035@ogr.uludag.edu.tr", "password", UserRole.Admin);
            }

            // 3. Admin
            if (!UserExists("032390031@ogr.uludag.edu.tr"))
            {
                AddUser("Can", "Karakoç", "032390031@ogr.uludag.edu.tr", "password", UserRole.Admin);
            }
        }

        public static DataTable GetScooters()
        {
            using var con = GetConnection();
            con.Open();

            string query = "SELECT id, name, status, battery, qr_code, lat, lng FROM Scooters";

            using var cmd = new SqliteCommand(query, con);
            using var reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(reader);

            return dt;
        }

        public static DataTable GetAllUsers()
        {
            using var con = GetConnection();
            con.Open();
            using var cmd = con.CreateCommand();

            // Şifre hash'ini de çekiyoruz ama tabloda gizlemek daha profesyonel durur
            cmd.CommandText = "SELECT id, name, surname, email, balance, role, created_at, password_hash FROM Users";

            using var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            return dt;
        }

        public static bool UpdateUserBalance(int userId, double amount)
        {
            using var con = GetConnection();
            con.Open();
            using var cmd = con.CreateCommand();

            // Mevcut bakiyeye ekleme yapıyoruz
            cmd.CommandText = "UPDATE Users SET balance = balance + $amount WHERE id = $id";
            cmd.Parameters.AddWithValue("$amount", amount);
            cmd.Parameters.AddWithValue("$id", userId);

            return cmd.ExecuteNonQuery() > 0;
        }


        public static bool DeleteScooter(int id)
        {
            try
            {
                using var con = GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();

                cmd.CommandText = "DELETE FROM Scooters WHERE id = $id";
                cmd.Parameters.AddWithValue("$id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                // Eğer kiralama geçmişi varsa silmeye izin vermeyebilir (Foreign Key Constraint)
                return false;
            }
        }





        public static bool AddScooter(string name, double lat, double lng, int battery, string qrCode)
        {
            try
            {
                using var con = GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();

                cmd.CommandText = @"
            INSERT INTO Scooters (name, lat, lng, battery, qr_code, status) 
            VALUES ($name, $lat, $lng, $battery, $qr, 'available')";

                cmd.Parameters.AddWithValue("$name", name);
                cmd.Parameters.AddWithValue("$lat", lat);
                cmd.Parameters.AddWithValue("$lng", lng);
                cmd.Parameters.AddWithValue("$battery", battery);
                cmd.Parameters.AddWithValue("$qr", qrCode);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
        }


    }
}



// Users loggedInUser = DatabaseHelper.UserLogin("test@test.com", "12345");
// loggedInUser.role == UserRole.Admin
// loggedInUser.role == UserRole.User