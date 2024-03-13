using PasswordManager.ViewModels;

namespace PasswordManager.Views;
public partial class LoginPinPage : ContentPage
{
	private readonly Label[] Values;
	public LoginPinPage()
	{
		InitializeComponent();
		BindingContext = new LoginPinViewModel(Navigation);
		Values = [LblValue1, LblValue2, LblValue3, LblValue4, LblValue5, LblValue6];
	}

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
		var pin = (Entry)sender;
        if (pin.Text == "")
        {
            foreach (var item in Values)
                item.FontFamily = "FAR";
            return;
        }
        var oldLength = string.IsNullOrEmpty(e.OldTextValue) ? 0 : e.OldTextValue.Length;
        var newLength = e.NewTextValue.Length;
        if (newLength > oldLength)
            Values[newLength - 1].FontFamily = "FAS";
        else
            Values[newLength].FontFamily = "FAR";
    }
}