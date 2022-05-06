using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using interfaces;

namespace Bebidas
{
	public class Gaseosa : IBebida
	{
		public string Marca { get; set; }
		public double Cantidad { get; set; }
		public double Precio { get; set; }
		public double Impuesto { get; set; }
		public double CantidadAzucar { get; set; }
		public DateTime FechaElaboracion { get; set; }
		public double ValorFinal { get => ValorFinal * Impuesto / CantidadAzucar; set { } }
	}
}
