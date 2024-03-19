using PasswordManager.Data;
using PasswordManager.Helpers;
using PasswordManager.Models;
using PasswordManager.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PasswordManager.ViewModels;
public class ListAccountViewModel : BaseViewModel
{
    #region VARIABLES
    private string title;
    private string textSearch;
    private int idCategoria;
    readonly SQLiteHelper db = new ();
    readonly TextEncript textEncript = new();
    private ObservableCollection<Cuenta> listAccounts;
    public ObservableCollection<Cuenta> ListAccounts
    {
        get => listAccounts;
        set
        {
            SetValue(ref listAccounts, value);
        }
    }
    #endregion
    #region CONSTRUCTOR
    public ListAccountViewModel(INavigation navigation, int idCategoria)
    {
        Navigation = navigation;
        this.idCategoria = idCategoria;
        if (idCategoria == 1) Title = "Sitios Web";
        else if (idCategoria == 2) Title = "Aplicaciones";
    }
    #endregion
    #region OBJETOS
    public string TextSearch
    {
        get { return textSearch; }
        set { SetValue(ref textSearch, value); }
    }
    public string Title
    {
        get { return title; }
        set { SetValue(ref title, value); }
    }
    #endregion
    #region PROCESOS
    public async Task GetAccounts()
    {
        ListAccounts = new ObservableCollection<Cuenta>(await db.GetAccounts(idCategoria));
    }
    private async Task RedirectAddAccount()
    {
        await Navigation.PushAsync(new AddAccountPage(idCategoria));
    }
    private void ShowPassword(Cuenta cuenta)
    {
        cuenta.ShowPassword = !cuenta.ShowPassword;
        if (cuenta.ShowPassword) cuenta.PasswordView = textEncript.DesencriptPassword(cuenta.Password);
        else cuenta.PasswordView = cuenta.Password;
    }
    #endregion
    #region COMANDOS
    public ICommand RedirectAddAccountCommand => new Command(async () => await RedirectAddAccount());
    public ICommand ShowPasswordCommand => new Command<Cuenta>(ShowPassword);
    #endregion
}
