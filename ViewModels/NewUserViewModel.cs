using System;
using Newtonsoft.Json;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System.Windows.Input;
using RecibosNominaCGA.VIEWMODELS;
using RecibosNominaCGA.Models;
using System.Text;
using RecibosNominaCGA.Views;
using System.Text.RegularExpressions;

namespace RecibosNominaCGA.ViewModels
{
	public class NewUserViewModel:BaseViewModel
	{
        #region Variables
        string _Usuario;
        string _Nombre;
        string _Correo;
        string _Telefono;
        string _Empresa;
        string _Puesto;
        ContactoModel _contactoModel= new ContactoModel();

        #endregion
        #region Constructor
        public NewUserViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion
        #region Objetos
        public string Usuario
        {
            get { return _Usuario; }
            set { SetValue(ref _Usuario, value); }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { SetValue(ref _Nombre, value); }
        }
        public string Correo
        {
            get { return _Correo; }
            set { SetValue(ref _Correo, value); }
        }
        public string Telefono
        {
            get { return _Telefono; }
            set { SetValue(ref _Telefono, value); }
        }
        public string Empresa
        {
            get { return _Empresa; }
            set { SetValue(ref _Empresa, value); }
        }
        public string Puesto
        {
            get { return _Puesto; }
            set { SetValue(ref _Puesto, value); }
        }
        #endregion
        #region Procesos
        //public async Task Prueba()
        //{

        //}
        public void MostrarContrasena()
        {

        }
        public async Task IrVersionPrueba()
        {
            try
            {
                IsBusy = true;               
                IsEnable = false;                

                Uri RequestUri = new Uri("http://cgyasc2014-001-site6.ctempurl.com/api/UserInvitado");
              //Uri RequestUri = new Uri("http://cgyasc2014-001-site6.ctempurl.com/api/UserInvitado");
               // Uri RequestUri = new Uri("http://localhost:63538//api/usertk");
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(new {TEST=true});
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
                        //Preferences.Set("LOGEADO", true);
                        // Preferences.Set("TOKEN", obj.TOKEN);
                        Preferences.Set("CORREO", obj.CORREO);
                        Preferences.Set("REQUIERE_AUT", obj.REQUIERE_AUT);
                        
                    }
                    await Navigation.PushAsync(new TabbedMenu());
                }
                else
                {
                    string Mensaje = "";
                    var content = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                    if (obj != null)
                        Mensaje = obj["Message"] ?? "";
                    await DisplayAlert("Upps..", Mensaje, "Aceptar");
                    IsEnable = true;
                    IsBusy = false;
                }
            }catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Continuar");
            }
        }
        public async Task SolicitarInformacion()
        {
            try
            {
                IsBusy = true;               
                IsEnable = false;
                _contactoModel=new()
                {
                    NOMBRE = this.Nombre,
                    CORREO = this.Correo,
                    TELEFONO = this.Telefono,
                    CIA = this.Empresa,
                    PUESTO = this.Puesto
                };
                string StrDatos = "";
                if (string.IsNullOrWhiteSpace(this.Nombre))   {StrDatos = StrDatos + "Falta Nombre Completo\n" ;}
                if (string.IsNullOrWhiteSpace(this.Correo))   {  StrDatos += "Falta Correo Electrónico\n"; }
                else if (!IsValidEmail(this.Correo))
                    {
                        StrDatos += "Correo Electrónico no tiene un formato válido\n";
                    }
                   
                if (string.IsNullOrWhiteSpace(this.Telefono)) { StrDatos += "Falta Número Telefónico\n"; }
                if (string.IsNullOrWhiteSpace(this.Empresa)) { StrDatos += "Falta Nombre de Empresa \n"; }
                if (string.IsNullOrWhiteSpace(this.Puesto)) { StrDatos += "Falta Puesto\n"; }
                if(!string.IsNullOrEmpty(StrDatos)) {
                    await DisplayAlert("Validar Informacion", StrDatos, "Aceptar");
                    return;
                }


                Uri RequestUri = new Uri("http://cgyasc2014-001-site6.ctempurl.com/api/enviocontacto");
                // Uri RequestUri = new Uri("http://localhost:63538//api/usertk");
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(_contactoModel);
                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(RequestUri, contentJson);
                if (response.IsSuccessStatusCode)
                {                  
                    var content = await response.Content.ReadAsStringAsync();                   
                    await DisplayAlert("Aviso", "Nos pondremos en contacto con la informacion proporcionada","Aceptar");
                }
                else
                {
                    string Mensaje = "";
                    var content = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                    if (obj != null)
                        Mensaje = obj["Message"] ?? "";
                    await DisplayAlert("Upps..", Mensaje, "Aceptar");
                    IsEnable = true;
                    IsBusy = false;
                }
            }catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Continuar");
            }
        }
        bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }
        #endregion

        
        #region Comandos
        //public ICommand Asynccommand => new Command(async () => await Prueba());
        //public ICommand Restablecercommand => new Command(async () => await RestablecerContrasena());
        public ICommand Metodcommand => new Command(MostrarContrasena);
        public ICommand IrVersionPruebacommand => new Command(async () => await IrVersionPrueba());
        public ICommand SolicitarInformacioncommand => new Command(async () => await SolicitarInformacion());
        
        
        #endregion
    }
}

