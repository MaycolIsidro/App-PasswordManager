using PasswordManager.ViewModels;

namespace PasswordManager.Views;

public partial class LoginPinPage : ContentPage
{
	public LoginPinPage()
	{
		InitializeComponent();
		BindingContext = new LoginPinViewModel(Navigation);
	}
}