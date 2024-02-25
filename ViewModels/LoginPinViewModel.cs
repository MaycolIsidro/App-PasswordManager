using Mopups.Services;
using PasswordManager.Data;
using PasswordManager.Models;
using PasswordManager.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PasswordManager.ViewModels
{
    internal class LoginPinViewModel : BaseViewModel
    {
        #region VARIABLES
        private ObservableCollection<Cuenta> cuentas;
        private readonly SQLiteHelper db;
        private string pin = "";
        private List<Color> colorsValue = new List<Color> { Colors.White, Colors.White, Colors.White, Colors.White, Colors.White, Colors.White };
        #endregion
        #region CONSTRUCTOR
        public LoginPinViewModel(INavigation navigation)
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
        public string Pin
        {
            get { return pin; }
            set { SetValue(ref pin, value);
                if (pin.Length == 6) LoginUser();
            }
        }

        public List<Color> ColorsValue
        {
            get { return colorsValue; }
            set { SetValue(ref colorsValue, value); }
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
        public void WritePin(string value)
        {
            Pin += value;
            ColorsValue[Pin.Length - 1] = Colors.Black;
            OnPropertyChanged(nameof(ColorsValue));
        }
        private void DeletePin()
        {
            if (Pin.Length > 0)
            {
                ColorsValue[Pin.Length - 1] = Colors.White;
                Pin = Pin.Substring(0, Pin.Length - 1);
                OnPropertyChanged(nameof(ColorsValue));
            }
        }
        private async void LoginUser()
        {
            App.Current.MainPage = new NavigationPage(new HomePage());
        }
        private void ReturnLoginPage()
        {
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }
        #endregion
        #region COMANDOS
        public ICommand RedirectAddCommand => new Command(async () => await RedirectAddAccount());
        public ICommand WritePinCommand => new Command<string>(WritePin);
        public ICommand DeletePinCommand => new Command(DeletePin);
        public ICommand ReturnLoginCommand => new Command(ReturnLoginPage);
        #endregion
    }
}
