using Mopups.Services;
using PasswordManager.Data;
using PasswordManager.Helpers;
using PasswordManager.Models;
using PasswordManager.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PasswordManager.ViewModels;
public class HomeViewModel : BaseViewModel
{
    #region VARIABLES
    private ObservableCollection<Cuenta> cuentas;
    private readonly SQLiteHelper db;
    readonly TextEncript textEncript = new();
    #endregion
    #region CONSTRUCTOR
    public HomeViewModel(INavigation navigation)
    {
        Navigation = navigation;
        db = new SQLiteHelper();
    }
    #endregion
    #region OBJETOS
    public ObservableCollection<Cuenta> Cuentas
    {
        get { return cuentas; }
        set { SetValue(ref cuentas, value); }
    }
    #endregion
    #region PROCESOS
    public async Task RedirectAddAccount()
    {
        await Navigation.PushAsync(new AddAccountPage(0));
    }
    public async Task GetCuentas()
    {
        Cuentas = new ObservableCollection<Cuenta>(await db.GetAccountsRecent());
    }
    public async Task ShowDetails(Cuenta account)
    {
        await MopupService.Instance.PushAsync(new DetailsAccountPage(account));
    }
    public async Task DeleteAccount(Cuenta account)
    {
        bool question = await DisplayAlert("Aviso", "¿Está seguro que desea eliminar el registro?", "Sí", "No");
        if (question)
        {
            await db.DeleteAccount(account);
        }
    }
    private async void CopyToClipboard(Cuenta cuenta)
    {
        await UpdateDateAccess(cuenta);
        await Clipboard.Default.SetTextAsync(textEncript.DesencriptPassword(cuenta.Password));
    }
    private async Task RedirectionToListAccounts(string idCategoria)
    {
        await Navigation.PushAsync(new ListAccountsPage(int.Parse(idCategoria)));
    }
    private async void ShowPassword(Cuenta cuenta)
    {
        cuenta.ShowPassword = !cuenta.ShowPassword;
        if (cuenta.ShowPassword)
        {
            await UpdateDateAccess(cuenta);
            cuenta.PasswordView = textEncript.DesencriptPassword(cuenta.Password);
        }
        else cuenta.PasswordView = cuenta.Password;
    }
    private async Task UpdateDateAccess(Cuenta cuenta)
    {
        cuenta.UltimoAcceso = DateTime.Now;
        await db.UpdateAccount(cuenta);
    }
    #endregion
    #region COMANDOS
    public ICommand RedirectAddCommand => new Command(async () => await RedirectAddAccount());
    public ICommand ShowDetailsCommand => new Command<Cuenta>(async (p) => await ShowDetails(p));
    public ICommand DeleteAccountCommand => new Command<Cuenta>(async (p) => await DeleteAccount(p));
    public ICommand CopyToClipboardCommand => new Command<Cuenta>(CopyToClipboard);
    public ICommand RedirectListAccountsCommand => new Command<string>(async (p) => await RedirectionToListAccounts(p));
    public ICommand ShowPasswordCommand => new Command<Cuenta>(ShowPassword);
    #endregion
}
