using Mopups.Services;
using PasswordManager.Data;
using PasswordManager.Helpers;
using PasswordManager.Models;
using System.Windows.Input;

namespace PasswordManager.ViewModels
{
    public class AddAccountViewModel : BaseViewModel
    {
        #region VARIABLES
        string _Password;
        public string SitioWeb { get; set; }
        public string Url { get; set; }
        public string Usuario { get; set; }
        private List<string> Errors { get; set; }
        readonly TextEncript encript;
        readonly SQLiteHelper db;
        #endregion
        #region CONSTRUCTOR
        public AddAccountViewModel()
        {
            encript = new TextEncript();
            db = new SQLiteHelper();
        }
        #endregion
        #region OBJETOS
        public string Password
        {
            get { return _Password; }
            set { SetValue(ref _Password, value); }
        }
        #endregion
        #region PROCESOS
        private void GeneratePassword()
        {
            Password = encript.GeneratePassword();
        }
        private async Task SaveAccount()
        {
            try
            {
                ValidateForm();
                if (Errors.Count > 0)
                {
                    await DisplayAlert("Error", Errors[0], "Ok");
                    return;
                }
                var password = encript.EncriptPassword(Password);
                Cuenta account = new()
                {
                    SitioWeb = SitioWeb,
                    UrlSitio = Url,
                    Usuario = Usuario,
                    Password = password
                };
                await db.Save(account);
                await MopupService.Instance.PopAsync();
            }
            catch (Exception)
            {
                await DisplayAlert("Aviso", "No se pudo registrar la cuenta", "Ok");
            }
        }
        private void ValidateForm()
        {
            Errors ??= new List<string>();
            if (string.IsNullOrEmpty(SitioWeb)) Errors.Add("Debe ingresar el nombre de la aplicación o sitio web");
            else if (string.IsNullOrEmpty(Usuario)) Errors.Add("Debe ingresar un usuario");
            else if (string.IsNullOrEmpty(Password)) Errors.Add("Debe ingresar una contraseña");
        }
        #endregion
        #region COMANDOS
        public ICommand GeneratePasswordCommand => new Command(GeneratePassword);
        public ICommand SaveCommand => new Command(async () => await SaveAccount());
        #endregion
    }
}
