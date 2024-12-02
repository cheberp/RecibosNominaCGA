using RecibosNominaCGA.Views;

namespace RecibosNominaCGA;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("ReciboPage", typeof(ReciboPage));
        Routing.RegisterRoute("VisorRecibo", typeof(VisorReciboPage));
        Routing.RegisterRoute("Iniciop", typeof(InicioPage));
    }
}

