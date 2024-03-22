using PasswordManager.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace PasswordManager.Views;

public partial class SiginPage : ContentPage
{
	readonly SiginViewModel viewModel;
    public SiginPage()
	{
		InitializeComponent();
		BindingContext = viewModel = new SiginViewModel(Navigation);
	}
	private bool ValidateForm()
	{
		bool isValid = true;
		if (string.IsNullOrEmpty(TxtNombres.Text))
		{
			isValid = false;
            viewModel.Error = "Debe completar todos los campos";
			TxtNombres.Text = "";
		}
        if (string.IsNullOrEmpty(TxtPhone.Text))
        {
            isValid = false;
            viewModel.Error = "Debe completar todos los campos";
            TxtPhone.Text = "";
        }
        if (string.IsNullOrEmpty(TxtUser.Text))
        {
            isValid = false;
            viewModel.Error = "Debe completar todos los campos";
            TxtUser.Text = "";
        }
        if (string.IsNullOrEmpty(TxtPassword.Text))
        {
            isValid = false;
            viewModel.Error = "Debe completar todos los campos";
            TxtPassword.Text = "";
        }
        if (TxtPin.Text == null || TxtPin.Text.Length < 6)
        {
            isValid = false;
            viewModel.Error = "El PIN debe contener 6 caracteres";
            TxtPin.Text = "";
        }
        return isValid;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (ValidateForm()) await viewModel.SaveUser();
    }
}