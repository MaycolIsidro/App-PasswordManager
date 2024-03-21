using PasswordManager.Models;
using PasswordManager.ViewModels;

namespace PasswordManager.Views;
public partial class HomePage : ContentPage
{
	readonly HomeViewModel vm;
	public HomePage(User user)
	{
		InitializeComponent();
		BindingContext = vm = new HomeViewModel(Navigation, user);
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await vm.GetCuentas();
        await vm.GetTotalAccounts();
    }
}