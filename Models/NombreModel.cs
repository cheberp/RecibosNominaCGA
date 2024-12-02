using System;
namespace RecibosNominaCGA.Models
{
	public class NombreModel
	{
        public string TOKEN { get; set; }
        public string NOMBRE { get; set; }
        public string NOMBRE_CIA { get; set; }
        public int CIA { get; set; }
        public int EMPLEADO { get; set; }
        public string TURNO { get; set; }
        public string HORARIO { get; set; }
        public string SUPERVISOR { get; set; }
        public string CORREO { get; set; }

        public bool REQUIERE_AUT { get; set; }

        public NombreModel(string _token,string _nombre, string _nombrecia, int _cia  , int _empleado,string _turno , string _horario,string _supervisor,string _correo, bool _requiere_aut)
       {
            this.TOKEN = _token;
            this.NOMBRE = _nombre;
            this.NOMBRE_CIA = _nombrecia;
            this.CIA = _cia;
            this.EMPLEADO = _empleado;
            this.TURNO = _turno;
            this.HORARIO = _horario;
            this.SUPERVISOR = _supervisor;
            this.CORREO = _correo;
            this.REQUIERE_AUT = _requiere_aut;
       }
        public NombreModel()
        {
            this.TOKEN = "";
            this.NOMBRE = "";
            this.NOMBRE_CIA = "";
            this.CIA = 0;
            this.EMPLEADO = 0;
            this.TURNO = "";
            this.HORARIO = "";
            this.SUPERVISOR = "";
            this.CORREO = "";
            this.REQUIERE_AUT = false;
        }
    }


}

