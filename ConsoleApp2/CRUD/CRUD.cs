using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ConexionSQL;
using Establecimientos;
using Validaciones;

namespace MenuCrud
{
	public static class CRUD
	{
		public static void MostrarMenu()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("##############################################");
			Console.WriteLine("##############################################");
			Console.WriteLine("##############################################");
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("########### Bienvenido al CRUD ###############");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("##############################################");
			Console.WriteLine("##############################################");
			Console.WriteLine("##############################################\n");
			Console.WriteLine("Ingrese la opcion que desea utilizar: \n");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("1 - Cargar Comercio\n");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("2 - Eliminar Comercio\n");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("3 - Modificar Comercio\n");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine("4 - Mostrar Comercio\n");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("5 - Mostrar todos los comercios\n");
			Console.ResetColor();
			Console.WriteLine("6 - Salir\n");					
		}

		public static Restaurant DatosRestaurant()
		{								
			Restaurant unRestaurant = new Restaurant();

			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Ingrese el cuit del restaurant: ");
			string aux = Console.ReadLine();
			
			if (ValidacionesString.Cuit(aux))
				unRestaurant.Cuit = aux;
			else
				unRestaurant.Cuit = "";
				
			Console.WriteLine("\nIngrese el nombre del restaurant: ");
			aux = Console.ReadLine();

			if (ValidacionesString.SoloLetras(aux))
				unRestaurant.Nombre = aux;
			else
				unRestaurant.Nombre = null;
	
			Console.WriteLine("\nIngrese la ubicación del restaurant: ");
			aux = Console.ReadLine();

			if (ValidacionesString.LetrasNumeros(aux))
				unRestaurant.Ubicacion = aux;
			else
				unRestaurant.Ubicacion = null;

			Console.WriteLine("\nIngrese el país del restaurant: ");
			aux = Console.ReadLine();

			if (ValidacionesString.SoloLetras(aux))
				unRestaurant.Pais = aux;
			else
				unRestaurant.Pais = null;

			Console.WriteLine("\nIngrese el telefono del restaurant: ");
			aux = Console.ReadLine();

			if (ValidacionesString.TelefonoFijo(aux))
				unRestaurant.Telefono = aux;
			else
				unRestaurant.Telefono = null;

			Console.WriteLine("\nIngrese la cantidad del empleados: ");
			aux = Console.ReadLine();

			if (ValidacionesString.SoloNumeros(aux))
				unRestaurant.CantidadEmpleados = Convert.ToInt32(aux);
			else
				unRestaurant.CantidadEmpleados = null;

			Console.WriteLine("\nIngrese la fecha de inicio de actividades: ");
			aux = Console.ReadLine();

			if (ValidacionesString.FormatoFecha(aux))
				unRestaurant.FechaAlta = Convert.ToDateTime(aux);
			else
				unRestaurant.FechaAlta = null;

			Console.WriteLine("\nIngrese el total de impuestos pagos");
			unRestaurant.ImporteImpuestos = Convert.ToDecimal(Console.ReadLine());

			Console.WriteLine("\nTiene los impuestos al día: ");
			unRestaurant.ImpuestosAlDia = Convert.ToBoolean(Console.ReadLine());

			Console.WriteLine("\nIngrese el mail de contacto: ");
			aux = Console.ReadLine();

			if (ValidacionesString.CorreoFormato(aux))
				unRestaurant.MailContacto = aux;
			else
				unRestaurant.MailContacto = null;
		
			return unRestaurant;
		}

		public static void UpdateMenu(string cuit, string conexion, string mensajeOk, string mensajeError)
		{
			string ingreso;
			int opcion;
			string query;
			string confirmar;
			string mensaje = "\nIngrese el nuevo dato a modificar: ";
			string mensajeConfirmar = "\nDesea continuar con la operacion? Si, para continuar o cualquier tecla para cancelar";

			do
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Green;

				Console.WriteLine($"Cuit: {cuit}");

				Console.ForegroundColor = ConsoleColor.Yellow;

				Console.WriteLine("\nBienvenido al Menu de Modificaciones");
				Console.WriteLine("\nQué desea modificar?");
				Console.WriteLine("\n1)Nombre");
				Console.WriteLine("\n2)Ubicación");
				Console.WriteLine("\n3)País");
				Console.WriteLine("\n4)Telefono");
				Console.WriteLine("\n5)Cantidad de Empleados");
				Console.WriteLine("\n6)Fecha Alta");
				Console.WriteLine("\n7)Estado de los Impuestos al Día");
				Console.WriteLine("\n8)El Importe de Impuestos");
				Console.WriteLine("\n9)Mail de Contacto");
				Console.WriteLine("\n10)Salir");

				Console.WriteLine("\nIngrese una opcion para operar: ");
				ingreso = Console.ReadLine();

				while (!int.TryParse(ingreso, out opcion) || Convert.ToInt32(opcion) < 0 || Convert.ToInt32(opcion) > 11)
				{
					Console.WriteLine("Error, Reingrese la opcion : ");
					ingreso = Console.ReadLine();
				}
					switch (opcion)
					{
						case 1:
						Console.Clear();
						query = "UPDATE establecimientosPrueba SET nombre =@Value" +
								" WHERE cuit =@Cuit";

						Console.Clear();
						Console.WriteLine("Modificar: Nombre");
						Console.WriteLine(mensaje);
						ingreso = Console.ReadLine();
						if (ConexionDB.ValidarCampo(ValidacionesString.NombreApellido, ingreso))
						{
							Console.WriteLine(mensajeConfirmar);
							confirmar = Console.ReadLine();
							if (ConexionDB.ConfirmarOperacion(confirmar))
							{
								ConexionDB.UpdateParameter(query, conexion, ingreso, cuit);
								Console.WriteLine(mensajeOk);
								Console.ForegroundColor = ConsoleColor.Green;
								ConexionDB.MostrarResto(cuit, conexion);
							}
							else
							{
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine("\nSe ha cancelado la operacion");
								Console.ReadLine();
							}
						}
							break;

						case 2:
						Console.Clear();
						query = "UPDATE establecimientosPrueba SET ubicacion = @Value" +
								" WHERE cuit =@Cuit";

						Console.Clear();
						Console.WriteLine("Modificar: Ubicacion");
						Console.WriteLine(mensaje);
						ingreso = Console.ReadLine();
						if (ConexionDB.ValidarCampo(ValidacionesString.LetrasNumeros, ingreso))
						{
							Console.WriteLine(mensajeConfirmar);
							confirmar = Console.ReadLine();
							if (ConexionDB.ConfirmarOperacion(confirmar))
							{
								ConexionDB.UpdateParameter(query, conexion, ingreso, cuit);
								Console.WriteLine(mensajeOk);
								Console.ForegroundColor = ConsoleColor.Green;
								ConexionDB.MostrarResto(cuit, conexion);
							}
							else
							{
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine(mensajeError);
								Console.ReadLine();
							}
						}
							break;

						case 3:

						query = "UPDATE establecimientosPrueba SET pais = @Value" +
								" WHERE cuit =@Cuit";

						Console.Clear();
						Console.WriteLine("Modificar: Pais");
						Console.WriteLine(mensaje);
						ingreso = Console.ReadLine();
						if (ConexionDB.ValidarCampo(ValidacionesString.SoloLetras, ingreso))
						{
							Console.WriteLine(mensajeConfirmar);
							confirmar = Console.ReadLine();
							if (ConexionDB.ConfirmarOperacion(confirmar))
							{
								ConexionDB.UpdateParameter(query, conexion, ingreso, cuit);
								Console.WriteLine(mensajeOk);
								Console.ForegroundColor = ConsoleColor.Green;
								ConexionDB.MostrarResto(cuit, conexion);
							}
							else
							{
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine(mensajeError);
								Console.ReadLine();
							}
						}
							break;

						case 4:
						query = "UPDATE establecimientosPrueba SET telefono = @Value" +
								" WHERE cuit =@Cuit";

						Console.Clear();
						Console.WriteLine("Modificar: Telefono de Contacto");
						Console.WriteLine(mensaje);
						ingreso = Console.ReadLine();
						if (ConexionDB.ValidarCampo(ValidacionesString.TelefonoFijo, ingreso))
						{
							Console.WriteLine(mensajeConfirmar);
							confirmar = Console.ReadLine();
							if (ConexionDB.ConfirmarOperacion(confirmar))
							{
								ConexionDB.UpdateParameter(query, conexion, ingreso, cuit);
								Console.WriteLine(mensajeOk);
								Console.ForegroundColor = ConsoleColor.Green;
								ConexionDB.MostrarResto(cuit, conexion);
							}
							else
							{
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine(mensajeError);
								Console.ReadLine();
							}
						}
						break;

						case 5:
						query = "UPDATE establecimientosPrueba SET cantidadEmpleados = @Value" +
								" WHERE cuit =@Cuit";

						Console.Clear();
						Console.WriteLine("Modificar: Cantidad Empleados");
						Console.WriteLine(mensaje);
						ingreso = Console.ReadLine();
						if (ConexionDB.ValidarCampo(ValidacionesString.SoloNumeros, ingreso))
						{
							Console.WriteLine(mensajeConfirmar);
							confirmar = Console.ReadLine();
							if (ConexionDB.ConfirmarOperacion(confirmar))
							{
								ConexionDB.UpdateParameter(query, conexion, Convert.ToInt32(ingreso), cuit);
								Console.ForegroundColor = ConsoleColor.Green;
								Console.WriteLine(mensajeOk);
								ConexionDB.MostrarResto(cuit, conexion);
							}
							else
							{
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine(mensajeError);
								Console.ReadLine();
							}
						}
						break;

						case 6:
						query = "UPDATE establecimientosPrueba SET fechaAlta = @Value" +
								" WHERE cuit =@Cuit";

						Console.Clear();
						Console.WriteLine("Modificar: Fecha Alta");
						Console.WriteLine($"{mensaje} (dd/mm/aaaa)");
						ingreso = Console.ReadLine();
						if (ConexionDB.ValidarCampo(ValidacionesString.FormatoFecha, ingreso))
						{
							Console.WriteLine(mensajeConfirmar);
							confirmar = Console.ReadLine();
							if (ConexionDB.ConfirmarOperacion(confirmar))
							{
								ConexionDB.UpdateParameter(query, conexion, Convert.ToDateTime(ingreso), cuit);
								Console.ForegroundColor = ConsoleColor.Green;
								Console.WriteLine(mensajeOk);
								ConexionDB.MostrarResto(cuit, conexion);
							}
							else
							{
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine(mensajeError);
								Console.ReadLine();
							}
						}
						break;

						case 7:
							break;

						case 8:
							break;

						case 9:
						query = "UPDATE establecimientosPrueba SET mailContacto = @Value" +
								" WHERE cuit =@Cuit";

						Console.Clear();
						Console.WriteLine("Modificar: Mail");
						Console.WriteLine(mensaje);
						ingreso = Console.ReadLine();
						if (ConexionDB.ValidarCampo(ValidacionesString.CorreoFormato, ingreso))
						{
							Console.WriteLine(mensajeConfirmar);
							confirmar = Console.ReadLine();
							if (ConexionDB.ConfirmarOperacion(confirmar))
							{
								ConexionDB.UpdateParameter(query, conexion, ingreso, cuit);
								Console.ForegroundColor = ConsoleColor.Green;
								Console.WriteLine(mensajeOk);
								ConexionDB.MostrarResto(cuit, conexion);
								Console.ReadLine();
							}
							else
							{
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine(mensajeError);
								Console.ReadLine();
							}
						}
						break;

						case 10:
							Console.ResetColor();
							Console.WriteLine("\n\nUsted ha salido del menu de modificaciones");
							Console.ReadKey();
							break;
					}

			} while (opcion != 10);
			
		}
	}
}
