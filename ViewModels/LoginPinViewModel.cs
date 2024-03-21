using PasswordManager.Data;
using PasswordManager.Views;
using System.Windows.Input;

namespace PasswordManager.ViewModels
{
    internal class LoginPinViewModel : BaseViewModel
    {
        #region VARIABLES
        private readonly SQLiteHelper db;
        private string pin = "";
        private string error = "";
        #endregion
        #region CONSTRUCTOR
        public LoginPinViewModel(INavigation navigation)
        {
            Navigation = navigation;
            db = new SQLiteHelper();
        }
        #endregion
        #region OBJETOS
        public string Pin
        {
            get { return pin; }
            set { SetValue(ref pin, value);
                if (pin.Length == 6) LoginUser();
            }
        }
        public string Error
        {
            get { return error; }
            set { SetValue(ref error, value); }
        }
        #endregion
        #region PROCESOS
        public void WritePin(string value)
        {
            Error = "";
            Pin += value;
        }
        private void DeletePin()
        {
            if (Pin.Length > 0) Pin = Pin.Substring(0, Pin.Length - 1);
        }
        private async void LoginUser()
        {
            var lastIdUserLogin = Preferences.Get("LastIdUserLogin", 0);
            if (lastIdUserLogin == 0) return;
            var user = await db.LoginUserPin(lastIdUserLogin,Pin);
            if (user is null)
            {
                Error = "El PIN es incorrecto";
                Pin = "";
                return;
            }
            App.Current.MainPage = new NavigationPage(new HomePage(user)); 
        }
        private async void ReturnLoginPage()
        {
            Preferences.Set("LastIdUserLogin", 0);
            await Navigation.PushAsync(new LoginPage());
        }
        #endregion
        #region COMANDOS
        public ICommand WritePinCommand => new Command<string>(WritePin);
        public ICommand DeletePinCommand => new Command(DeletePin);
        public ICommand ReturnLoginCommand => new Command(ReturnLoginPage);
        #endregion
    }
}
