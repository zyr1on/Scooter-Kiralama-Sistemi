namespace Scooter_Kiralama_Sistemi
{
    public class Users
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public double balance { get; set; }
        public UserRole role { get; set; }
    }

    public enum UserRole
    {
        Admin = 0,     
        User = 1    
    }

}