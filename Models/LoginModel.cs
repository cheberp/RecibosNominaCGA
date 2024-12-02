using System;
namespace RecibosNominaCGA.Models
{
	public class LoginModel
	{
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string NvoPassword { get; set; }
        public int CIA { get; set; }
        public string MODELO { get; set; }
        public string MARCA { get; set; }
        public string TOKEN { get; set; }
        public string APP { get; set; }
        //public string NOMBREDISPOSITIVO { get; set; }
        public LoginModel(string usuario, string nvoPassword, int cia)
        {
            Usuario = usuario;
            NvoPassword = nvoPassword;
            CIA = cia;
        }
         public LoginModel(string usuario, int cia, string token)
        {
            Usuario = usuario??"";
            TOKEN = token??"";
            CIA = cia;
        }
        public LoginModel(string _usuario, string _password, string _modelo, string _marca, string _token,string _app)
        {
            Usuario = _usuario;
            Password = _password;
            MODELO = _modelo;
            MARCA = _marca;
            TOKEN = _token;
            APP = _app;
        }
    }
}

