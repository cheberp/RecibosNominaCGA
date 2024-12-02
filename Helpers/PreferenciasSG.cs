using System;
using RecibosNominaCGA.Models;

namespace RecibosNominaCGA.Helpers
{
	public class PreferenciasSG
	{
		private static PreferenciasSG _instance { get; set; } = new PreferenciasSG();
		private static NombreModel Config { get; set; }= new NombreModel();

		private PreferenciasSG(NombreModel _nm)
		{
			if (Config !=null){
			Config.TOKEN = _nm.TOKEN??"";
			Config.NOMBRE = _nm.TOKEN??"";
			Config.NOMBRE_CIA = _nm.NOMBRE_CIA;
			Config.CIA = _nm.CIA;
			Config.EMPLEADO = _nm.EMPLEADO;
			Config.TURNO = _nm.TURNO;
			Config.HORARIO = _nm.HORARIO??"";
			Config.SUPERVISOR = _nm.SUPERVISOR??"";
			Config.CORREO = _nm.CORREO??"";
			}
        }
		private PreferenciasSG()
		{
			if (Config !=null){
			Config.TOKEN = "";
			Config.NOMBRE = "";
			Config.NOMBRE_CIA = "";
			Config.CIA = 0;
			Config.EMPLEADO =0 ;
			Config.TURNO = "";
			Config.HORARIO = "";
			Config.SUPERVISOR = "";
			Config.CORREO = "";
			}
        }


		public static PreferenciasSG GetPreferencias(NombreModel _nm)
		{
            if (_instance == null)
            {
                _instance = new PreferenciasSG(_nm);
            }
            return _instance;
        }
		public static bool Logeado()
		{
			return true;
		}
	}
}

