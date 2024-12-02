using RecibosNominaCGA.VIEWMODELS;

namespace RecibosNominaCGA.Views;

public partial class InicioPage : ContentPage
{
	public InicioPage()
	{
        InitializeComponent();
		BindingContext = new InicioViewModel(Navigation);
	}
}
