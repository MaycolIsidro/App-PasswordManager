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
        var lastIdUserLogin = Preferences.Get("LastIdUserLogin", 0);
        if (lastIdUserLogin != 0)
        {
            App.Current.MainPage = new NavigationPage(new LoginPinPage());
        }
    }
}