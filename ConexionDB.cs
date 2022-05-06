using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Establecimientos;
using Newtonsoft.Json;
using Validaciones;
using MenuCrud;

namespace ConexionSQL
{
	public static class ConexionDB
	{
		public static string ConnectionString { get => "Data Source=localhost\\SQLEXPRESS02;Initial Catalog=Prueba;Trusted_Connection=True";}

		public static List<Restaurant> Get()
		{
			string query = "SELECT * FROM dbo.establecimientosPrueba";  //Defino la query 
			List<Restaurant> restaurants = new List<Restaurant>();  //Defino la lista de objetos a retornar

			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);

				try
				{
					connection.Open();  //Abro la conexion a la base

					SqlDataReader reader = command.ExecuteReader(); //Instancio el reader (permite leer los datos de la base)

					while (reader.Read())   //Permite itinerar entre las columnas de la tabla, cuando no existan mas para leer, retorna false
					{
						Restaurant unRestaurant = new Restaurant(); //Instancio el objeto para el mapeo con la DB

						unRestaurant.Cuit = reader.GetString(0);    //Asigno todas las columnas a los atributos del objeto de mapeo

						if (!reader.IsDBNull(1))
							unRestaurant.Nombre = reader.GetString(1);
						else
							unRestaurant.Nombre = null;

						if (!reader.IsDBNull(2))
							unRestaurant.Ubicacion = reader.GetString(2);
						else
							unRestaurant.Ubicacion = null;

						if (!reader.IsDBNull(3))
							unRestaurant.Pais = reader.GetString(3);
						else
							unRestaurant.Pais = null;

						if (!reader.IsDBNull(4))
							unRestaurant.Telefono = reader.GetString(4);
						else
							unRestaurant.Telefono = null;

						if (!reader.IsDBNull(5))
							unRestaurant.CantidadEmpleados = reader.GetInt32(5);
						else
							unRestaurant.CantidadEmpleados = null;

						if (!reader.IsDBNull(6))
							unRestaurant.FechaAlta = reader.GetDateTime(6);
						else
							unRestaurant.FechaAlta = null;

						if (reader[7].ToString().Contains("1"))
							unRestaurant.ImpuestosAlDia = true;
						else
							unRestaurant.ImpuestosAlDia = false;

						if (!reader.IsDBNull(7))
							unRestaurant.ImporteImpuestos = reader.GetDecimal(8);
						else
							unRestaurant.ImporteImpuestos = null;
						//unRestaurant.TotalImpuestos = reader.GetDecimal(9);
						if (!reader.IsDBNull(10))
							unRestaurant.MailContacto = reader.GetString(10);
						else
							unRestaurant.MailContacto = null;

						restaurants.Add(unRestaurant);  //Agrego los objetos cargados a la lista
					}

					reader.Close(); //Cierro el reader
					connection.Close(); //Cierro la conexion
				}
				catch (Exception ex)
				{
					throw new Exception("Hay un error en la db" + ex.Message);
				}
			}
			return restaurants; //Retorno la lista
		}

		public static void Add(Restaurant restaurante)
		{
			string query = "INSERT INTO establecimientosPrueba" +
				"(cuit, nombre, ubicacion, pais, telefono, cantidadEmpleados, fechaAlta, impuestosAlDia, importeImpuestos, mailContacto) values" +
				"(@Cuit, @Nombre, @Ubicacion, @Pais, @Telefono, @CantidadEmpleados, @FechaAlta, @ImpuestosAlDia, @ImporteImpuestos, @MailContacto)";

			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);

				if(restaurante.Cuit is not null)
					command.Parameters.AddWithValue("@Cuit", restaurante.Cuit);
				else
					command.Parameters.AddWithValue("@Cuit", DBNull.Value);

				if(restaurante.Nombre is not null)
					command.Parameters.AddWithValue("@Nombre", restaurante.Nombre);
				else
					command.Parameters.AddWithValue("@Nombre", DBNull.Value);

				if(restaurante.Ubicacion is not null)
					command.Parameters.AddWithValue("@Ubicacion", restaurante.Ubicacion);
				else
					command.Parameters.AddWithValue("@Ubicacion", DBNull.Value);

				if(restaurante.Pais is not null)
					command.Parameters.AddWithValue("@Pais", restaurante.Pais);
				else
					command.Parameters.AddWithValue("@Pais", DBNull.Value);

				if(restaurante.Telefono is not null)
					command.Parameters.AddWithValue("@Telefono", restaurante.Telefono);
				else
					command.Parameters.AddWithValue("@Telefono", DBNull.Value);

				if (restaurante.CantidadEmpleados is not null)
					command.Parameters.AddWithValue("@CantidadEmpleados", restaurante.CantidadEmpleados);
				else
					command.Parameters.AddWithValue("@CantidadEmpleados", DBNull.Value);

				if(restaurante.FechaAlta is not null)
					command.Parameters.AddWithValue("@FechaAlta", restaurante.FechaAlta);
				else
					command.Parameters.AddWithValue("@FechaAlta", DBNull.Value);

				command.Parameters.AddWithValue("@ImpuestosAlDia", restaurante.ImpuestosAlDia);

				if(restaurante.ImporteImpuestos is not null)
					command.Parameters.AddWithValue("@ImporteImpuestos", restaurante.ImporteImpuestos);
				else
					command.Parameters.AddWithValue("@ImporteImpuestos", DBNull.Value);

				if(restaurante.MailContacto is not null)
					command.Parameters.AddWithValue("@MailContacto", restaurante.MailContacto);
				else
					command.Parameters.AddWithValue("@MailContacto", DBNull.Value);

				try
				{
					connection.Open();  //Abro la conexion a la base

					command.ExecuteNonQuery();

					connection.Close(); //Cierro la conexion
				}
				catch (Exception ex)
				{
					throw new Exception("Hay un error en la db" + ex.Message);
				}
			}
		}

		public static void Delete(string cuit)
		{
			string query = "DELETE FROM establecimientosPrueba" +
							" WHERE cuit =@Cuit ";

			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Cuit", cuit);

				try
				{
					connection.Open();  //Abro la conexion a la base

					command.ExecuteNonQuery();

					connection.Close(); //Cierro la conexion
				}
				catch (Exception ex)
				{
					throw new Exception("Hay un error en la db" + ex.Message);
				}
			}
		}

		public static Restaurant GetResto(string cuit, string conexion)
		{
			string query = "SELECT * FROM establecimientosPrueba" +	//query 
				" WHERE cuit =@Cuit ";

			Restaurant unRestaurant = new Restaurant(); //Instancio el objeto para el mapeo con la DB

			using (SqlConnection connection = new SqlConnection(conexion))	//llamo al object Connection para iniciar la conexion
			{
				SqlCommand command = new SqlCommand(query, connection);	//LLamo al SQLcommand y le asigno la query junto a la conexion
				command.Parameters.AddWithValue("@Cuit", cuit);	//Asigno el valor al alias @Cuit
				
				try
				{
					connection.Open();  //Abro la conexion a la base

					SqlDataReader reader = command.ExecuteReader(); //Instancio el reader (permite leer los datos de la base)

					while (reader.Read())   //Permite itinerar entre las columnas de la tabla, cuando no existan mas para leer, retorna false
					{
						unRestaurant.Cuit = reader.GetString(0);    //Asigno todas las columnas a los atributos del objeto de mapeo

						if (!reader.IsDBNull(1))
							unRestaurant.Nombre = reader.GetString(1);
						else
							unRestaurant.Nombre = null;

						if (!reader.IsDBNull(2))
							unRestaurant.Ubicacion = reader.GetString(2);
						else
							unRestaurant.Ubicacion = null;

						if (!reader.IsDBNull(3))
							unRestaurant.Pais = reader.GetString(3);
						else
							unRestaurant.Pais = null;

						if (!reader.IsDBNull(4))
							unRestaurant.Telefono = reader.GetString(4);
						else
							unRestaurant.Telefono = null;

						if (!reader.IsDBNull(5))
							unRestaurant.CantidadEmpleados = reader.GetInt32(5);
						else
							unRestaurant.CantidadEmpleados = null;

						if (!reader.IsDBNull(6))
							unRestaurant.FechaAlta = reader.GetDateTime(6);
						else
							unRestaurant.FechaAlta = null;

						if (reader[7].ToString().Contains("1"))
							unRestaurant.ImpuestosAlDia = true;
						else
							unRestaurant.ImpuestosAlDia = false;

						if (!reader.IsDBNull(7))
							unRestaurant.ImporteImpuestos = reader.GetDecimal(8);
						else
							unRestaurant.ImporteImpuestos = null;
						//unRestaurant.TotalImpuestos = reader.GetDecimal(9);
						if (!reader.IsDBNull(10))
							unRestaurant.MailContacto = reader.GetString(10);
						else
							unRestaurant.MailContacto = null;
					}
					reader.Close(); //Cierro el reader
					command.ExecuteNonQuery();
					connection.Close(); //Cierro la conexion
				}
				catch (Exception ex)
				{
					throw new Exception("Hay un error en la db" + ex.Message);
				}				
			}
			return unRestaurant;
		}

		public static bool BuscarCuit(string cuit)
		{
			string query = "IF EXISTS(SELECT cuit FROM establecimientosPrueba WHERE cuit = @Cuit) BEGIN SELECT 1 END ELSE BEGIN SELECT 0 END";				
			bool estado = true;

			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Cuit", cuit);

				try
				{
					connection.Open();  //Abro la conexion a la base

					SqlDataReader reader = command.ExecuteReader(); //Instancio el reader (permite leer los datos de la base)

					while (reader.Read())   //Permite itinerar entre las columnas de la tabla, cuando no existan mas para leer, retorna false
					{
						if (reader.GetInt32(0) == 0)
							estado = false;
						else
							estado = true;
					}

					reader.Close(); //Cierro el reader
					connection.Close(); //Cierro la conexion
				}
				catch (Exception ex)
				{
					throw new Exception("Hay un error en la db" + ex.Message);
				}
			}
			return estado;
		}

		public static void UpdateParameter<T>(string query, string conexion, T parametro, string cuit)
		{
			using (SqlConnection connection = new SqlConnection(conexion))
			{
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Cuit", cuit);
				command.Parameters.AddWithValue("@Value", parametro);
				//Regex regex = new Regex(@"^=[A-Za-z]+ ?[A-Za-z]+\w$");
				//var match = regex.Match("=hola mundo");

				try
				{
					connection.Open();  //Abro la conexion a la base

					command.ExecuteNonQuery();

					connection.Close(); //Cierro la conexion
				}
				catch (Exception ex)
				{
					throw new Exception("Hay un error en la db" + ex.Message);
				}
			}
		}

		public static string ValidarCampo(Predicate<string> condicion, string ingreso)
		{		
			int intentos = 1;
			int restantes = 4;
				
			if (!condicion(ingreso))
			{
				do
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine($"Error, reingrese el dato a modificar (Intentos : {restantes - intentos}): ");
					intentos++;
					ingreso = Console.ReadLine();					

				} while ((intentos < 4) && !condicion(ingreso));

				if (condicion(ingreso))
				{
					return ingreso;
				}
				else if (intentos == 4)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Se acabaron los intentos :(");
					Console.ReadKey();
					return null;
				}				
					return null;				
			}
			else
				return ingreso;			
		}

		public static void MostrarResto(string cuitMostrar, string conexion)
		{
			var restoMostrar = GetResto(cuitMostrar, conexion);

			try
			{
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("\n" + "Cuit: " + restoMostrar.Cuit + "\n" +
										   "Nombre: " + restoMostrar.Nombre + "\n" +
										   "Ubicacion: " + restoMostrar.Ubicacion + "\n" +
										   "Pais: " + restoMostrar.Pais + "\n" +
										   "Telefono: " + restoMostrar.Telefono + "\n" +
										   "Empleados: " + restoMostrar.CantidadEmpleados + "\n" +
										   "Fecha Alta: " + restoMostrar.FechaAlta + "\n" +
										   "Impuestos al dia: " + Convert.ToInt32(restoMostrar.ImpuestosAlDia) + "\n" +
										   "Importe Impuestos: " + restoMostrar.ImporteImpuestos + "\n" +
										   "Mail: " + restoMostrar.MailContacto + "\n");
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}		
		}

		public static void MostrarTodosRestos(List<Restaurant> listaRestos)
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Cyan;

			try
			{
				listaRestos.ForEach(d => Console.WriteLine("\n" +
										   "Cuit: " + d.Cuit + "\n" +
										   "Nombre: " + d.Nombre + "\n" +
										   "Ubicacion: " + d.Ubicacion + "\n" +
										   "Pais: " + d.Pais + "\n" +
										   "Telefono: " + d.Telefono + "\n" +
										   "Empleados: " + d.CantidadEmpleados + "\n" +
										   "Fecha Alta: " + d.FechaAlta + "\n" +
										   "Impuestos al dia: " + Convert.ToInt32(d.ImpuestosAlDia) + "\n" +
										   "Importe Impuestos: " + d.ImporteImpuestos + "\n" +
										   "Mail: " + d.MailContacto + "\n\n" +
										   "**********************************************\n" +
										   "**********************************************\n" +
										   "**********************************************"));

				Console.WriteLine("\nPresione una tecla para continuar");
				Console.ReadKey();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}				
		}

		public static void ParseJson(List<Restaurant> listaRestos)
		{
			var json = JsonConvert.SerializeObject(listaRestos);
			System.IO.File.WriteAllText(@"C:\Users\braian.cespedes\Desktop\Capacitacion\path.txt", json);
		}

		public static void ValidarNuevoCuit(string ingreso)
		{
			if (ValidacionesString.NotNull(ingreso))
			{
				if (!ValidacionesString.Cuit(ingreso))
				{
					int intentos = 1;
					int restantes = 4;
					do
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine($"Error, reingrese el dato a modificar (Intentos : {restantes - intentos}): ");
						intentos++;
						ingreso = Console.ReadLine();

					} while ((intentos < 4) && !ValidacionesString.Cuit(ingreso));

					if (ValidacionesString.Cuit(ingreso))
					{
						if (!BuscarCuit(ingreso))
						{
							Add(CRUD.DatosRestaurant(ingreso));
							Console.ForegroundColor = ConsoleColor.Green;
							Console.WriteLine("\nRestaurant agregado con exito!!!");
							Console.ReadKey();
						}
						else if (BuscarCuit(ingreso))
						{
							Console.ForegroundColor = ConsoleColor.Blue;
							Console.WriteLine("\nYa existe el cuit ingresado en la base de datos");
							Console.ReadKey();
						}
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Se acabaron los intentos :(");
						Console.ReadKey();
					}
				}
				else if (!BuscarCuit(ingreso))
				{
					Add(CRUD.DatosRestaurant(ingreso));
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("\nRestaurant agregado con exito!!!");
					Console.ReadKey();
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.WriteLine("\nYa existe el cuit ingresado en la base de datos");
					Console.ReadKey();
				}
			}
		}
	}
}
