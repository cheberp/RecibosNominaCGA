namespace RecibosNominaCGA.Views;

using System.Windows.Input;
using RecibosNominaCGA.ViewModels;
using RecibosNominaCGA.VIEWMODELS;

public partial class NewUserPage : ContentPage
{
	public ICommand ValidateFieldCommand { get; }

	public NewUserPage()
	{
		//ValidateFieldCommand = new Command(OnValidateField);
		InitializeComponent();
		BindingContext = new NewUserViewModel(Navigation);
		
	}

	 private void OnValidateField()
    {
        // Lógica para la validación del campo
    }

}