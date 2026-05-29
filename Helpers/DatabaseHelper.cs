using Microsoft.Data.Sqlite;
using System.Data;

namespace Scooter_Kiralama_Sistemi.Helpers
{
    public class DatabaseHelper
    {
        #region Yapılandırma ve Bağlantı Ayarları

        private static readonly string DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scooter.db");
        private static readonly string ConnectionString = $"Data Source={DbPath}";

        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection(ConnectionString);
        }

        public static void InitializeDatabase()
        {
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
                    role          INTEGER NOT NULL DEFAULT 1, -- 0=Admin, 1=User
                    created_at    TEXT    NOT NULL DEFAULT (datetime('now', 'localtime'))
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
                    created_at TEXT    NOT NULL DEFAULT (datetime('now', 'localtime'))
                );

                CREATE TABLE IF NOT EXISTS Rentals (
                    id          INTEGER PRIMARY KEY AUTOINCREMENT,
                    user_id     INTEGER NOT NULL,
                    scooter_id  INTEGER NOT NULL,
                    start_date  TEXT    NOT NULL DEFAULT (datetime('now', 'localtime')),
                    end_date    TEXT,
                    days        INTEGER NOT NULL,
                    total_price REAL    NOT NULL,
                    status      TEXT    NOT NULL DEFAULT 'active',
                    created_at  TEXT    NOT NULL DEFAULT (datetime('now', 'localtime')),
                    FOREIGN KEY (user_id)     REFERENCES Users(id),
                    FOREIGN KEY (scooter_id)  REFERENCES Scooters(id)
                );

                CREATE TABLE IF NOT EXISTS Payments (
                    id         INTEGER PRIMARY KEY AUTOINCREMENT,
                    user_id    INTEGER NOT NULL,
                    rental_id  INTEGER NOT NULL,
                    amount     REAL    NOT NULL,
                    card_last4 TEXT    NOT NULL,
                    created_at TEXT    NOT NULL DEFAULT (datetime('now', 'localtime')),
                    FOREIGN KEY (user_id)   REFERENCES Users(id),
                    FOREIGN KEY (rental_id) REFERENCES Rentals(id)
                );
            ";
            cmd.ExecuteNonQuery();
            SeedDefaultAdmins(); 
        }

        #endregion

        #region Kullanıcı İşlemleri (Authentication & Management)

        public static bool AddUser(string name, string surname, string email, string password, UserRole role = UserRole.User)
        {
            try
            {
                if (UserExists(email)) return false;

                string hash = BCrypt.Net.BCrypt.HashPassword(password);

                using var con = GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();
                cmd.CommandText = @"
                    INSERT INTO Users (name, surname, email, password_hash, role)
                    VALUES (@name, @surname, @email, @password_hash, @role)";
                
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@surname", surname);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password_hash", hash);
                cmd.Parameters.AddWithValue("@role", (int)role);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false;
            }
        }

        private static bool UserExists(string email)
        {
            using var con = GetConnection();
            con.Open();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE email = @email";
            cmd.Parameters.AddWithValue("@email", email);

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
                cmd.CommandText = "DELETE FROM Users WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", userId);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Kullanıcı silme hatası: " + ex.Message);
                return false;
            }
        }

        public static Users? UserLogin(string email, string password)
        {
            try
            {
                using var con = GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Users WHERE email = @email";
                cmd.Parameters.AddWithValue("@email", email);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string hash = reader.GetString(reader.GetOrdinal("password_hash"));

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
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static Users? GetUserByEmailAndPassword(string email, string password) => UserLogin(email, password);

        public static Users? GetUserById(int userId)
        {
            using var con = GetConnection();
            con.Open();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Users WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", userId);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

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

        public static DataTable GetAllUsers()
        {
            using var con = GetConnection();
            con.Open();
            using var cmd = con.CreateCommand();
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
            cmd.CommandText = "UPDATE Users SET balance = balance + @amount WHERE id = @id";
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@id", userId);

            return cmd.ExecuteNonQuery() > 0;
        }

        private static void SeedDefaultAdmins()
        {
            if (!UserExists("032390072@ogr.uludag.edu.tr"))
                AddUser("Semih", "Özdemir", "032390072@ogr.uludag.edu.tr", "password", UserRole.Admin);

            if (!UserExists("032390035@ogr.uludag.edu.tr"))
                AddUser("Muhsin", "Yılmaz", "032390035@ogr.uludag.edu.tr", "password", UserRole.Admin);

            if (!UserExists("032390031@ogr.uludag.edu.tr"))
                AddUser("Can", "Karakoç", "032390031@ogr.uludag.edu.tr", "password", UserRole.Admin);
        }

        #endregion

        #region Scooter İşlemleri (Scooter Management)

        public static bool AddScooter(string name, double lat, double lng, int battery, string qrCode)
        {
            try
            {
                using var con = GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();
                cmd.CommandText = @"
                    INSERT INTO Scooters (name, lat, lng, battery, qr_code, status) 
                    VALUES (@name, @lat, @lng, @battery, @qr, 'available')";

                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@lat", lat);
                cmd.Parameters.AddWithValue("@lng", lng);
                cmd.Parameters.AddWithValue("@battery", battery);
                cmd.Parameters.AddWithValue("@qr", qrCode);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
        }

        public static bool DeleteScooter(int id)
        {
            try
            {
                using var con = GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM Scooters WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false;
            }
        }

        public static DataRow GetScooterById(int id)
        {
            using var con = GetConnection();
            con.Open();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Scooters WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public static ScooterInfo? GetScooterByQr(string qrCode)
        {
            using var con = GetConnection();
            con.Open();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT id, name, status, battery, lat, lng FROM Scooters WHERE qr_code = @qr";
            cmd.Parameters.AddWithValue("@qr", qrCode);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            return new ScooterInfo
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Name = reader.GetString(reader.GetOrdinal("name")),
                Status = reader.GetString(reader.GetOrdinal("status")),
                Battery = reader.GetInt32(reader.GetOrdinal("battery")),
                Lat = reader.GetDouble(reader.GetOrdinal("lat")),
                Lng = reader.GetDouble(reader.GetOrdinal("lng"))
            };
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

        public static bool UpdateScooterStatus(int scooterId, string status)
        {
            using var con = GetConnection();
            con.Open();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE Scooters SET status = @status WHERE id = @id";
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@id", scooterId);
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool UpdateScooterLocation(int scooterId, double lat, double lng)
        {
            using var con = GetConnection();
            con.Open();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE Scooters SET lat = @lat, lng = @lng WHERE id = @id";
            cmd.Parameters.AddWithValue("@lat", lat);
            cmd.Parameters.AddWithValue("@lng", lng);
            cmd.Parameters.AddWithValue("@id", scooterId);
            return cmd.ExecuteNonQuery() > 0;
        }

        #endregion

        #region Kiralama İşlemleri (Rental Logic & Senkronizasyon)

        public static bool RentScooter(int userId, int scooterId, int days, double totalPrice)
        {
            using var con = GetConnection();
            con.Open();
            using var transaction = con.BeginTransaction();
            try
            {
                using var cmd = con.CreateCommand();
                cmd.CommandText = @"
                    INSERT INTO Rentals (user_id, scooter_id, days, total_price, status) 
                    VALUES (@uId, @sId, @days, @price, 'pending')";

                cmd.Parameters.AddWithValue("@uId", userId);
                cmd.Parameters.AddWithValue("@sId", scooterId);
                cmd.Parameters.AddWithValue("@days", days);
                cmd.Parameters.AddWithValue("@price", totalPrice);
                cmd.ExecuteNonQuery();

                using var updateCmd = con.CreateCommand();
                updateCmd.CommandText = "UPDATE Scooters SET status = 'reserved' WHERE id = @sId";
                updateCmd.Parameters.AddWithValue("@sId", scooterId);
                updateCmd.ExecuteNonQuery();

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine("Kiralama Hatası: " + ex.Message);
                return false;
            }
        }

        public static bool StartRental(int rentalId, DateTime startedAt)
        {
            using var con = GetConnection();
            con.Open();

            using var getCmd = con.CreateCommand();
            getCmd.CommandText = "SELECT days FROM Rentals WHERE id = @id";
            getCmd.Parameters.AddWithValue("@id", rentalId);
            var days = Convert.ToInt32(getCmd.ExecuteScalar());

            // YENİ: Tamamen yerel (Türkiye) saatine göre hesaplama yapılıyor
            var localStart = startedAt;
            var localEndDate = localStart.AddDays(days);

            using var cmd = con.CreateCommand();
            cmd.CommandText = @"
                UPDATE Rentals 
                SET start_date = @start, end_date = @end, status = 'active'
                WHERE id = @id";
            
            cmd.Parameters.AddWithValue("@start", localStart.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@end", localEndDate.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@id", rentalId);
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool EndRental(int rentalId)
        {
            return EndRental(rentalId, DateTime.Now);
        }

        public static bool EndRental(int rentalId, DateTime endedAt)
        {
            using var con = GetConnection();
            con.Open();
            using var transaction = con.BeginTransaction();
            try
            {
                using var cmdScooter = con.CreateCommand();
                cmdScooter.CommandText = @"
                    UPDATE Scooters 
                    SET status = 'available' 
                    WHERE id = (SELECT scooter_id FROM Rentals WHERE id = @rentalId)";
                cmdScooter.Parameters.AddWithValue("@rentalId", rentalId);
                cmdScooter.ExecuteNonQuery();

                using var cmdRental = con.CreateCommand();
                cmdRental.CommandText = @"
                    UPDATE Rentals 
                    SET status = 'finished', end_date = @end 
                    WHERE id = @rentalId";
                
                cmdRental.Parameters.AddWithValue("@end", endedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                cmdRental.Parameters.AddWithValue("@rentalId", rentalId);
                cmdRental.ExecuteNonQuery();

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine("Kiralama Sonlandırma Hatası: " + ex.Message);
                return false;
            }
        }

        public static Rentals getActiveRental(int userId)
        {
            Rentals rental = null;
            using var con = GetConnection();
            con.Open();
            using var cmd = con.CreateCommand();
            cmd.CommandText = @"
                SELECT r.*, s.name as scooter_name, s.qr_code 
                FROM Rentals r 
                JOIN Scooters s ON r.scooter_id = s.id 
                WHERE r.user_id = @userId AND r.status IN ('pending', 'active')
                LIMIT 1";

            cmd.Parameters.AddWithValue("@userId", userId);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                rental = new Rentals
                {
                    id = Convert.ToInt32(reader["id"]),
                    scooter_id = Convert.ToInt32(reader["scooter_id"]),
                    scooter_name = reader["scooter_name"].ToString(),
                    qr_code = reader["qr_code"].ToString(),
                    start_date = reader["start_date"] == DBNull.Value ? null : reader["start_date"].ToString(),
                    end_date = reader["end_date"] == DBNull.Value ? null : reader["end_date"].ToString(),
                    total_price = Convert.ToDouble(reader["total_price"]),
                    status = reader["status"].ToString()
                };
            }
            return rental;
        }

        public static DataTable GetAllRentals()
        {
            using var con = GetConnection();
            con.Open();
            using var cmd = con.CreateCommand();
            cmd.CommandText = @"
                SELECT 
                    r.id AS 'Kiralama ID', 
                    u.name || ' ' || u.surname AS 'Müşteri', 
                    s.name AS 'Scooter', 
                    r.start_date AS 'Başlangıç', 
                    r.end_date AS 'Bitiş', 
                    r.total_price AS 'Ücret (TL)', 
                    r.status AS 'Durum'
                FROM Rentals r
                JOIN Users u ON r.user_id = u.id
                JOIN Scooters s ON r.scooter_id = s.id
                ORDER BY r.created_at DESC";

            using var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            return dt;
        }

        public static void ExpireOverdueRentals()
        {
            using var con = GetConnection();
            con.Open();
            using var cmd = con.CreateCommand();

            // YENİ: SQLite tarafında da Türkiye yerel saati (localtime) ile tam eşleşme sağlandı.
            cmd.CommandText = @"
                SELECT r.id, r.scooter_id FROM Rentals r
                WHERE r.status = 'active' 
                AND r.end_date IS NOT NULL 
                AND datetime(r.end_date) < datetime('now', 'localtime')";

            var expired = new List<(int rentalId, int scooterId)>();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                expired.Add((reader.GetInt32(0), reader.GetInt32(1)));
            reader.Close();

            foreach (var (rentalId, scooterId) in expired)
            {
                EndRental(rentalId, DateTime.Now);
                UpdateScooterStatus(scooterId, "available");
                Console.WriteLine($"[Auto] Rental #{rentalId} süresi doldu, kapatıldı.");
            }
        }

        #endregion
    }
}