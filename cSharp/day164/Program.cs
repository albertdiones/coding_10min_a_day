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



void InsertRow(
    MySqlConnection connection,
    string tableName,
    Dictionary<string, string> row
)
{
    if (row == null || row.Count == 0)
        throw new ArgumentException("At least one column must be specified.");

    var columNames = string.Join(", ",
        row.Select(c => $"{c.Key}")
    );

    
    var values = "'" + string.Join("', '",
        row.Select(c => $"{c.Value}")
    ) + "'";


    string query = $"INSERT INTO {tableName}({columNames}) VALUES({values});";

    using var cmd = new MySqlCommand(query, connection);
    cmd.ExecuteNonQuery();
}



string SelectCount(
    MySqlConnection connection,
    string tableName
)
{

    string query = $"SELECT COUNT(0) FROM {tableName}";

    using var cmd = new MySqlCommand(query, connection);
    return cmd.ExecuteScalar().ToString();
}


using (MySqlConnection connection = new MySqlConnection(connectionString))
{
        connection.Open();
        
        Random rand = new Random();

        int x = rand.Next(1,9999999);

        var row = new Dictionary<string, string>
            {
                { "id", x.ToString() }
            };

        string tableName = "lamesa_2861101";

        InsertRow(connection, tableName, row);

        Console.WriteLine("inserted row!! id: " + x);
        Console.WriteLine("table row count: " + SelectCount(connection, tableName));
}
