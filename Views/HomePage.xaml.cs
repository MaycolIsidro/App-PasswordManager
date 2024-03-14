using Mopups.Services;
using PasswordManager.Models;
using PasswordManager.ViewModels;

namespace PasswordManager.Views;

public partial class HomePage : ContentPage
{
	HomeViewModel vm;
	public HomePage()
	{
		InitializeComponent();
		BindingContext = vm = new HomeViewModel(Navigation);
        this.Appearing += (sender, e) =>
        {
            MopupService.Instance.Popped += Instance_Popped;
        };
	}

    private async void Instance_Popped(object? sender, Mopups.Events.PopupNavigationEventArgs e)
    {
        await vm.GetCuentas();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await vm.GetCuentas();
    }
}