using PasswordManager.ViewModels;

namespace PasswordManager.Views;

public partial class AddAccountPage
{
	public AddAccountPage()
	{
		InitializeComponent();
		BindingContext = new AddAccountViewModel();
	}
}