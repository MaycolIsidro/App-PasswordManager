using Mopups.Services;
using PasswordManager.Data;
using PasswordManager.Helpers;
using PasswordManager.Models;
using System.Windows.Input;

namespace PasswordManager.ViewModels;
public class AddAccountViewModel : BaseViewModel
{
    #region VARIABLES
    private int lengthPassword = 16;
    private int idCategoria;
    private string errorPasswordGenerator = "";
    string _Password;
    public string SitioWeb { get; set; }
    public string Usuario { get; set; }
    public bool UseNumbers { get; set; } = true;
    public bool UseSymbols { get; set; } = true;
    public bool UseUppers { get; set; } = true;
    public bool UseLowers { get; set; } = true;
    readonly TextEncript encript = new();
    readonly SQLiteHelper db = new();
    #endregion
    #region CONSTRUCTOR
    public AddAccountViewModel(int idCategoria)
    {
        IdCategoria = idCategoria;
    }
    #endregion
    #region OBJETOS
    public string Password
    {
        get { return _Password; }
        set { SetValue(ref _Password, value); }
    }
    public int IdCategoria
    {
        get { return idCategoria; }
        set { SetValue(ref idCategoria, value); }
    }
    public int LengthPassword
    {
        get { return lengthPassword; }
        set { SetValue(ref lengthPassword, value); }
    }
    public string ErrorPasswordGenerator
    {
        get { return errorPasswordGenerator; }
        set { SetValue(ref errorPasswordGenerator, value); }
    }
    #endregion
    #region PROCESOS
    private void GeneratePassword()
    {
        Password = "";
        ErrorPasswordGenerator = "";
        try
        {
            Password = TextEncript.GeneratePassword(LengthPassword, UseNumbers, UseSymbols, UseUppers, UseLowers);
        }
        catch (Exception ex)
        {
            ErrorPasswordGenerator = ex.Message;
        }
    }
    private void SeleccionarCategoria(string idCategoria)
    {
        IdCategoria = int.Parse(idCategoria);
    }
    public async Task SaveAccount()
    {
        try
        {
            var password = encript.EncriptPassword(Password);
            Cuenta account = new()
            {
                Nombre = SitioWeb,
                UltimoAcceso = DateTime.Now,
                Usuario = Usuario,
                Password = password,
                CategoriaId = IdCategoria
            };
            await db.Save(account);
            await Navigation.PopAsync();
        }
        catch (Exception)
        {
            await DisplayAlert("Aviso", "No se pudo registrar la cuenta", "Ok");
        }
    }
    private void AddPasswordLength()
    {
        LengthPassword++;
    }
    private void DecreasePasswordLength()
    {
        if (LengthPassword > 0) LengthPassword--;
    }
    private async void CopyToClipboard()
    {
        await Clipboard.Default.SetTextAsync(Password);
    }
    #endregion
    #region COMANDOS
    public ICommand GeneratePasswordCommand => new Command(GeneratePassword);
    public ICommand AddPasswordLengthCommand => new Command(AddPasswordLength);
    public ICommand DecreasePasswordLengthCommand => new Command(DecreasePasswordLength);
    public ICommand SeleccionarCategoriaCommand => new Command<string>(SeleccionarCategoria);
    public ICommand SaveCommand => new Command(async () => await SaveAccount());
    public ICommand CopyToClipboardCommand => new Command(CopyToClipboard);
    #endregion
}
