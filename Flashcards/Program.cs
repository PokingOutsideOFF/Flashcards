// See https://aka.ms/new-console-template for more information
using System.Configuration;
using System.Data.SqlClient;

public class Program
{
    static void Main()
    {

        string? connectionString = ConfigurationManager.AppSettings.Get("FlashcardsDBConnection");
        string query = "SELECT * FROM stack";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Access columns by index or name
                    Console.WriteLine(reader[0].ToString() +" - "+reader[1].ToString());
                }
            }
        }
    }
}


