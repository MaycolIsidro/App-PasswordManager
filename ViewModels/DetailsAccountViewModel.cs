using Mopups.Services;
using PasswordManager.Data;
using PasswordManager.Helpers;
using PasswordManager.Models;
using System.Windows.Input;

namespace PasswordManager.ViewModels
{
    public class DetailsAccountViewModel : BaseViewModel
    {
        #region VARIABLES
        private string password;
        public Cuenta Cuenta { get; set; }
        private readonly SQLiteHelper db;
        private readonly TextEncript encript;
        #endregion
        #region CONSTRUCTOR
        public DetailsAccountViewModel(Cuenta cuenta)
        {
            Cuenta = cuenta;
            db = new SQLiteHelper();
            encript = new TextEncript();
        }
        #endregion
        #region OBJETOS
        public string Password
        {
            get { return password; }
            set { SetValue(ref password, value); }
        }
        public void DesencriptPassword()
        {
            var password = encript.DesencriptPassword(Cuenta.Password);
            Password = password;
        }
        private async Task UpdateAccount()
        {
            try
            {
                var password = encript.EncriptPassword(Password);
                Cuenta.Password = password;
                await db.UpdateAccount(Cuenta);
                await DisplayAlert("Aviso", "Registro actualizado correctamente", "Ok");
                await MopupService.Instance.PopAsync();
            }
            catch (Exception)
            {
                await DisplayAlert("Aviso", "No se pudo actualizar el registro", "Ok");
            }
        }
        #endregion
        #region PROCESOS
        #endregion
        #region COMANDOS
        public ICommand SaveCommand => new Command(async () => await UpdateAccount());
        #endregion
    }
}
