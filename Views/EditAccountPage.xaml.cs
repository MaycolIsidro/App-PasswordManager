using PasswordManager.Models;
using PasswordManager.ViewModels;

namespace PasswordManager.Views;

public partial class EditAccountPage
{
	public EditAccountPage(Cuenta account)
	{
		InitializeComponent();
		BindingContext = new EditAccountViewModel(account);
	}
}