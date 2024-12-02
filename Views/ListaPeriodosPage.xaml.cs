using RecibosNominaCGA.VIEWMODELS;

namespace RecibosNominaCGA.Views;

public partial class ListaPeriodosPage : ContentPage
{
	public ListaPeriodosPage()
	{
		InitializeComponent();
		BindingContext = new ListaPeriodosViewModel(Navigation);

	}
}
