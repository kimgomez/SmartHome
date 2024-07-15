using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SmartHome
{
    public class ClassSmartHome
    {
        public static string conexion = "Data source = 192.168.5.150; Initial Catalog = SmartHome; Integrated Security=False; User Id= test; Password = 1234;";

        public static SqlConnection conectar = new SqlConnection(conexion);
        public static void abrir()
        {
            if (conectar.State == ConnectionState.Closed) 
            {
                conectar.Open();
            }
        }

        public static void cerrar()
        {
            if (conectar.State == ConnectionState.Open)
            {
                conectar.Close();
            }
        }
        //private readonly string connectionString;

        //public ClassSmartHome(string server, string database, string user, string password)
        //{
        //    //connectionString = $"Server={server};Database={database};User Id={user};Password={password};";
        //    connectionString = $"Data Source=localhost\\SQLEXPRESS;Database={database};Integrated Security=SSPI";
        //   //bool n= TestConnectionAsync();
        //}

        //public async Task<bool> TestConnectionAsync()
        //{
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            await connection.OpenAsync();
        //            Console.WriteLine("Connection successful.");
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Connection failed: {ex.Message}");
        //        return false;
        //    }
        //}

        //public async Task<int> ExecuteNonQueryAsync(string query)
        //{
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            await connection.OpenAsync();
        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                return await command.ExecuteNonQueryAsync();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Query execution failed: {ex.Message}");
        //        return -1;
        //    }
        //}

        //public async Task<int> ExecuteScalarAsync(string query, SqlParameter[] parameters)
        //{
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            await connection.OpenAsync();
        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                if (parameters != null)
        //                {
        //                    command.Parameters.AddRange(parameters);
        //                }

        //                return (int)await command.ExecuteScalarAsync();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Query execution failed: {ex.Message}");
        //        return -1;
        //    }
        //}

        //public async Task ReadDataAsync(string query)
        //{
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            await connection.OpenAsync();
        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                using (SqlDataReader reader = await command.ExecuteReaderAsync())
        //                {
        //                    while (await reader.ReadAsync())
        //                    {
        //                        Console.WriteLine($"{reader[0]} - {reader[1]}");
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Data reading failed: {ex.Message}");
        //    }
        //}
    }
}
