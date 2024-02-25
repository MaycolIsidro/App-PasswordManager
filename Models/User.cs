using SQLite;

namespace PasswordManager.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string ClavePin { get; set; }
    }
}
