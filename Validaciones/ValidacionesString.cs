using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Validaciones
{
	public static class ValidacionesString
	{
		//Valida si el valor de la cadena es NULL
		public static readonly Predicate<string> NotNull =
			(d) => d != null;

		//Valida que la cadena no este vacia
		public static readonly Predicate<string> CadenaNoVacia =
			(d) => !Regex.IsMatch(d, @"^$");

		//Valida que un string sea un numero decimal
		public static readonly Predicate<string> NumeroDecimal =
			(d) => Regex.IsMatch(d, @"^\d+\.\d+$");

		//Valida el tamaño de la cadena
		public static readonly Predicate<string> TamanioCadena =
			(d) => d.Length > 5;

		//Valida solo caracteres alfabeticos (Mayusculas y Minusculas)
		public static readonly Predicate<string> SoloLetras =
			(d) => Regex.IsMatch(d, @"^[A-Za-z]+$");

		//Valida si el string tiene el primer caracter con mayuscula
		public static readonly Predicate<string> SoloLetrasPrimeraMayus =
			(d) => Regex.IsMatch(d, @"^[A-Z][a-z]+$");

		//Valida la cantidad de letras que contiene la cadena
		public static readonly Predicate<string> CantidadLetras =
			(d) => Regex.IsMatch(d, @"^\w{8}$");

		//Valida que la cadena contenga solo letras y numeros
		public static readonly Predicate<string> LetrasNumeros =
			(d) => Regex.IsMatch(d, @"([A-Za-z0-9]+) ?([A-Za-z0-9])$");

		//Valida solo caracteres numericos
		public static readonly Predicate<string> SoloNumeros =
			(d) => Regex.IsMatch(d, @"^[0-9]+$");

		//Valida que tenga solo 8 digitos numericos
		public static readonly Predicate<string> CantidadNumeros =
			(d) => Regex.IsMatch(d, @"^\d{8}$");

		//Valida patente
		public static readonly Predicate<string> CantidadNumerosLetras =
			(d) => Regex.IsMatch(d, @"^([A-Za-z0-9]{3}) [A-Za-z0-9]{3}$");

		//Valida caracteres alfabeticos en mayuscula y numeros
		public static readonly Predicate<string> LetrasMayusNum =
			(d) => Regex.IsMatch(d, @"^[A-Z0-9]+$");

		//Valida caracteres alfabeticos en mayuscula, numeros separados por espacio
		public static readonly Predicate<string> LetrasMayusNumEspacio =
			(d) => Regex.IsMatch(d, @"([A-Z0-9]+) ([A-Z0-9]+)$");

		//Valida caracteres alfabeticos (sin discriminar formato mayuscula o minuscula) separados por espacio
		public static readonly Predicate<string> NombreApellido =
			(d) => Regex.IsMatch(d, @"^([A-Za-z]+) ?([A-Za-z]+)$");

		//Valida que el primer caracter de la cadena antes y despues de un espacio sea mayuscula
		public static readonly Predicate<string> FormatoNombreApellido =
			(d) => Regex.IsMatch(d, @"^([A-Z][a-z]+) ([A-Z][a-z]+)$");

		//Valida formato de correo electronico
		public static readonly Predicate<string> CorreoFormato =
			(d) => Regex.IsMatch(d, @"^(.*[A-Za-z0-9]+)@([A-Za-z0-9]+\.)(\w{2,3})$");

		//Valida cadena con formato de numero de documento
		public static readonly Predicate<string> Documento =
			(d) => Regex.IsMatch(d, @"^\d{8}$");

		//Valida un numero de Cuit
		public static readonly Predicate<string> Cuit =
			(d) => Regex.IsMatch(d, @"^\d{2}-\d{8}-?[0-9]?[0-9]$");

		//Valida numero de telefono
		public static readonly Predicate<string> TelefonoFijo =
			(d) => Regex.IsMatch(d, @"^?\d{3} \d{8}$");

		//Valida numero de celular
		//public static readonly Predicate<string> TelefonoMovil =
		//	(d) => Regex.IsMatch(d, @"^\d{}$");

		public static readonly Predicate<string> FormatoFecha =
			(d) => Regex.IsMatch(d, @"[0-1]?[0-9][/\- |][0-9]?[0-2][/\- |][0-9]{4}");

		public static bool ConfirmarOperacion(string ingreso)
		{
			return new string[] { "SI", "Si", "S", "si", "s" }.Any(c => ingreso.Contains(c));
		}
	}
	public static class ValidarStrings
	{
		public static readonly Predicate<string>[] validacionesParaStrings =
		{
			//(d) => ValidacionesString.NotNull(d),
			//(d) => ValidacionesString.CadenaNoVacia(d),
			//(d) => ValidacionesString.SoloLetrasPrimeraMayus(d)
			(d) => ValidacionesString.SoloNumeros(d)		
		};

		public static readonly Predicate<string> cumplenCondicionString = e => Validador.Validar(e, validacionesParaStrings);
		public static readonly Predicate<string> noCumplenCondicionString = e => !Validador.Validar(e, validacionesParaStrings);
	}
}
