using PasswordManager.Data;
using PasswordManager.Helpers;
using PasswordManager.Models;
using System.Windows.Input;

namespace PasswordManager.ViewModels
{
    public class SiginViewModel : BaseViewModel
    {
        #region VARIABLES
        private string error = "";
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string ClavePin { get; set; }

        private readonly SQLiteHelper db;
        #endregion
        #region CONSTRUCTOR
        public SiginViewModel(INavigation navigation)
        {
            Navigation = navigation;
            db = new SQLiteHelper();
        }
        #endregion
        #region OBJETOS
        public string Error
        {
            get { return error; }
            set { SetValue(ref error, value); }
        }
        #endregion
        #region PROCESOS
        public async Task Return()
        {
            await Navigation.PopAsync();
        }
        public async Task SaveUser()
        {
            TextEncript encript = new TextEncript();
            var password = encript.EncriptPassword(Password);
            var user = new User()
            {
                Name = Name,
                LastName = Lastname,
                Usuario = User,
                Phone = Phone,
                Password = password,
                ClavePin = ClavePin
            };
            await db.Save(user);
            await Return();
        }
        #endregion
        #region COMANDOS
        public ICommand ReturnCommand => new Command(async () => await Return());
        #endregion
    }
}
