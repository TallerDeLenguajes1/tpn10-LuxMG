using System;
using System.IO;

namespace Inmobiliaria{
	enum TipoDeOper{Venta, Alquiler};
	enum TipoDeProp{Departamento, Casa, Duplex, Penthouse, Terreno};
	public static Random random = new Random(Environment.TickCount);

	class Propiedad{
		private int id;
		private string tipoDePropiedad;
		private string tipoDeOperacion;
		private float tamanio;
		private int cantBaños;
		private int cantHabitaciones;
		private string domicilio;
		private int precio;
		private bool estado; //activo/inactivo

		public int ID{get => id; set => id = value;}
		public string TipoDePropiedad{get => tipoDePropiedad; set => tipoDePropiedad = value;}
		public string TipoDeOperacion{get => tipoDeOperacion; set => tipoDeOperacion = value;}
		public float Tamanio{get => tamanio; set => tamanio = value;}
		public int CantBaños{get => cantBaños; set => cantBaños = value;}
		public int CantHabitaciones{get => cantHabitaciones; set => cantHabitaciones = value;}
		public string Domicilio{get => domicilio; set => domicilio = value;}
		public int Precio{get => precio; set => precio = value;}
		public bool Estado{get => estado; set => estado = value;}

		public Propiedad(int id, string tp, string to, float tam, int cb, int ch, string dom, int pr, bool est){
			ID = id;
			TipoDePropiedad = tp;
			TipoDeOperacion = to;
			Tamanio = tam;
			CantBaños = cb;
			CantHabitaciones = ch;
			Domicilio = dom;
			Precio = pr;
			Estado = est;
		}

		public double ValorDelInmueble(){
			double valor = 0;

			if(tipoDeOperacion == TipoDeOper.Venta.ToString()){
				double IVA = precio*0.21;
				int costoTransf = 10000;
				valor = precio + IVA;
				double ingBrutos = valor*0.1;

				valor += ingBrutos + costoTransf;
			}else{//tipoDeOperacion == TipoDeOper.Alquiler
				valor = precio*0.02;
				double extras = valor*0.005;

				valor += extras;
			}

			return valor;
		}

		public void CrearPropiedad(string dom, string to){
			
		}
	}

	class ArchivoCSV{
		public void Cargar(List<Propiedad> Propiedades){
			string linea = "";
			using(StreamWriter sw = new StreamWriter("Propiedades.csv")){
				foreach(Propiedad prop in Propiedades){
					linea = prop.ID.ToString() + ";" + prop.TipoDePropiedad + ";" + prop.TipoDeOperacion + ";" + prop.Tamanio.ToString() + ";" + prop.CantBaños.ToString() + ";" + prop.CantHabitaciones.ToString() + ";" + prop.Domicilio + ";" + prop.Precio.ToString() + ";" + prop.Estado.ToString();
					sw.WriteLine(linea);
				}
			}
		}

		public Lista<Propiedad> Leer(){
			string linea = "";
			string[] p;
			Propiedad prop;
			List<Propiedad> Propiedades = new List<Propiedad>();
			using(StreamReader sr = new StreamReader("PropiedadesBase.csv")){
				while((linea = sr.ReadLine()) != null){
					p = linea.Split(";");
					prop = Propiedad.CrearPropiedad(p[0], p[1]);//con domicilio y tipo de propiedad
					Propiedades.Add(prop);
				}
			}
			return Propiedades;
		}
	}
}