using System.Windows.Input;

namespace RecibosNominaCGA.VIEWMODELS
{
	public class InicioViewModel:BaseViewModel
	{
        #region Variables
        int _Empleado;
        string _Nombre;
        string _Empresa;
        int _Cia;
        string _Turno;
        string _Horario;
        string _Supervisor;
        string _FotoEmpleado;
        string _LogoEmpresa;

        #endregion
        #region Constructor
        public InicioViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Title = "Inicio";
            Nombre = Preferences.Get("Nombre", string.Empty);
            Empresa = Preferences.Get("Nombre_CIA", string.Empty);
            _Cia = Preferences.Get("CIA", 0);
            Empleado = Preferences.Get("EMPLEADO", 0);
            Turno = Preferences.Get("Turno", string.Empty);
            Horario = Preferences.Get("Horario", string.Empty);
            Supervisor = Preferences.Get("Supervisor", string.Empty);
            FotoEmpleado = "http://cgyasc2014-001-site1.ctempurl.com/cia" + _Cia + "/fotos/" + _Empleado + ".jpg";
            LogoEmpresa = "http://cgyasc2014-001-site1.ctempurl.com/cia" + _Cia + "/logo.jpg";
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
        public string Empresa
        {
            get { return _Empresa; }
            set { SetValue(ref _Empresa, value); }
        }
        public string Turno
        {
            get { return _Turno; }
            set { SetValue(ref _Turno, value); }
        }

        public string Horario
        {
            get { return _Horario; }
            set { SetValue(ref _Horario, value); }
        }
        public string Supervisor
        {
            get { return _Supervisor; }
            set { SetValue(ref _Supervisor, value); }
        }
        public string FotoEmpleado
        {
            get { return _FotoEmpleado; }
            set { SetValue(ref _FotoEmpleado, value); }
        }
        public string LogoEmpresa
        {
            get { return _LogoEmpresa; }
            set { SetValue(ref _LogoEmpresa, value); }
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

