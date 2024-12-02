namespace RecibosNominaCGA.Views;

using RecibosNominaCGA.ViewModels;
using RecibosNominaCGA.VIEWMODELS;

public partial class NewUserPage : ContentPage
{
	public NewUserPage()
	{
		InitializeComponent();
		BindingContext = new NewUserViewModel(Navigation);
	}
}