﻿using Mopups.Services;
using PasswordManager.Data;
using PasswordManager.Helpers;
using PasswordManager.Models;
using System.Windows.Input;

namespace PasswordManager.ViewModels;
public class AddAccountViewModel : BaseViewModel
{
    #region VARIABLES
    private int lengthPassword = 16;
    private int idCategoria;
    string _Password;
    public string SitioWeb { get; set; }
    public string Url { get; set; }
    public string Usuario { get; set; }
    public bool UseNumbers { get; set; } = true;
    public bool UseSymbols { get; set; } = true;
    public bool UseUppers { get; set; } = true;
    public bool UseLowers { get; set; } = true;
    private List<string> Errors { get; set; }
    readonly TextEncript encript = new();
    readonly SQLiteHelper db = new();
    #endregion
    #region CONSTRUCTOR
    public AddAccountViewModel(int idCategoria)
    {
        IdCategoria = idCategoria;
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
    #endregion
    #region PROCESOS
    private void GeneratePassword()
    {
        Password = TextEncript.GeneratePassword(LengthPassword, UseNumbers, UseSymbols, UseUppers, UseLowers);
    }
    private void SeleccionarCategoria(string idCategoria)
    {
        IdCategoria = int.Parse(idCategoria);
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
        Errors ??= [];
        if (string.IsNullOrEmpty(SitioWeb)) Errors.Add("Debe ingresar el nombre de la aplicación o sitio web");
        else if (string.IsNullOrEmpty(Usuario)) Errors.Add("Debe ingresar un usuario");
        else if (string.IsNullOrEmpty(Password)) Errors.Add("Debe ingresar una contraseña");
    }
    private void AddPasswordLength()
    {
        LengthPassword++;
    }
    private void DecreasePasswordLength()
    {
        if (LengthPassword > 0) LengthPassword--;
    }
    #endregion
    #region COMANDOS
    public ICommand GeneratePasswordCommand => new Command(GeneratePassword);
    public ICommand AddPasswordLengthCommand => new Command(AddPasswordLength);
    public ICommand DecreasePasswordLengthCommand => new Command(DecreasePasswordLength);
    public ICommand SeleccionarCategoriaCommand => new Command<string>(SeleccionarCategoria);
    public ICommand SaveCommand => new Command(async () => await SaveAccount());
    #endregion
}
