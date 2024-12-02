using RecibosNominaCGA.Models;
using RecibosNominaCGA.VIEWMODELS;

namespace RecibosNominaCGA.Views;

//[XamlCompilation(XamlCompilationOptions.Compile)]
//[QueryProperty("PERIODO", "periodo")]

public partial class ReciboPage : ContentPage
{
    //private string pERIODO;
    //private ReciboViewModel vm;

    //public string PERIODO
    //{
    //    get => pERIODO; set
    //    {
    //        pERIODO = value;
    //        vm = new ReciboViewModel(Navigation, pERIODO);
    //        BindingContext = vm;
    //    }
    //}

    public ReciboPage(ListaPeriodosModel Periodo)
	{
		InitializeComponent();
        BindingContext = new ReciboViewModel(Navigation, Periodo);

    }
}