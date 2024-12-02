using System.Windows.Input;
using RecibosNominaCGA.Models;
using Newtonsoft.Json;
using System.Text;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using RecibosNominaCGA.Views;
using RecibosNominaCGA.Helpers;
using RecibosNominaCGA.ViewModels;

namespace RecibosNominaCGA.VIEWMODELS
{
	public class LoginViewModel:BaseViewModel
	{
        #region Variables
        string _Usuario;
        string _Password;
        string _ocultarContrasena;
        string _marca;
        string _modelo;
        public NombreModel Nombre = new NombreModel();
        #endregion
        #region Constructor
        public LoginViewModel(INavigation navigation)
        {
            Navigation = navigation;
            IsPassword = true;
            OcultarContrasena = "Ocultar.png";
            Modelo = DeviceInfo.Model;
            Marca = DeviceInfo.Manufacturer;
            //NombreDispositivo= DeviceInfo.Name;
            string tipoDispositivo = DeviceInfo.DeviceType.ToString();

            if (Preferences.ContainsKey("LOGEADO") && Preferences.Get("LOGEADO", false))
            {
                Password = Preferences.Get("PASSWORD", string.Empty);
                Usuario = Preferences.Get("USUARIO", string.Empty);
                IsEnable = false;
                 Task lg = Login();
            }
            else
            {
                if (tipoDispositivo != "Physical")
                {
                    IsEnable = true;
                    /*
                    Task lg = Login();
                    */
                    Task ale = DisplayAlert("Atencion", "Dispositivo Virtual no olvide cerrar su sesion al finalizar", "ok");
                }
                else
                {
                    if (Preferences.ContainsKey("USUARIO") && Preferences.ContainsKey("PASSWORD"))
                    {
                        IsEnable = false;
                        //IsBusy = true;
                        Task  lgn= Login();
                    }
                }
            }
            IsBusy = false;
            //IsEnable = true;
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
        public string OcultarContrasena
        {
            get { return _ocultarContrasena; }
            set { SetValue(ref _ocultarContrasena, value); }
        }
        public string Marca
        {
            get { return _marca; }
            set { SetValue(ref _marca, value); }
        }
        public string Modelo
        {
            get { return _modelo; }
            set { SetValue(ref _modelo, value); }
        }
        #endregion
        #region Procesos
        public async Task Login()
        {
            if (VersionTracking.Default.IsFirstLaunchForVersion(VersionTracking.Default.CurrentVersion) && VersionTracking.Default.PreviousVersion != null && VersionTracking.Default.CurrentVersion != VersionTracking.Default.PreviousVersion)
            {
                await ActualizarTK();
            }
            string _token = Preferences.Get("TOKEN", string.Empty);
            string Mensaje = "";
            LoginModel login = new LoginModel(Usuario, Password, Modelo, Marca, _token, Settings.ObtenerAplicacion());
            try
            {
                IsBusy = true;               IsEnable = false;
                Uri RequestUri = new Uri("http://cgyasc2014-001-site6.ctempurl.com/api/usertk");
                // Uri RequestUri = new Uri("http://localhost:63538//api/usertk");
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(login);
                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(RequestUri, contentJson);
                if (response.IsSuccessStatusCode)
                {
                    //string content = await response.Content.ReadAsStreamAsync();
                    var content = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<NombreModel>(content);
                    if (obj != null)
                    {
                        Preferences.Set("Nombre", obj.NOMBRE);
                        Preferences.Set("Nombre_CIA", obj.NOMBRE_CIA);
                        Preferences.Set("CIA", obj.CIA);
                        Preferences.Set("EMPLEADO", obj.EMPLEADO);
                        Preferences.Set("Turno", obj.TURNO);
                        Preferences.Set("Horario", obj.HORARIO);
                        Preferences.Set("Supervisor", obj.SUPERVISOR);
                        Preferences.Set("LOGEADO", true);
                        Preferences.Set("TOKEN", obj.TOKEN);
                        Preferences.Set("CORREO", obj.CORREO);
                        Preferences.Set("REQUIERE_AUT", obj.REQUIERE_AUT);
                        //Preferences.Set("PASSWORD", Password);
                        //Preferences.Set("USUARIO", Usuario);
                        VerificarEstadoHuella();
                        //Preferences.Set("Nombre_CIA", obj.TOKEN);
                        //IsBusy = false;
                        //IsEnable = true;
                    }
                }
                else
                {

                    var content = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                    if (obj != null)
                        Mensaje = obj["Message"] ?? "";
                    await DisplayAlert("Upps..", Mensaje, "Aceptar");
                    IsEnable = true;
                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Continuar");
            }
        }
        public async Task NewLogin()
        {
            try
            {
                IsBusy = true;               
                IsEnable = false;
                await Navigation.PushAsync( new NavigationPage(new NewUserPage()));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Continuar");
            }
        }
        // public async Task Login()
        // {

        //     string _token = Preferences.Get("TOKEN", string.Empty);
        //     if (Preferences.ContainsKey("USUARIO") && Preferences.ContainsKey("PASSWORD"))
        //     {
        //         Usuario = Preferences.Get("USUARIO", string.Empty);
        //         Password = Preferences.Get("PASSWORD", string.Empty);
        //     }

        //     LoginModel login = new LoginModel(Usuario, Password, Modelo, Marca,_token,"AppReciboElectronico");
        //     try
        //     {
        //         IsBusy = true;
        //         IsEnable = false;
        //         Uri RequestUri = new Uri("http://cgyasc2014-001-site6.ctempurl.com/api/usertk");
        //         HttpClient client = new HttpClient();
        //         var json = JsonConvert.SerializeObject(login);
        //         var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
        //         var response = await client.PostAsync(RequestUri, contentJson);
        //         if (response.IsSuccessStatusCode)
        //         {
        //             //string content = await response.Content.ReadAsStreamAsync();
        //             var content = await response.Content.ReadAsStringAsync();
        //             var obj = JsonConvert.DeserializeObject<NombreModel>(content);
        //             Preferences.Set("Nombre", obj.NOMBRE);
        //             Preferences.Set("Nombre_CIA", obj.NOMBRE_CIA);
        //             Preferences.Set("CIA", obj.CIA);
        //             Preferences.Set("EMPLEADO", obj.EMPLEADO);
        //             Preferences.Set("Turno", obj.TURNO);
        //             Preferences.Set("Horario", obj.HORARIO);
        //             Preferences.Set("Supervisor", obj.SUPERVISOR);
        //             Preferences.Set("LOGEADO", false);
        //             Preferences.Set("TOKEN", obj.TOKEN);
        //             Preferences.Set("CORREO", obj.CORREO);
        //             VerificarEstadoHuella();
        //             //Preferences.Set("Nombre_CIA", obj.TOKEN);
        //             //IsBusy = false;
        //             //IsEnable = true;
        //         }
        //         else
        //         {
        //             //IsBusy = false;
        //             //IsEnable = true;
                    
        //             await DisplayAlert("Upps..", "El usuario o la contraseña son incorrectos, Vuelve a intentarlo!", "Aceptar");
        //             //await DisplayAlert("Upps..", response.RequestMessage.ToString(), "Aceptar");
        //             IsEnable = true;
        //             IsBusy = false;
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         //IsBusy = false;
        //         //IsEnable = true;
        //         await DisplayAlert("Error", ex.Message, "Continuar");
        //     }
        // }

    public async Task ActualizarTK()
        {
            int cia = Preferences.Get("CIA", 0);
            string tk = Preferences.Get("TOKEN", string.Empty);
            LoginModel aToken = new LoginModel(Usuario, cia, tk);
            Uri RequestUri = new Uri("http://cgyasc2014-001-site6.ctempurl.com/api/ActualizacionToken/");
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(aToken);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(RequestUri, contentJson);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Información", "Se ha actualizado el Token correctamente", "ok");
                Preferences.Remove("TOKEN");
                string tktemp = content.ToString().Substring(1, content.Length - 2);
                Preferences.Set("TOKEN", tktemp);

            }
            else
            {
                await DisplayAlert("Error", "No se pudo actualizar correctamente el Token del dispositivo, solicite ayuda para desvincular el dispositivo", "ok");
            }
        }
        //public async Task RestablecerContrasena()
        //{
        //    //await Navigation.PushAsync(new RestablecerContraseña());
        //}
        public void MostrarContrasena()
        {
            if (IsPassword)
            {
                IsPassword = false;
                OcultarContrasena = "Ver.png";
            }
            else
            {
                IsPassword = true;
                OcultarContrasena = "Ocultar.png";
            }
        }
        public async void VerificarEstadoHuella()
        {
            bool isAva = await CrossFingerprint.Current.IsAvailableAsync(true);
            if (!isAva)
            {
                //await DisplayAlert("alert", "No se encontró autenticación por huella", "ok");
                VerificarDispositivo();
            }
            else
            {
                AuthenticationRequestConfiguration _conf = new AuthenticationRequestConfiguration("Autenticación", "Ingrese su huella");
                var res = await CrossFingerprint.Current.AuthenticateAsync(_conf);
                if (res.Authenticated)
                {
                    //sucess!!!!!
                    //Preferences.Set("USUARIO", Usuario);
                    //Preferences.Set("PASSWORD", Password);
                    VerificarDispositivo();
                    //await DisplayAlert("Exito", "Huella detectada exitosamente", "Ok");
                    //Navigation.PushAsync(new Principal());
                }
                else
                {
                    //wrong
                    //await DisplayAlert("Error", "Huella no reconocida", "Ok");
                    VerificarDispositivo();
                    IsBusy = false;
                    //resultado = false;
                }
            }

        }
public async void VerificarDispositivo()
        {
            try
            {

                var client = new HttpClient();

                string uri = "http://cgyasc2014-001-site6.ctempurl.com/api/user?usuario=" + Usuario + "&MARCA=" + Marca + "&MODELO=" + Modelo/*+"&NOMBREDISPOSITVO="+NombreDispositivo*/;
                var response = await client.GetStringAsync(uri);
                var resultado = JsonConvert.DeserializeObject<bool>(response);
                if (resultado)
                {

                    Preferences.Set("USUARIO", Usuario);
                    Preferences.Set("PASSWORD", Password);
                    //Pagina a la cual contenga el menu flyout o tabbed
                    await Navigation.PushAsync(new TabbedMenu());
                    //await Navigation.PushAsync(new NavigationPage(new PrincipalPage()));
                }
                else
                {
                    await DisplayAlert("Error", "El usuario no tiene registrado este dispositivo", "Ok");
                }
                IsEnable = true;
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsEnable = true;
                IsBusy = false;
                await DisplayAlert("Error:", ex.ToString(), "OK");
            }
        }
        // public async void VerificarDispositivo()
        // {
        //     try
        //     {
        //         if (Preferences.ContainsKey("USUARIO") && Preferences.ContainsKey("PASSWORD"))
        //         {
        //             Usuario = Preferences.Get("USUARIO", string.Empty);
        //             Password = Preferences.Get("PASSWORD", string.Empty);
        //         }
        //         var client = new HttpClient();
        //         //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //         string uri = "http://cgyasc2014-001-site6.ctempurl.com/api/user?usuario=" + Usuario + "&MARCA=" + Marca + "&MODELO=" + Modelo/*+"&NOMBREDISPOSITVO="+NombreDispositivo*/;
        //         var response = await client.GetStringAsync(uri);
        //         var resultado = JsonConvert.DeserializeObject<bool>(response);
        //         if (resultado)
        //         {
        //             //await Navigation.PushAsync(new AppShell());
        //             //await Shell.Current.GoToAsync("//Inicio");
        //             await Navigation.PushAsync(new TabbedMenu());
        //             Preferences.Set("USUARIO", Usuario);
        //             Preferences.Set("PASSWORD", Password);
        //         }
        //         else
        //         {
        //             await DisplayAlert("Error", "El usuario no tiene registrado este dispositivo", "Ok");
        //         }
        //         IsEnable = true;
        //         IsBusy = false;
        //     }
        //     catch (Exception ex)
        //     {
        //         IsEnable = true;
        //         IsBusy = false;
        //         await DisplayAlert("Error:", ex.ToString(), "OK");
        //     }
        // }
        #endregion
        #region Comandos
        public ICommand Logincommand => new Command(async () => await Login());
        public ICommand NewUsercommand => new Command(async () => await NewLogin());
        //public ICommand Restablecercommand => new Command(async () => await RestablecerContrasena());
        public ICommand Vercontrasenacommand => new Command(MostrarContrasena);
        #endregion
    }
}

