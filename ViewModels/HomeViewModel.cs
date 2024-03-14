using Mopups.Services;
using PasswordManager.Data;
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
        await MopupService.Instance.PushAsync(new AddAccountPage());
    }
    public async Task GetCuentas()
    {
        Cuentas = new ObservableCollection<Cuenta>(await db.GetAccounts());
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
    #endregion
    #region COMANDOS
    public ICommand RedirectAddCommand => new Command(async () => await RedirectAddAccount());
    public ICommand ShowDetailsCommand => new Command<Cuenta>(async (p) => await ShowDetails(p));
    public ICommand DeleteAccountCommand => new Command<Cuenta>(async (p) => await DeleteAccount(p));
    #endregion
}
