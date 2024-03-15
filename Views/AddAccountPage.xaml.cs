using PasswordManager.ViewModels;

namespace PasswordManager.Views;
public partial class AddAccountPage
{
	public AddAccountPage(int idCategoria)
	{
		InitializeComponent();
		BindingContext = new AddAccountViewModel(idCategoria);
	}
}