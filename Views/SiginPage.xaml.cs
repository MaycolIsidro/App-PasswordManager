using PasswordManager.ViewModels;

namespace PasswordManager.Views;

public partial class SiginPage : ContentPage
{
	public SiginPage()
	{
		InitializeComponent();
		BindingContext = new SiginViewModel(Navigation);
	}
}