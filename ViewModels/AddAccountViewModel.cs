﻿using PasswordManager.Data;
using PasswordManager.Helpers;
using PasswordManager.Models;
using System.Windows.Input;

namespace PasswordManager.ViewModels;
public class AddAccountViewModel : BaseViewModel
{
    #region VARIABLES
    private int lengthPassword = 16;
    private string errorPasswordGenerator = "";
    private List<string> icons = ["facebook","instagram","snapchat"];
    private string icon;
    string _Password;
    private Cuenta account;
    private string title;
    private string nombre;
    private string usuario;
    private string error = "";
    private int idCategoria;
    public bool UseNumbers { get; set; } = true;
    public bool UseSymbols { get; set; } = true;
    public bool UseUppers { get; set; } = true;
    public bool UseLowers { get; set; } = true;
    readonly TextEncript encript = new();
    readonly SQLiteHelper db = new();
    #endregion
    #region CONSTRUCTOR
    public AddAccountViewModel(INavigation navigation, int idCategoria, Cuenta? account)
    {
        Navigation = navigation;
        IdCategoria = idCategoria;
        Title = "Agregar cuenta";
        if (account != null)
        {
            Title = "Editar cuenta";
            Account = account;
            Nombre = Account.Nombre;
            Usuario = Account.Usuario;
            Icon = Account.IconImage;
            Password = encript.DesencriptPassword(Account.Password);
        }
    }
    #endregion
    #region OBJETOS
    public string Password
    {
        get { return _Password; }
        set { SetValue(ref _Password, value); }
    }
    public int IdCategoria
    {
        get { return idCategoria; }
        set { SetValue(ref idCategoria, value); }
    }
    public int LengthPassword
    {
        get { return lengthPassword; }
        set { SetValue(ref lengthPassword, value); }
    }
    public string ErrorPasswordGenerator
    {
        get { return errorPasswordGenerator; }
        set { SetValue(ref errorPasswordGenerator, value); }
    }
    public List<string> Icons
    {
        get { return icons; }
        set { SetValue(ref icons, value); }
    }
    public string Icon
    {
        get { return icon; }
        set { SetValue(ref icon, value); }
    }
    public Cuenta Account
    {
        get { return account; }
        set { SetValue(ref account, value); }
    }
    public string Title
    {
        get { return title; }
        set { SetValue(ref title, value); }
    }
    public string Nombre
    {
        get { return nombre; }
        set { SetValue(ref nombre, value); }
    }
    public string Usuario
    {
        get { return usuario; }
        set { SetValue(ref usuario, value); }
    }
    public string Error
    {
        get { return error; }
        set { SetValue(ref error, value); }
    }
    #endregion
    #region PROCESOS
    private void GeneratePassword()
    {
        Password = "";
        ErrorPasswordGenerator = "";
        try
        {
            Password = TextEncript.GeneratePassword(LengthPassword, UseNumbers, UseSymbols, UseUppers, UseLowers);
        }
        catch (Exception ex)
        {
            ErrorPasswordGenerator = ex.Message;
        }
    }
    private void SeleccionarCategoria(string idCategoria)
    {
        if (idCategoria == "1") Icon = "";
        IdCategoria = int.Parse(idCategoria);
    }
    public async Task SaveAccount()
    {
        try
        {
            var password = encript.EncriptPassword(Password);
            if (Account != null)
            {
                Account.Nombre = Nombre;
                Account.UltimoAcceso = DateTime.Now;
                Account.Usuario = Usuario;
                Account.Password = password;
                Account.CategoriaId = IdCategoria;
                Account.IconImage = Icon;
                await db.UpdateAccount(Account);
                await Navigation.PopAsync();
                return;
            }
            Cuenta account = new()
            {
                Nombre = Nombre,
                UltimoAcceso = DateTime.Now,
                Usuario = Usuario,
                Password = password,
                CategoriaId = IdCategoria,
                IconImage = Icon??=""
            };
            await db.Save(account);
            await Navigation.PopAsync();
        }
        catch (Exception)
        {
            await DisplayAlert("Aviso", "No se pudo registrar la cuenta", "Ok");
        }
    }
    private void AddPasswordLength() => LengthPassword++;
    private void DecreasePasswordLength()
    {
        if (LengthPassword > 0) LengthPassword--;
    }
    private async void CopyToClipboard()
    {
        await Clipboard.Default.SetTextAsync(Password);
    }
    private void SelectIcon(string icon)
    {
        Icon = icon;
    }
    #endregion
    #region COMANDOS
    public ICommand GeneratePasswordCommand => new Command(GeneratePassword);
    public ICommand AddPasswordLengthCommand => new Command(AddPasswordLength);
    public ICommand DecreasePasswordLengthCommand => new Command(DecreasePasswordLength);
    public ICommand SeleccionarCategoriaCommand => new Command<string>(SeleccionarCategoria);
    public ICommand SaveCommand => new Command(async () => await SaveAccount());
    public ICommand CopyToClipboardCommand => new Command(CopyToClipboard);
    public ICommand SelectIconCommand => new Command<string>(SelectIcon);
    #endregion
}
