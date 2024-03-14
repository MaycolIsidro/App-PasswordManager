using SQLite;

namespace PasswordManager.Models
{
    public class Cuenta
    {
        [PrimaryKey, AutoIncrement]
        public int CuentaId { get; set; }
        public string SitioWeb { get; set; }
        public string UrlSitio { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public int CategoriaId { get; set; }

    }
}
