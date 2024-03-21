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
    private readonly int idCategoria;
    readonly SQLiteHelper db = new ();
    readonly TextEncript textEncript = new();
    List<Cuenta> accounts;
    private ObservableCollection<Cuenta> listAccounts;
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
        set { SetValue(ref textSearch, value);
            FilterAccounts();
        }
    }
    public string Title
    {
        get { return title; }
        set { SetValue(ref title, value); }
    }
    public ObservableCollection<Cuenta> ListAccounts
    {
        get => listAccounts;
        set
        {
            SetValue(ref listAccounts, value);
        }
    }
    #endregion
    #region PROCESOS
    public async Task GetAccounts()
    {
        accounts = await db.GetAccounts(idCategoria);
        ListAccounts = new ObservableCollection<Cuenta>(accounts);
    }
    private async Task RedirectAddAccount()
    {
        await Navigation.PushAsync(new AddAccountPage(idCategoria));
    }
    public async Task RedirectEditAccount(Cuenta account)
    {
        await Navigation.PushAsync(new AddAccountPage(account.CategoriaId, account));
    }
    private async void CopyToClipboard(Cuenta cuenta)
    {
        await UpdateDateAccess(cuenta);
        await Clipboard.Default.SetTextAsync(textEncript.DesencriptPassword(cuenta.Password));
    }
    private async void ShowPassword(Cuenta cuenta)
    {
        await UpdateDateAccess(cuenta);
        cuenta.ShowPassword = !cuenta.ShowPassword;
        if (cuenta.ShowPassword) cuenta.PasswordView = textEncript.DesencriptPassword(cuenta.Password);
        else cuenta.PasswordView = cuenta.Password;
    }
    private async Task UpdateDateAccess(Cuenta cuenta)
    {
        cuenta.UltimoAcceso = DateTime.Now;
        await db.UpdateAccount(cuenta);
    }
    private async Task DeleteAccount(Cuenta account)
    {
        bool question = await DisplayAlert("Aviso", "¿Está seguro que desea eliminar el registro?", "Sí", "No");
        if (question)
        {
            ListAccounts.Remove(account);
            await db.DeleteAccount(account);
        }
    }
    private void FilterAccounts()
    {
        if (string.IsNullOrEmpty(TextSearch)) ListAccounts = new ObservableCollection<Cuenta>(accounts);
        else ListAccounts = new ObservableCollection<Cuenta>(accounts.Where(p => p.Nombre.ToUpper().Contains(TextSearch.ToUpper())));
    }
    #endregion
    #region COMANDOS
    public ICommand RedirectAddAccountCommand => new Command(async () => await RedirectAddAccount());
    public ICommand ShowPasswordCommand => new Command<Cuenta>(ShowPassword);
    public ICommand CopyToClipboardCommand => new Command<Cuenta>(CopyToClipboard);
    public ICommand DeleteAccountCommand => new Command<Cuenta>(async (p) => await DeleteAccount(p));
    public ICommand RedirectEditAccountCommand => new Command<Cuenta>(async (p) => await RedirectEditAccount(p));
    #endregion
}
