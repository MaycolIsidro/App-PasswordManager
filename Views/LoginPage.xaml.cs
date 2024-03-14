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
        bool RecordarSesion = Preferences.Get("RecordarSesion", false);
        if (RecordarSesion)
        {
            App.Current.MainPage = new NavigationPage(new LoginPinPage());
        }
    }
}