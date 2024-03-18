using PasswordManager.ViewModels;

namespace PasswordManager.Views;
public partial class AddAccountPage
{
	bool usePasswordGenerator;
	readonly AddAccountViewModel viewModel;
    public AddAccountPage(int idCategoria)
	{
		InitializeComponent();
		BindingContext = viewModel = new AddAccountViewModel(idCategoria);
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		viewModel.Password = "";
		usePasswordGenerator = !usePasswordGenerator;
		BtnUsePasswordGenerator.IsVisible = !usePasswordGenerator;
		StckPasswordGenerator.IsVisible = usePasswordGenerator;
    }
}