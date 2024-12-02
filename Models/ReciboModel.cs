using System;
using System.Drawing;

namespace RecibosNominaCGA.Models
{
	public class ReciboModel
	{
		public string TIPO { get; set; }
		public int CONCEPTO { get; set; }
		public string DESCRIPCION { get; set; }
		public double CANTIDAD { get; set; }
		public double VALOR { get; set; }


		public ReciboModel()
		{
			TIPO = "";
			CONCEPTO = 0;
			DESCRIPCION = "";
			CANTIDAD = 0;
			VALOR = 0;
			

        }

        public ReciboModel(string _tipo,int _concepto,string _desc,double _cantidad,double _valor)
		{
            this.TIPO = _tipo;
			this.CONCEPTO = _concepto;
			this.DESCRIPCION = _desc;
			this.CANTIDAD = _cantidad;
			this.VALOR = _valor;
		}
	}
}

