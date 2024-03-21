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
    public string IconImage { get; set; }
    public string IconText => string.IsNullOrEmpty(IconImage) ? Nombre[0].ToString().ToUpper() : "";
    public DateTime UltimoAcceso { get; set; }
    public int CategoriaId { get; set; }
    public bool ShowPassword { get; set; }
    public string BackColorIcon => string.IsNullOrEmpty(IconImage) ? "#3162F2" : "#EFEFEF";
}
