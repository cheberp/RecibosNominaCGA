using RecibosNominaCGA.Views;

namespace RecibosNominaCGA;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new NavigationPage(new LoginPage());

    }
}

