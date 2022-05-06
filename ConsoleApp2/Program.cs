using System;
using System.Collections.Generic;
using ConexionSQL;
using Establecimientos;
using MenuCrud;
using Validaciones;

namespace ConsoleApp2
{
	class Program
	{
		static void Main(string[] args)
		{
			#region Ejemplos pasados

			#region coment
			////List<Bar> bares;

			////bares = new List<Bar>()
			////{
			////	new Bar("Birra Bar")
			////	{
			////		Cervezas = new List<Cerveza>()
			////		{
			////			new Cerveza(){Marca = "Corona", Cantidad = 2.5, FechaElaboracion = DateTime.UtcNow },
			////			new Cerveza(){Marca = "Quilmes Black", Cantidad = 3, FechaElaboracion = DateTime.Now },
			////			new Cerveza(){Marca = "Palermo", Cantidad = 9, FechaElaboracion = DateTime.Now }
			////		}
			////	},
			////	new Bar("Lovers")
			////	{
			////		Cervezas = new List<Cerveza>()
			////		{
			////			new Cerveza(){Marca = "Stella", Cantidad = 1, FechaElaboracion = DateTime.Now },
			////			new Cerveza(){Marca = "Brahma", Cantidad = 2, FechaElaboracion = DateTime.UtcNow },
			////			new Cerveza(){Marca = "Quilmes", Cantidad = 1.5, FechaElaboracion = DateTime.Now }
			////		}
			////	}
			////};

			//////var cervezasOrdenadas = from d in cervezas
			//////						where d.Cantidad >= 1.5
			//////						orderby d.Marca
			//////						select d;

			//////foreach (var d in cervezasOrdenadas)
			//////{
			//////	Console.WriteLine($" Marca = {d.Marca}\n Cantidad = {d.Cantidad}L\n Fecha de Elaboracion = {d.FechaElaboracion}\n\n ");
			//////}


			//////var nombreBares = (from d in bares
			//////				  where d.Cervezas.Where(c => c.Marca == "Stella").Count() > 0
			//////				  select d).ToList();


			//List<Cerveza> pruebasCerveza = new List<Cerveza>()
			//{
			//	new Cerveza(){Marca = "Pepito", Cantidad = 5, FechaElaboracion = DateTime.Now },
			//	new Cerveza(){Marca = "Budweiser", Cantidad = 7, FechaElaboracion = DateTime.Now },
			//	new Cerveza(){Marca = "Chela", Cantidad = 9, FechaElaboracion = DateTime.Now }

			//};


			////List<Cerveza> nuevasCervezas = new List<Cerveza>()
			////{
			////	new Cerveza(){Marca = "pepe", Cantidad = 5, FechaElaboracion = DateTime.Now },
			////	new Cerveza(){Marca = "Pepito", Cantidad = 7, FechaElaboracion = DateTime.Now },
			////	new Cerveza(){Marca = "Chela", Cantidad = 9, FechaElaboracion = DateTime.Now }

			////};

			//////var e = (from d in pruebasCerveza
			//////		 where d.Cantidad > 1
			//////		 select d).ToList();


			////var cervezasIguales = pruebasCerveza.Where(s => nuevasCervezas.Any(d => s.Marca == d.Marca)).ToList();
			////cervezasIguales.Sort((x, y) => y.Cantidad.CompareTo(x.Cantidad));

			////cervezasIguales.ForEach(n => Console.WriteLine($"Marca = {n.Marca}\nCantidad = {n.Cantidad}L\nFecha = {n.FechaElaboracion}\n"));

			////Console.ReadLine();

			//string pepito = "Pepito";

			//Dictionary<Guid, string> cervezas = new Dictionary<Guid, string>();

			//cervezas.Add(Guid.NewGuid(), "Pepito");
			//cervezas.Add(Guid.NewGuid(), "Quilmes");
			//cervezas.Add(Guid.NewGuid(), "Chela");

			//foreach (var d in cervezas)
			//{
			//	if (pepito == d.Value)
			//	{
			//		var e = d.Key;

			//		throw new StackOverflowException();
			//	}
			//}
			#endregion
			//	var predicado = new Predicate<string>(Verificar);

			//	if (predicado("hola"))
			//	{
			//		Console.WriteLine("1");
			//	}
			//	else
			//	{
			//		Console.WriteLine("2");
			//	}
			//}

			//static bool Verificar(string aux)
			//{
			//	if (aux.Contains("h") || aux.Contains("a") || aux.Contains("i"))
			//	{
			//		return true;
			//	}
			//	return false;
			//}

			#region Regex

			//Regex validarnombre = new Regex(@"^([A-Z][a-z]+) ([A-Z][a-z]+)$");  //Valida que la primer letra del nombre tenga mayuscula, espacio y el apellido tambien contenga mayuscula
			//Regex validaDocumento = new Regex(@"^\d{8}$");  //Valida que el documento sea solo numeros con 8 digitos
			//Regex validartelefonoFijo = new Regex(@"^([3-4])(\d{7})$"); //Valida que el telefono contenga 8 digitos
			//Regex validarcelular = new Regex(@"^\d[10-11]\d{8}$");      //Valida que el celular tenga 10 digitos incluyendo una validacion del codigo de area (``)
			//Regex validarpatente = new Regex(@"^([A-Z0-9]{3})-([A-Z0-9]{3})$"); //Valida una patente teniendo 3 caracteres(letras o numeros) un - y otra vez 3 caracteres sean numeros o letras
			//Regex validarCorreo = new Regex(@"^(.*[a-zA-Z0-9]+)[@]([a-zA-Z0-9]+\.)(\w{2,3})$"); //Valida un correo electronico
			//Regex validarFormateNombre = new Regex(@"^[A-Z]$");
			//string numero = "1157949044";
			//string fijo = "42223384";
			//string nombre = "Juan Lopez";
			//string patenteEjem = "OTZ-908";
			//string correo = "Ana_L@@ura@hotmail.com";

			//if (validarCorreo.IsMatch(correo))
			//{
			//	Console.WriteLine("Si");
			//}
			//else
			//{
			//	Console.WriteLine("No");
			//}

			#endregion


			//Cerveza unaCerveza = new Cerveza() { Marca = "Quilmes Brown", Cantidad = 3, FechaElaboracion = DateTime.Now };
			//Console.WriteLine(Validador.Validar(unaCerveza, ValidarCervezas.validaciones));

			//List<string> nombres = new List<string>() { "Pepe", "2", "@", "", "lalo" };
			//List<double> numeros = new List<double>() {1.4, -2.5, 0, 5.40, 100 };
			//List<string> nombresValidados = new List<string>();
			//List<string> nombreasInvalidos = new List<string>();

			//string prueba = "juan lopez";

			//var e = Validaciones.ValidacionesString.Cuit(Console.Read().ToString());

			//var e = nombres.FindAll(ValidarStrings.cumplenCondicionString).ToList();
			//e.ForEach(e => Console.WriteLine("\n\n" + e));

			////var h = nombres.FindAll(ValidarStrings.noCumplenCondicionString).ToList();
			////h.ForEach(h => Console.WriteLine("\n\n"+h));

			//var n = numeros.FindAll(ValidarNumeros.cumplenCondicionNum).ToList();
			//n.ForEach(n => Console.WriteLine("\n\n" + n));


			//Console.WriteLine(Validador.Validar(prueba, ValidarStrings.validaciones));

			//var func = Validador.Validar(prueba, ValidarStrings.validaciones) ?
			//Success :
			//(Action)Error;

			//func();

			//static void Success() => Console.WriteLine("Valido");
			//static void Error() => Console.WriteLine("No valido");
			//}

			//List<int> ar = new List<int>() { 1, 4, 6, 7, 9 };

			//var p = ar.Aggregate(0, (acc, item) =>
			//{
			//	return acc + item;
			//});

			#endregion

			//Action<int, int> MostrarNum = (d, e) => Console.WriteLine(d * e);
			//Action<List<int>> MostrarList = (d) => d.ForEach(Console.WriteLine);

			//MostrarNum(2, 2);
			//MostrarList(new List<int> {1,2,3});

			List<Restaurant> mostrarRestos = new List<Restaurant>();
			string ingreso;
			int opcion;
			string mensajeOk = "\nSe ha modificado el parametro con exito!!!";
			string mensajeError = "\nSe ha cancelado la operacion";
			string mensajeConfirmar = "Desea continuar con la operacion? S/Seguir o N/Cancelar";
			string confirmar;
			string cuitValidado;
			string ingresoUser;

			do
			{
				CRUD.MostrarMenu();
				Console.ForegroundColor = ConsoleColor.DarkYellow;
				Console.WriteLine("Ingrese la opcion deseada: ");
				ingreso = Console.ReadLine();

				while (!int.TryParse(ingreso, out opcion) || opcion < 0 || opcion > 7)
				{
					Console.WriteLine("Error, Reingrese la opcion : ");
					ingreso = Console.ReadLine();
				}

				switch (opcion)
				{
					case 1:
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("Ingrese el cuit: ");
						ingreso = Console.ReadLine();
						ConexionDB.ValidarNuevoCuit(ingreso);
						break;
					
					case 2:
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Ingrese el cuit a eliminar: ");
						ingresoUser = Console.ReadLine();
						cuitValidado = ConexionDB.ValidarCampo(ValidacionesString.Cuit, ingresoUser);

						if (ValidacionesString.NotNull(cuitValidado))
						{
							if (ConexionDB.BuscarCuit(cuitValidado))
							{
								Console.ForegroundColor = ConsoleColor.Green;
								ConexionDB.MostrarResto(cuitValidado, ConexionDB.ConnectionString);
								Console.WriteLine(mensajeConfirmar);
								confirmar = Console.ReadLine();
								if (ValidacionesString.ConfirmarOperacion(confirmar))
								{
									Console.ForegroundColor = ConsoleColor.Green;
									ConexionDB.Delete(cuitValidado);
									Console.WriteLine($"\nSe ha eliminado el comercio perteneciente al cuit: {cuitValidado}");
									Console.ReadKey();
								}
								else
								{
									Console.ForegroundColor = ConsoleColor.Red;
									Console.WriteLine("\nSe ha cancelado la operacion");
									Console.ReadLine();
								}
							}
							else
							{
								Console.WriteLine("\nNo se ha encontrado el cuit especificado en la base de datos");
								Console.ReadKey();
							}
						}				
							break;

					case 3:
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.WriteLine("Ingrese el cuit a modificar: ");
						ingresoUser = Console.ReadLine();
						cuitValidado = ConexionDB.ValidarCampo(ValidacionesString.Cuit, ingresoUser);

						if (ValidacionesString.NotNull(cuitValidado))
						{
							if (ConexionDB.BuscarCuit(cuitValidado))
							{
								Console.Clear();
								CRUD.UpdateMenu(cuitValidado, ConexionDB.ConnectionString, mensajeOk, mensajeError);
							}
							else
							{
								Console.WriteLine("\nNo se ha encontrado el cuit especificado en la base de datos");
								Console.ReadKey();
							}
						}
							break;

					case 4:
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Magenta;
						Console.WriteLine("Ingrese el cuit del establecimiento a mostrar: ");
						ingresoUser = Console.ReadLine();
						cuitValidado = ConexionDB.ValidarCampo(ValidacionesString.Cuit, ingresoUser);

						if (ValidacionesString.NotNull(cuitValidado))
						{
							if (ConexionDB.BuscarCuit(cuitValidado))
							{
								ConexionDB.MostrarResto(cuitValidado, ConexionDB.ConnectionString);
								Console.WriteLine("Presione una tecla para continuar");
								Console.ReadKey();
							}

							else
							{
								Console.WriteLine("\nNo se ha encontrado el cuit especificado en la base de datos");
								Console.ReadKey();
							}
						}
							break;

					case 5:
						ConexionDB.MostrarTodosRestos(ConexionDB.Get());
							break;

					case 6:
						ConexionDB.ParseJson(ConexionDB.Get());
						Console.ForegroundColor = ConsoleColor.DarkCyan;
						Console.WriteLine("\nArhivo generado con exito!");
						Console.ReadKey();
							break;

					case 7:
						Console.ResetColor();
						Console.WriteLine("\nUsted ha salido del sistema");
						Console.ReadKey();
							break;
				}
			} while (opcion != 7);			
		}
	}	
}
