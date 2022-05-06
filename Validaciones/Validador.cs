using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validaciones
{
	public class Validador
	{
		/// <summary>
		/// Permite validar si un objeto cumple determinada condicion
		/// </summary>
		/// <param name="cerveza"></param> objeto a validar
		/// <param name="condicion"></param> Coleccion de predicados del tipo cerveza, incluye las validaciones a hacer
		/// <returns>Devuelve true o false</returns>
		public static bool Validar<T>(T objeto, params Predicate<T>[] condicion) =>
		condicion.ToList().Where(d =>
		{
			return !d(objeto);

		}).Count() == 0;    //Si se cumple alguna condicion retorna false (numero mayor a 0), en caso de que no se cumpla ninguna retorna true (0)
	}
}
