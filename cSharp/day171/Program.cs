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


void PrettyPrintRows(
    string[][] rows,
    int? selectedColumnIndex = null,
    bool includeRowNumbers = false,
    int rowNumberOffset = 0
)
{
    if (rows.Length == 0)
    {
        return;
    }

    int[] colMaxLengths = new int[rows[0].Length];

    foreach (string[] row in rows)
    {
        foreach (
            var (index, cell)
                in row.Select(
                    (arrayValue, Arrayindex)
                        => (Arrayindex, arrayValue)
                ))
        {
            colMaxLengths[index] = Math.Max(
                colMaxLengths[index],
                cell.Length
            );
        }
    }

    int totalLength = colMaxLengths.Sum();

    int minTableWidth = totalLength + (4 * colMaxLengths.Length);

    if (includeRowNumbers)
    {
        minTableWidth += 2;
    }

    int vacantSpace = Console.WindowWidth - minTableWidth;

    int[] columnWidths = (int[])colMaxLengths.Clone();

    foreach (
        var (columnNumber, columnWidth)
                in columnWidths.Select(
                    (arrayValue, Arrayindex)
                        => (Arrayindex, arrayValue)
                )
    )
    {
        double additional = vacantSpace * columnWidths[columnNumber] / totalLength;

        columnWidths[columnNumber] += Convert.ToInt32(
            Math.Round(
                additional
            )
        );
    }

    int tableWidth = minTableWidth + vacantSpace -2;

    // Border sa taas
    Console.WriteLine(
        ' ' +
        new string('-', tableWidth - 3)
    );

    foreach (
        var (rowId, row)
            in rows.Select(
                (arrayValue, Arrayindex)
                    => (Arrayindex, arrayValue)
            )
    )
    {

        Console.Write("|");
        foreach (
            var (index, cell)
                in row.Select(
                    (arrayValue, Arrayindex)
                        => (Arrayindex, arrayValue)
                )
        )
        {

            if (includeRowNumbers && index == 0)
            {
                Console.Write(rowId < 10
                    ? (rowId + rowNumberOffset) + " |"
                    : (rowId + rowNumberOffset) + "|"
                );
            }

            int padding = columnWidths[index]
                - cell.Length;

            string cellDisplay = cell;
            if (padding < 0) {
               int newLength = Math.Max(0, cell.Length + padding);
               cellDisplay = cell.Substring(0, newLength);
            }


            Console.Write(
                ' '
            );

            if (rowId == 0)
            {
                ConsoleWriteWithColor(
                    cellDisplay,
                    ConsoleColor.Green
                );
            }
            else
            {
                Console.Write(cellDisplay);
            }
            if (padding > 0) {
                Console.Write(
                    new string(' ', padding)
                );
            }

            Console.Write(
                " |"
            );
        }
        Console.Write("\n");

        if (rowId == 0)
        {
            Console.WriteLine(
                new string(
                    '-',
                    tableWidth - 2
                )
            );
        }
    }



    // bottom border
    Console.WriteLine(
        ' ' +
        new string('-', tableWidth - 3)
    );
}


void ConsoleWriteWithColor(
    string message,
    ConsoleColor foregroundColor,
    ConsoleColor? backgroundColor = null
)
{
    Console.ForegroundColor = foregroundColor;

    if (backgroundColor != null)
    {
        Console.BackgroundColor = backgroundColor.Value;
    }

    Console.Write(message);
    Console.ResetColor();
}



using (MySqlConnection connection = new MySqlConnection(connectionString))
{
    connection.Open();
        
    string tableName = "lamesa_2861101";

    Console.WriteLine("table row count: " + SelectCount(connection, tableName));

    Console.WriteLine("Please input an id you want to update");

    string inputValue = Console.ReadLine();

    int.TryParse(inputValue, out int inputId);

    Console.WriteLine("Please input new name for the row selected (" + inputValue + ")");

    string inputNameValue = Console.ReadLine();


    UpdateRow(
        connection,
        tableName,
        inputValue,        
        new Dictionary<string, string>{
            {  "name", inputNameValue }
        }
    );
    
    
    string query2 = $"SELECT * FROM {tableName}";

    using var cmd2 = new MySqlCommand(query2, connection);

    var reader = cmd2.ExecuteReader();
    while (reader.Read()) {
        List<string> values = new List<string>();
        for (int i = 0; i < reader.FieldCount; i++)
        {
            values.Add(reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString());
        }
        PrettyPrintRows(
            new string[][]{
                new string[]{"Id","Name"},
                values.ToArray()
            }
        );
    }

}
