using System;
using System.Net.Http.Headers;
using System.Reflection;
using System.Windows.Input;
using RecibosNominaCGA.VIEWMODELS;

namespace RecibosNominaCGA.ViewModels
{
	public class VisorReciboViewModel:BaseViewModel
	{
        #region Variables
        string _Usuario;
        string _Password;
        string _Periodo;
        Stream m_pdfDocumentStream;
        string _url;

        #endregion
        #region Constructor
        public VisorReciboViewModel(INavigation navigation, string _periodo)
        {
            Navigation = navigation;
            PERIODO = _periodo;
            CargaRecibo();
            //m_pdfDocumentStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("RecibosNominaCGA.Assets.kioscomanual.pdf");
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
        public Stream PdfDocumentStream
        {
            get { return m_pdfDocumentStream; }
            set
            {
                m_pdfDocumentStream = value; OnPropertyChanged();
            }
        }
        public string PERIODO
        {
            get { return _Periodo; }
            set { SetValue(ref _Periodo, value); }
        }
        public string URL
        {
            get { return _url; }
            set { SetValue(ref _url, value); }
        }
        #endregion
        #region Procesos
        public void  CargaRecibo()
        {

            //string token = Preferences.Get("TOKEN", string.Empty);
            int cia = Preferences.Get("CIA", 0);
            int empleado = Preferences.Get("EMPLEADO", 0);
            var client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string uri = "http://cgyasc2014-001-site6.ctempurl.com/api/rec?CIA=" + cia.ToString() + "&EMPLEADO=" + empleado.ToString() + "&PERIODO=" + PERIODO;
            Uri siteUri = new Uri(uri);

            //await Navigation.PushAsync(new NavigationPage(new VisorReciboPage(PERIODO)));
            //Task<bool> PlatformOpenAsync(NSUrl nativeUrl) =>
            //UIApplication.SharedApplication.OpenUrlAsync(nativeUrl, new UIApplicationOpenUrlOptions());
            //Task<bool> PlatformOpenAsync(OpenFileRequest request) =>
            // Task.FromResult(NSWorkspace.SharedWorkspace.OpenFile(request.File.FullPath));
            //await Launcher.OpenAsync(siteUri);

            //retrievefiles.DownloadFile(uri, System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "Recibo.pdf"));
            //await Launcher.Default.OpenAsync(new OpenFileRequest("Nomina_"+PERIODO.ToString(), new ReadOnlyFile(uri)));
            //var response = await client.GetStreamAsync(uri);
        }
        public void MostrarContrasena()
        {

        }
        #endregion
        #region Comandos
        //public ICommand Asynccommand => new Command(async () => await Prueba());
        //public ICommand Restablecercommand => new Command(async () => await RestablecerContrasena());
        public ICommand Metodcommand => new Command(MostrarContrasena);
        #endregion
    }
}

