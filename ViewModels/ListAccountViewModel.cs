using PasswordManager.Data;
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
    public ObservableCollection<Cuenta> ListAccounts { get; set; }
    #endregion
    #region CONSTRUCTOR
    public ListAccountViewModel(INavigation navigation, int idCategoria)
    {
        Navigation = navigation;
        this.idCategoria = idCategoria;
        if (idCategoria == 1) Title = "Redes Sociales";
        else if (idCategoria == 1) Title = "Aplicaciones";
        else Title = "Tarjetas";
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
    #endregion
    #region COMANDOS
    //public ICommand SaveCommand => new Command(async () => await UpdateAccount());
    #endregion
}
