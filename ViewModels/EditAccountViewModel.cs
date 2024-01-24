using Mopups.Services;
using PasswordManager.Data;
using PasswordManager.Helpers;
using PasswordManager.Models;
using System.Windows.Input;

namespace PasswordManager.ViewModels
{
    public class EditAccountViewModel : BaseViewModel
    {
        #region VARIABLES
        private string password;
        public Cuenta Cuenta { get; set; }
        private readonly SQLiteHelper db;
        #endregion
        #region CONSTRUCTOR
        public EditAccountViewModel(Cuenta cuenta)
        {
            Cuenta = cuenta;
            DesencriptPassword();
            db = new SQLiteHelper();
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
            TextEncript encript = new TextEncript();
            var password = encript.DesencriptPassword(Cuenta.Password);
            Password = password;
        }
        #endregion
        #region PROCESOS
        private async Task UpdateAccount()
        {
            try
            {
                TextEncript encript = new TextEncript();
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
        #region COMANDOS
        public ICommand SaveCommand => new Command(async () => await UpdateAccount());
        #endregion
    }
}
