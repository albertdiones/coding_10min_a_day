using System;
using MySql.Data.MySqlClient;
string connectionString = "Server=localhost;User ID=root;Password=543210-1;Database=hello1";

void createSchema(MySqlConnection connection, string schemaName)
{
   
    string query = "CREATE SCHEMA " + schemaName ;
    MySqlCommand cmd = new MySqlCommand(query, connection);
    cmd.ExecuteNonQuery();
}
void CreateTable(
    MySqlConnection connection,
    string tableName,
    Dictionary<string, string> columns
)
{
    if (columns == null || columns.Count == 0)
        throw new ArgumentException("At least one column must be specified.");

    var columnDefinitions = string.Join(", ",
        columns.Select(c => $"{c.Key} {c.Value}")
    );

    string query = $"CREATE TABLE `{tableName}` ({columnDefinitions});";

    using var cmd = new MySqlCommand(query, connection);
    cmd.ExecuteNonQuery();
}

using (MySqlConnection connection = new MySqlConnection(connectionString))
{
    try
    {
        connection.Open();
        
        Random rand = new Random();

        int x = rand.Next(1,9999999);

        var columns = new Dictionary<string, string>
            {
                { "id", "INT AUTO_INCREMENT PRIMARY KEY" }
            };

        string tableName = "lamesa_" + x;

        CreateTable(connection, tableName, columns);

        Console.WriteLine("created table: " + tableName);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}
