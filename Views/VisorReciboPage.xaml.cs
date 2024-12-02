namespace RecibosNominaCGA.Views;
[XamlCompilation(XamlCompilationOptions.Compile)]


public partial class VisorReciboPage : ContentPage
{
    public System.IO.Stream InputStream { get; }
    public VisorReciboPage(string Periodo)
	{
		InitializeComponent();
        int cia = Preferences.Get("CIA", 0);
        int empleado = Preferences.Get("EMPLEADO", 0);
        var client = new HttpClient();
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        string uri = "http://cgyasc2014-001-site6.ctempurl.com/api/rec?CIA=" + cia.ToString() + "&EMPLEADO=" + empleado.ToString() + "&PERIODO=" + Periodo;

        string source = uri;
        this.wv.Source = source;
        //Launcher.Default.OpenAsync(new OpenFileRequest("Nomina_" + Periodo, new ReadOnlyFile(uri,)));
        Launcher.OpenAsync(uri);
        wv.Reload();

        //BinaryReader b = new BinaryReader(file.InputStream);
        //byte[] binData = b.ReadBytes(file.ContentLength);

        //string result = System.Text.Encoding.UTF8.GetString(binData);
        //BindingContext = new VisorReciboViewModel(Navigation, Periodo);
    }
}
