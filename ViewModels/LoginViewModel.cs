using PasswordManager.Data;
using PasswordManager.Views;
using System.Windows.Input;

namespace PasswordManager.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region VARIABLES
        public string User { get; set; }
        public string Password { get; set; }
        #endregion
        #region CONSTRUCTOR
        public LoginViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion
        #region OBJETOS
        #endregion
        #region PROCESOS
        public async Task LoginUser()
        {
            SQLiteHelper db = new SQLiteHelper();
            var result = await db.LoginUser(User, Password);
            if (result == null)
            {
                await DisplayAlert("Error","Usuario y/o contraseña incorrectos","Ok");
                return;
            }
            await Navigation.PushAsync(new HomePage());
        }
        public async Task RedirectSigin()
        {
            await Navigation.PushAsync(new SiginPage());
        }
        #endregion
        #region COMANDOS
        public ICommand LoginCommand => new Command(async () => await LoginUser());
        public ICommand SiginCommand => new Command(async () => await RedirectSigin());
        #endregion
    }
}
