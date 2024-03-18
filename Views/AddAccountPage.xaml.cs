using PasswordManager.ViewModels;

namespace PasswordManager.Views;
public partial class AddAccountPage
{
	bool usePasswordGenerator;
	public AddAccountPage(int idCategoria)
	{
		InitializeComponent();
		BindingContext = new AddAccountViewModel(idCategoria);
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		usePasswordGenerator = !usePasswordGenerator;
		BtnUsePasswordGenerator.IsVisible = !usePasswordGenerator;
		StckPasswordGenerator.IsVisible = usePasswordGenerator;
    }
}