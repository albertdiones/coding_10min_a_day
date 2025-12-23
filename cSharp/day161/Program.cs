using System;
using MySql.Data.MySqlClient;
string connectionString = "Server=localhost;User ID=root;Password=543210-1;";

using (MySqlConnection connection = new MySqlConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Successfully connected to MySQL!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}
