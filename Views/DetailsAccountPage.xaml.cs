using PasswordManager.Models;
using PasswordManager.ViewModels;

namespace PasswordManager.Views;

public partial class DetailsAccountPage
{
    DetailsAccountViewModel vm;
    public DetailsAccountPage(Cuenta account)
	{
		InitializeComponent();
		BindingContext = vm = new DetailsAccountViewModel(account);
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        vm.DesencriptPassword();
        btnGuardar.IsVisible = false;
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        btnGuardar.IsVisible = true;
    }
}