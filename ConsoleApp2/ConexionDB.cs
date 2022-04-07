using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Establecimientos;
using System.Data;
using Validaciones;
using System.Text.RegularExpressions;

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
			string query = "SELECT * FROM establecimientosPrueba" +
				" WHERE cuit =@Cuit ";

			Restaurant unRestaurant = new Restaurant(); //Instancio el objeto para el mapeo con la DB

			using (SqlConnection connection = new SqlConnection(conexion))
			{
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Cuit", cuit);
				
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

						//Agrego los objetos cargados a la lista
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

		public static bool ValidarCampo(Predicate<string> condicion, string ingreso)
		{		
			int intentos = 0;
			int restantes = 3;
				
			if (!condicion(ingreso))
			{
				do
				{
					Console.WriteLine($"Error, reingrese el dato a modificar (Intentos : {restantes - intentos}): ");
					ingreso = Console.ReadLine();
					intentos++;

				} while (!((intentos == 3) || !condicion(ingreso)));

				if (intentos == 3)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Se acabaron los intentos :(");
					Console.ReadKey();
					return false;
				}
				else if (condicion(ingreso))
				{
					return true;
				}
				else
				{
					Console.WriteLine("\n\nNo se ha encontrado el cuit especificado");
					return false;
				}
			}
			else
				return true;			
		}

		public static void MostrarResto(string cuitMostrar, string conexion)
		{
			var restoMostrar = GetResto(cuitMostrar, conexion);
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("\n\n" + "Cuit: " + restoMostrar.Cuit + "\n" +
									   "Nombre: " + restoMostrar.Nombre + "\n" +
									   "Ubicacion: " + restoMostrar.Ubicacion + "\n" +
									   "Pais: " + restoMostrar.Pais + "\n" +
									   "Telefono: " + restoMostrar.Telefono + "\n" +
									   "Empleados: " + restoMostrar.CantidadEmpleados + "\n" +
									   "Fecha Alta: " + restoMostrar.FechaAlta + "\n" +
									   "Impuestos al dia: " + Convert.ToInt32(restoMostrar.ImpuestosAlDia) + "\n" +
									   "Importe Impuestos: " + restoMostrar.ImporteImpuestos + "\n" +
									   "Mail: " + restoMostrar.MailContacto + "\n\n");

			Console.WriteLine("Presione una tecla para continuar");
			Console.ReadKey();
		}

		public static void MostrarTodosRestos()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Cyan;
			var mostrarEstablecimientos = Get();
			mostrarEstablecimientos.ForEach(d => Console.WriteLine("\n\n" +
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
										   "##############################################\n" +
										   "##############################################\n" +
										   "##############################################\n"));

			Console.WriteLine("Presione una tecla para continuar");
			Console.ReadKey();
		}

		public static bool ConfirmarOperacion(string ingreso)
		{
			return (new string[] { "SI", "Si", "S", "si", "s" }.Any(c => ingreso.Contains(c)));
		}

	}
}
