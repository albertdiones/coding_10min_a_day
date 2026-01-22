using System;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;


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


MySqlDataReader SelectRows(
    MySqlConnection connection,
    string tableName
)
{

    string query = $"SELECT * FROM {tableName}";

    using var cmd = new MySqlCommand(query, connection);

    return cmd.ExecuteReader();
}


void UpdateRow(
    MySqlConnection connection,
    string tableName,
    string rowId,
    Dictionary<string, string> values
)
{
    if (values == null || values.Count == 0)
        throw new ArgumentException("At least one column must be specified.");

    var updateSets = string.Join(", ",
        values.Select(c => $"{c.Key} = '{c.Value}'")
    );

    string query = $"UPDATE `{tableName}` SET {updateSets} WHERE id = {rowId};";

    Console.WriteLine(query);

    using var cmd = new MySqlCommand(query, connection);
    cmd.ExecuteNonQuery();
}


using (MySqlConnection connection = new MySqlConnection(connectionString))
{
    connection.Open();
        
    string tableName = "lamesa_2861101";

    Console.WriteLine("table row count: " + SelectCount(connection, tableName));

    Console.WriteLine("");

    Console.WriteLine("Please input the row id you want to delete:");

    string inputId = Console.ReadLine();
    
    int.TryParse(inputId, out int deleteId);

    string query = $"DELETE FROM {tableName} WHERE id = '{deleteId}'";

    using var cmd = new MySqlCommand(query, connection);

    cmd.ExecuteNonQuery();
    
    Console.WriteLine("Sucessfully deleted row");
    Console.WriteLine("table row count: " + SelectCount(connection, tableName));

}
