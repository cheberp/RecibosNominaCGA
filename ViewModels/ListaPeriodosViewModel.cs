using System.Windows.Input;
using Newtonsoft.Json;
using RecibosNominaCGA.Models;
using RecibosNominaCGA.Views;

namespace RecibosNominaCGA.VIEWMODELS
{
    public class ListaPeriodosViewModel : BaseViewModel
    {
        #region Variables
        int _Empleado;
        string _Nombre;
        int _Cia;

        List<ListaPeriodosModel> _lp = new List<ListaPeriodosModel>();

        ListaPeriodosModel _lpSeleccted = new ListaPeriodosModel();


        #endregion
        #region Constructor
        public ListaPeriodosViewModel(INavigation navigation)
        {
            Navigation = navigation;
            _Cia = Preferences.Get("CIA", 0);
            Nombre = Preferences.Get("Nombre", string.Empty);
            Empleado = Preferences.Get("EMPLEADO", 0);
            Title = "Nominas";
            Task llenaperiodos= LlenaPeriodos();
        }
        #endregion
        #region Objetos
        public int Empleado
        {
            get { return _Empleado; }
            set { SetValue(ref _Empleado, value); }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { SetValue(ref _Nombre, value); }
        }
        public List<ListaPeriodosModel> LP
        {
            get { return _lp; }
            set { SetValue(ref _lp, value); }
        }
        public ListaPeriodosModel LPSeleccted
        {
            get { return _lpSeleccted; }
            set { _lpSeleccted = value; OnPropertyChanged();        
           }
        }
        #endregion
        #region Procesos
        public async Task LlenaPeriodos()
        {
            try
            {
                IsBusy = true;
                IsEnable = false;

                string uri = "http://cgyasc2014-001-site6.ctempurl.com/api/periodo?CIA=" + _Cia + "&EMPLEADO="+ Empleado;
                HttpClient client = new HttpClient();
                //var contentJson = new StringContent("application/json");
                var response = await client.GetStringAsync(uri);
                var obj = JsonConvert.DeserializeObject<List<ListaPeriodosModel>>(response);
                if (response != null)
                {
                    LP = obj;
                }
                else
                {
                    await DisplayAlert("Error", "Ha ocurrido un problama", "Aceptar");
                    IsEnable = true;
                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Continuar");
            }
        }
        public async Task IrDetalle(ListaPeriodosModel Lps)
        {
            //await DisplayAlert("Item", "El periodo seleccionado es :" + Lps.PERIODO.ToString() + "-" + Lps.DESCRIPCION, "Ok");
            //await Shell.Current.GoToAsync($"ReciboPage?periodo={Lps.PERIODO.ToString()}");
            await Navigation.PushAsync( new NavigationPage(new ReciboPage(Lps)));
        }
        public void MostrarContrasena()
        {

        }
        #endregion
        #region Comandos
        //public ICommand Asynccommand => new Command(async () => await Prueba());
        //public ICommand Restablecercommand => new Command(async () => await RestablecerContrasena());
        public ICommand Metodcommand => new Command(MostrarContrasena);
        public ICommand DetalleCommand => new Command(async () => await IrDetalle(LPSeleccted));
        #endregion
    }
}

