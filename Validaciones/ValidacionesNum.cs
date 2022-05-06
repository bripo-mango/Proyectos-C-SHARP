using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Validaciones
{
	public class ValidacionesNumeros
	{
		//Valida que el valor sea mayor a 0
		public static readonly Predicate<double> ValorPositivo =
			(d) => d > 0;

		//Valida que el valor sea menor a 0
		public static readonly Predicate<double> ValorNegativo =
			(d) => d < 0;

		//Valida que el valor se encuentre entre un rango especifico
		public static readonly Predicate<double> ValorRangoDouble =
			(d) => d > 0 && d < 1000;

		public static readonly Predicate<int> ValorRangoInt =
			(d) => d > 0 && d < 7;
	}

	public class ValidarNumeros
	{
		public static readonly Predicate<int>[] validacionesNumInt =
		{
			(d) => ValidacionesNumeros.ValorRangoInt(d)
		};

		public static readonly Predicate<int> cumplenCondicionNum = e => Validador.Validar(e, validacionesNumInt);
		public static readonly Predicate<int> noCumplenCondicionNum = e => !Validador.Validar(e, validacionesNumInt);
	}
}
