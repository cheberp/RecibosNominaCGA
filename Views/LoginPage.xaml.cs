using RecibosNominaCGA.VIEWMODELS;

namespace RecibosNominaCGA.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel(Navigation);
	}
}
