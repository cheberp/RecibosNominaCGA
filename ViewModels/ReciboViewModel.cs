using System;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;
using RecibosNominaCGA.Models;
using RecibosNominaCGA.Views;

namespace RecibosNominaCGA.VIEWMODELS
{
	public class ReciboViewModel:BaseViewModel
	{
        #region Variables
        string _Usuario;
        string _Password;
        string _Periodo;
        List<ReciboModel> _reciboPercepciones = new List<ReciboModel>();
        List<ReciboModel> _reciboDeducciones = new List<ReciboModel>();
        List<ReciboModel> _reciboVales = new List<ReciboModel>();
        List<ReciboModel> _reciboTotales = new List<ReciboModel>();
        //IFileSaver FileSaver;
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        //WebClient retrievefiles = new WebClient();

        #endregion
        #region Constructor
        public ReciboViewModel(INavigation navigation,ListaPeriodosModel _periodo)
        {
            Navigation = navigation;
            Title = "Periodo del "+_periodo.TAFECHAI.ToString("dd/MM/yy") +" al "+_periodo.TAFECHAF.ToString("dd/MM/yy");
            PERIODO = _periodo.PERIODO.ToString();
            Task cargarecibo = MuestraRecibo();
        }
        #endregion
        #region Objetos
        public string Usuario
        {
            get { return _Usuario; }
            set { SetValue(ref _Usuario, value); }
        }
        public string Password
        {
            get { return _Password; }
            set { SetValue(ref _Password, value); }
        }
        public List<ReciboModel> ReciboPercepciones
        {
            get { return _reciboPercepciones; }
            set { SetValue(ref _reciboPercepciones, value); }
        }
        public List<ReciboModel> ReciboDeducciones
        {
            get { return _reciboDeducciones; }
            set { SetValue(ref _reciboDeducciones, value); }
        }
        public List<ReciboModel> ReciboVales
        {
            get { return _reciboVales; }
            set { SetValue(ref _reciboVales, value); }
        }
        public List<ReciboModel> ReciboTotales
        {
            get { return _reciboTotales; }
            set { SetValue(ref _reciboTotales, value); }
        }
        public string PERIODO
        {
            get { return _Periodo; }
            set { SetValue(ref _Periodo, value); }
        }
        #endregion
        #region Procesos
        public async Task DescargaRecibo()
        {
            //string token = Preferences.Get("TOKEN", string.Empty);
            int cia = Preferences.Get("CIA", 0);
            int empleado = Preferences.Get("EMPLEADO", 0);
            string tk = Preferences.Get("TOKEN", string.Empty);
            var client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string uri = "http://cgyasc2014-001-site6.ctempurl.com/api/recibotk?CIA=" + cia.ToString() + "&EMPLEADO=" + empleado.ToString() + "&TOKEN="+tk+"&PERIODO=" + PERIODO;
            Uri siteUri = new Uri(uri);
            
            //await Navigation.PushAsync(new NavigationPage(new VisorReciboPage(PERIODO)));
            //Task<bool> PlatformOpenAsync(NSUrl nativeUrl) =>
            //UIApplication.SharedApplication.OpenUrlAsync(nativeUrl, new UIApplicationOpenUrlOptions());
            //Task<bool> PlatformOpenAsync(OpenFileRequest request) =>
            // Task.FromResult(NSWorkspace.SharedWorkspace.OpenFile(request.File.FullPath));
            await Launcher.OpenAsync(siteUri);

            //retrievefiles.DownloadFile(uri, System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "Recibo.pdf"));
            //await Launcher.Default.OpenAsync(new OpenFileRequest("Nomina_"+PERIODO.ToString(), new ReadOnlyFile(uri)));
            //var response = await client.GetStreamAsync(uri);
        }
        public async Task MuestraRecibo()
        {
            try
            {
                IsBusy = true;
                IsEnable = false;
                int _Cia=Preferences.Get("CIA", 0);
                int _Empleado = Preferences.Get("EMPLEADO", 0);
                string uri = "http://cgyasc2014-001-site6.ctempurl.com/api/recibo?CIA=" + _Cia.ToString()+ "&EMPLEADO="+ _Empleado.ToString()+"&PERIODO="+PERIODO;
                HttpClient client = new HttpClient();
                //var contentJson = new StringContent("application/json");
                var response = await client.GetStringAsync(uri);
                var obj = JsonConvert.DeserializeObject<List<ReciboModel>>(response);
                if (response != null)
                {
                    ReciboPercepciones = obj.Where(p => p.TIPO.Equals("1-PERCEPCIONES")).ToList();
                    ReciboDeducciones = obj.Where(p => p.TIPO.Equals("2-DEDUCCIONES")).ToList();
                    ReciboVales = obj.Where(p => p.TIPO.Equals("3-VALES DESPENSA")).ToList();
                    ReciboTotales = obj.Where(p => p.TIPO.Equals("4-TOTALES")).ToList();
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
        public void MostrarContrasena()
        {

        }
        #endregion
        #region Comandos
        public ICommand DescargaReciboCommand => new Command(async () => await DescargaRecibo());
        //public ICommand Restablecercommand => new Command(async () => await RestablecerContrasena());
        //public ICommand Metodcommand => new Command(MostrarContrasena);
        #endregion
    }
}

