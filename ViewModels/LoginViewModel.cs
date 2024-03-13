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
        private bool recordarSesion;
        #endregion
        #region CONSTRUCTOR
        public LoginViewModel(INavigation navigation)
        {
            Navigation = navigation;
            RecordarSesion = Preferences.Get("RecordarSesion", false);
        }
        #endregion
        #region OBJETOS
        public bool RecordarSesion
        {
            get { return recordarSesion; }
            set { SetValue(ref recordarSesion, value); }
        }
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
            Preferences.Set("RecordarSesion", RecordarSesion);
            App.Current.MainPage = new NavigationPage(new HomePage());
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
