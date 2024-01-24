using PasswordManager.Helpers;
using PasswordManager.Models;

namespace PasswordManager.ViewModels
{
    public class DetailsAccountViewModel : BaseViewModel
    {
        #region VARIABLES
        private string password;
        public Cuenta Cuenta { get; set; }
        #endregion
        #region CONSTRUCTOR
        public DetailsAccountViewModel(Cuenta cuenta)
        {
            Cuenta = cuenta;
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
        #endregion
        #region COMANDOS
        #endregion
    }
}
