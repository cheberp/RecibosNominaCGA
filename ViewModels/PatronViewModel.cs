using System;
//using AndroidX.CustomView.Widget;
//using IntelliJ.Lang.Annotations;
using Newtonsoft.Json;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System.Windows.Input;
using RecibosNominaCGA.VIEWMODELS;

namespace RecibosNominaCGA.ViewModels
{
	public class PatronViewModel:BaseViewModel
	{
        #region Variables
        string _Usuario;
        string _Password;


        #endregion
        #region Constructor
        public PatronViewModel(INavigation navigation)
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
        public string Password
        {
            get { return _Password; }
            set { SetValue(ref _Password, value); }
        }
        #endregion
        #region Procesos
        //public async Task Prueba()
        //{

        //}
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

