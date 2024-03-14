using PasswordManager.ViewModels;

namespace PasswordManager.Views;

public partial class ListAccountsPage : ContentPage
{
	readonly ListAccountViewModel viewModel;
	public ListAccountsPage(int idCategoria)
	{
		InitializeComponent();
		BindingContext = viewModel = new ListAccountViewModel(Navigation, idCategoria);
	}
}