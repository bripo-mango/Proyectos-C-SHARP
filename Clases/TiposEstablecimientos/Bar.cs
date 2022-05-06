using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Establecimientos
{
	public class Bar : IEstablecimiento
	{
		public string Cuit { get; set; }
		#nullable enable
		public string? Nombre { get; set; }
		public string? Ubicacion { get; set; }
		public string? Pais { get; set; }
		public int? CantidadEmpleados { get; set; }
		public string? Telefono { get; set; }
		public DateTime? FechaAlta { get; set; }
		public bool? ImpuestosAlDia { get; set; }
		public decimal? ImporteImpuestos { get; set; }
		public bool? CumpleLey24788 { get; set; }
		public int? Capacidad { get; set; }
		public decimal? TasasMunicipales { get; set; }
		public decimal? TotalImpuestos { get => ImporteImpuestos / TasasMunicipales;  set { } }
		public string? MailContacto { get; set; }
		#nullable disable
	}
}
