using System;
using MySql.Data.MySqlClient;
string connectionString = "Server=localhost;User ID=root;Password=543210-1;";

void createSchema(MySqlConnection connection, string schemaName)
{
   
    string query = "CREATE SCHEMA " + schemaName ;
    MySqlCommand cmd = new MySqlCommand(query, connection);
    cmd.ExecuteNonQuery();
}

using (MySqlConnection connection = new MySqlConnection(connectionString))
{
    try
    {
        connection.Open();
        
        Random rand = new Random();

        int x = rand.Next(1,9999999);

        string schemaName = "hello" + x;
        createSchema(connection, schemaName);

        Console.WriteLine("created db: " + schemaName);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}
