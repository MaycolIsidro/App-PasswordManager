using PasswordManager.ViewModels;
using SQLite;

namespace PasswordManager.Models;
public class Cuenta : BaseViewModel
{
    [PrimaryKey, AutoIncrement]
    public int CuentaId { get; set; }
    public string Nombre { get; set; }
    public string Usuario { get; set; }
    public string Password {  get; set; }

    private string passwordView;
    public string PasswordView
    {
        get { return ShowPassword ? passwordView : "********"; }
        set { SetValue(ref passwordView, value); }
    }
    public string IconoName { get; set; }
    public string Icono => string.IsNullOrEmpty(IconoName) ? Nombre[0].ToString().ToUpper() : IconoName;
    public DateTime UltimoAcceso { get; set; }
    public int CategoriaId { get; set; }
    public bool ShowPassword { get; set; }
}
