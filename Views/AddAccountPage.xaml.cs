using PasswordManager.Models;
using PasswordManager.ViewModels;

namespace PasswordManager.Views;
public partial class AddAccountPage
{
	bool usePasswordGenerator;
	readonly AddAccountViewModel viewModel;
    public AddAccountPage(int idCategoria, Cuenta? account = null)
	{
		InitializeComponent();
		BindingContext = viewModel = new AddAccountViewModel(Navigation,idCategoria, account);
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		viewModel.Password = "";
		usePasswordGenerator = !usePasswordGenerator;
		BtnUsePasswordGenerator.IsVisible = !usePasswordGenerator;
		StckPasswordGenerator.IsVisible = usePasswordGenerator;
    }

	private bool ValidateForm()
	{
		bool isValid = true;
        if (string.IsNullOrEmpty(viewModel.Nombre))
		{
            viewModel.Error = "Debe completar todos los campos";
            isValid = false;
			viewModel.Nombre = "";
        }
		if (string.IsNullOrEmpty(viewModel.Usuario))
		{
            viewModel.Error = "Debe completar todos los campos";
			isValid = false;
			viewModel.Usuario = "";
        }
		if (string.IsNullOrEmpty(viewModel.Password))
		{
            viewModel.Error = "Debe completar todos los campos";
			isValid = false;
			viewModel.Password = "";
        }
        if (viewModel.IdCategoria == 0)
        {
            viewModel.Error = "Debe seleccionar una categoría";
            isValid = false;
        }
        return isValid;
	}
    private async void BtnGuardar_Clicked(object sender, EventArgs e)
    {
		if (ValidateForm()) await viewModel.SaveAccount();
    }
}