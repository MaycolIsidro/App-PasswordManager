using PasswordManager.ViewModels;

namespace PasswordManager.Views;
public partial class AddAccountPage
{
	bool usePasswordGenerator;
	readonly AddAccountViewModel viewModel;
    public AddAccountPage(int idCategoria)
	{
		InitializeComponent();
		BindingContext = viewModel = new AddAccountViewModel(Navigation,idCategoria);
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
		if (string.IsNullOrEmpty(viewModel.SitioWeb))
		{
			isValid = false;
			viewModel.SitioWeb = "";
        }
		if (string.IsNullOrEmpty(viewModel.Usuario))
		{
			isValid = false;
			viewModel.Usuario = "";
        }
		if (string.IsNullOrEmpty(viewModel.Password))
		{
			isValid = false;
			viewModel.Password = "";
        }
		return isValid;
	}
    private async void BtnGuardar_Clicked(object sender, EventArgs e)
    {
		if (ValidateForm()) await viewModel.SaveAccount();
    }
}