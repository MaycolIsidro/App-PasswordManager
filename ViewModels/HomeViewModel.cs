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
    private ObservableCollection<Cuenta> listAccounts;
    private readonly SQLiteHelper db;
    readonly TextEncript textEncript = new();
    private int numbersOfWebsite;
    private int numbersOfApps;
    #endregion
    #region CONSTRUCTOR
    public HomeViewModel(INavigation navigation)
    {
        Navigation = navigation;
        db = new SQLiteHelper();
    }
    #endregion
    #region OBJETOS
    public ObservableCollection<Cuenta> ListAccounts
    {
        get { return listAccounts; }
        set { SetValue(ref listAccounts, value); }
    }
    public int NumbersOfWebsite
    {
        get { return numbersOfWebsite; }
        set { SetValue(ref numbersOfWebsite, value); }
    }
    public int NumbersOfApps
    {
        get { return numbersOfApps; }
        set { SetValue(ref numbersOfApps, value); }
    }
    #endregion
    #region PROCESOS
    public async Task RedirectAddAccount()
    {
        await Navigation.PushAsync(new AddAccountPage(0));
    }
    public async Task GetCuentas()
    {
        ListAccounts = new ObservableCollection<Cuenta>(await db.GetAccountsRecent());
    }
    public async Task GetTotalAccounts()
    {
        var numbersOfAccounts = await db.GetNumberForAccounts();
        NumbersOfWebsite = numbersOfAccounts.Item1;
        NumbersOfApps = numbersOfAccounts.Item2;
    }
    public async Task RedirectEditAccount(Cuenta account)
    {
        await Navigation.PushAsync(new AddAccountPage(account.CategoriaId, account));
    }
    public async Task DeleteAccount(Cuenta account)
    {
        bool question = await DisplayAlert("Aviso", "¿Está seguro que desea eliminar el registro?", "Sí", "No");
        if (question)
        {
            ListAccounts.Remove(account);
            await db.DeleteAccount(account);
            await GetTotalAccounts();
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
    public ICommand RedirectEditAccountCommand => new Command<Cuenta>(async (p) => await RedirectEditAccount(p));
    public ICommand DeleteAccountCommand => new Command<Cuenta>(async (p) => await DeleteAccount(p));
    public ICommand CopyToClipboardCommand => new Command<Cuenta>(CopyToClipboard);
    public ICommand RedirectListAccountsCommand => new Command<string>(async (p) => await RedirectionToListAccounts(p));
    public ICommand ShowPasswordCommand => new Command<Cuenta>(ShowPassword);
    #endregion
}
