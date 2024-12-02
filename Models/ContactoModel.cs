using System;
namespace RecibosNominaCGA.Models
{
    public class ContactoModel
    {
        public string NOMBRE { get; set; }
        public string CORREO { get; set; }
        public string TELEFONO { get; set; }
        public string CIA { get; set; }
        public string PUESTO { get; set; }


        //public int CSERVICIO { get; set; }

        public ContactoModel()
        {
            NOMBRE = "";
            CORREO = "";
            TELEFONO = "";
            CIA = "";
            PUESTO = "";
        }
        /*
        public Contacto(int _id, string _desc)
        {
            this.ID = _id;
            this.DESCRIPCION = _desc;
        }*/
    }
}