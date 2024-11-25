
using Microsoft.Data.SqlClient;


namespace conexionBD
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string connectionString = "Server= DESKTOP-FBUD5U5; Database= Northwind; Trusted_Connection= True;TrustServerCertificate=True";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductID, ProductName, UnitPrice FROM Products";
                string insertQuery = "INSERT INTO Products (ProductName, UnitPrice, UnitsInStock) VALUES (@ProductName, @UnitPrice, @UnitsInStock)";
                string deleteQuery = "DELETE FROM Products WHERE ProductName = @ProductName";
                string updateQuery = "UPDATE Products SET UnitPrice = @UnitPrice WHERE ProductName = @ProductName";


                try
                {
                    conexion.Open();

                    using (SqlCommand command = new SqlCommand(insertQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@ProductName", "New Product");
                        command.Parameters.AddWithValue("@UnitPrice", 15.00);
                        command.Parameters.AddWithValue("@UnitsInStock", 50);


                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} fila(s) insertada(s).");
                    }
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            Console.WriteLine("ProductID\tProductName\t\tUnitPrice");

                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader["ProductID"]}\t{reader["ProductName"]}\t{reader["UnitPrice"]}");
                            }
                        }
                    }
                    using (SqlCommand updcommand = new SqlCommand(updateQuery, conexion))
                    {
                        updcommand.Parameters.AddWithValue("@ProductName", "New Product");
                        updcommand.Parameters.AddWithValue("@UnitPrice", 50.00);


                        int rowsupd = updcommand.ExecuteNonQuery();
                        Console.WriteLine($"{rowsupd} fila(s) actualizada(s).");
                    }
                    using (SqlCommand dltcommand = new SqlCommand(deleteQuery, conexion))
                    {
                        dltcommand.Parameters.AddWithValue("@ProductName", "New Product");
                        int rowsdlt = dltcommand.ExecuteNonQuery();
                        Console.WriteLine($"{rowsdlt} filas(s) eliminada(s).");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");

                }

                
                               


            }
        }
    }
}
