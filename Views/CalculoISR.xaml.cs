using RecibosNominaCGA.VIEWMODELS;

namespace RecibosNominaCGA.Views;

public partial class CalculoISR : ContentPage
{
	public CalculoISR()
	{
        InitializeComponent();
		BindingContext = new CalculoISRViewModel(Navigation);
	}
}
