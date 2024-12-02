using System;
namespace RecibosNominaCGA.Models
{
	public class ListaPeriodosModel
	{
		public int PERIODO { get; set; }
		public string DESCRIPCION { get; set; }
		public string MES { get; set; }
		public DateTime TAFECHAI { get; set; }
		public DateTime TAFECHAF { get; set; }
		public string TIPONOMINA { get; set; }
        public Color COLOR { get; set; }

        public ListaPeriodosModel(int _periodo,string _descripcion,string _mes,DateTime _tafechai,DateTime _tafechaf,string _tiponomina, string _color)
		{
			this.PERIODO = _periodo;
			this.DESCRIPCION = _descripcion;
			this.MES = _mes;
			this.TAFECHAI = _tafechai;
			this.TAFECHAF = _tafechaf;
			this.TIPONOMINA = _tiponomina;
			this.COLOR = Color.Parse(_color);
        }

		public ListaPeriodosModel()
		{
			PERIODO = 0;
			DESCRIPCION = "";
			MES = "";
			TAFECHAI = DateTime.Now;
			TAFECHAF = DateTime.Now;
			TIPONOMINA = "";
			COLOR = Color.FromRgb(0,0,0);
        }
	}
}

