namespace RecibosNominaCGA.Views;

public partial class LoginShell : Shell
{
	public LoginShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("AppShell", typeof(AppShell));
    }
}
