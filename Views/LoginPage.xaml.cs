using PasswordManager.ViewModels;

namespace PasswordManager.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel(Navigation);
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
		bool sesionIniciada = Preferences.Get("RecordarSesion", false);
        if (sesionIniciada)
        {
            App.Current.MainPage = new NavigationPage(new LoginPinPage());
        }
    }
}