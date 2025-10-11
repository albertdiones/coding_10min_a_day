                                                                                            using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

const string DbFile = "__db.csv";

static int AddToCsv(string[] rowValues)
{
    // Replace inner quotes and wrap each value in quotes
    for (int i = 0; i < rowValues.Length; i++)
    {
        rowValues[i] = "\"" + rowValues[i].Replace("\"", "\\\"") + "\"";
    }
    // Join row values into CSV format
    string row = string.Join(",", rowValues) + "\n";

    // Read entire file and count number of newline characters
    string contents = File.ReadAllText(DbFile);

    if (contents.Length > 0) {
        char contentLastCharacter = contents[^1];

        if (contentLastCharacter != '\n') {
            row = '\n' + row;
        }
    }

    // Append row to file (create if not exists)
        File.AppendAllText(DbFile, row, Encoding.UTF8);

    return contents.Count(c => c == '\n');
}

static string[] TrimQuotes(string[] values)
{
    return values.Select(v => v.Trim('"')).ToArray();
}

static string[] ReadCsvRow(int rowId)
{
    string[] rows = File.ReadAllLines(DbFile);
    if (rowId < 0 || rowId >= rows.Length)
        throw new IndexOutOfRangeException("Row ID out of range");

    return TrimQuotes(rows[rowId].Split(","));
}

static int GetCsvRowCount()
{
    return File.ReadAllLines(DbFile).Length;
}

static List<string[]> ReadAllCsvRow()
{
    int numRows = GetCsvRowCount();;

    var rows = new List<string[]>(numRows);
    for (int x = 0; x < numRows; x++)
    {
        rows.Add(ReadCsvRow(x));
    }
    return rows;
}

static int GetCsvColumnNumber(string columnName)
{
    string[] columns = ReadCsvRow(0);
    for (int i = 0; i < columns.Length; i++)
    {
        if (columns[i].Trim() == columnName)
        {
            return i + 1; // 1-based index like Go version
        }
    }
    throw new Exception("Column not found");
}

static string GetCSVCellValue(int rowId, int columnNumber) {
    string[] row = ReadCsvRow(rowId);


    return row[columnNumber - 1];
}

static int[] SearchCsvRows(string columnName, string value) {
    List<string[]> rows = ReadAllCsvRow();

    int columnNumber = GetCsvColumnNumber(columnName);
    int columnIndex = columnNumber - 1;

    Stack<int> result = new Stack<int>();

    for (int x = 1; x < rows.Count; x++)
    {
        string[] row = rows[x];
        if (row[columnIndex] == value)
        {
            result.Push(x);
        }
    }
    return result.ToArray();
}

static int? FindFirstCsvRow(string columnName, string value)
{
    List<string[]> rows = ReadAllCsvRow();

    int columnNumber = GetCsvColumnNumber(columnName);
    int columnIndex = columnNumber - 1;

    Stack<int> result = new Stack<int>();

    for (int rowId = 1; rowId < rows.Count; rowId++)
    {
        string[] row = rows[rowId];
        if (row[columnIndex] == value)
        {
            return rowId;
        }
    }
    return null;
}

void SetCsvToBlank()
{
    string dbFile = "__db.csv";

    try
    {
        // Open or create the file, overwrite existing content (truncate)
        using (var fileStream = new FileStream(dbFile, FileMode.Create, FileAccess.Write))
        {
            // Nothing to write — this effectively clears the file
        }
    }
    catch (Exception ex)
    {
        throw new Exception("Failed to clear CSV file", ex);
    }
}


void DeleteCsvRow(int deleteRowId)
{
    var rows = ReadAllCsvRow();
    SetCsvToBlank();

    for (int rowId = 0; rowId < rows.Count; rowId++)
    {
        var row = rows[rowId];
        if (rowId == deleteRowId)
        {
            continue;
        }
        AddToCsv(row);
    }
}

void SearchRoutine()
{
    Console.WriteLine("Please input your query:");

    string query1 = Console.ReadLine();

    int[] rowIds1 = SearchCsvRows("FirstName", query1);
    int[] rowIds2 = SearchCsvRows("LastName", query1);
    int[] rowIds3 = SearchCsvRows("Email", query1);


    //
    int[] rowIds = rowIds1.Concat(rowIds2).Concat(rowIds3).ToArray();

    Console.WriteLine(rowIds.Length);

    foreach (
        int rowId
        in rowIds
    )
    {
        string[] row = ReadCsvRow(rowId);
        Console.WriteLine(
            String.Join(
                ',',
                row
                )
        );
    }
}

void UpdateCsvRow(
    int rowId,
    string columnName,
    string value)
{
    var rows = ReadAllCsvRow();
    int columnNumber = GetCsvColumnNumber(columnName);
rows[rowId][columnNumber - 1] = value;

    SetCsvToBlank();

    foreach (var row in rows)
    {
        AddToCsv(row);
    }
}




void AddRowRoutine()
{
    Console.WriteLine("Input email:");

    string email1 = Console.ReadLine();

    int? rowId = FindFirstCsvRow("Email", email1);

    Console.WriteLine("Input first name:");
    string firstName = Console.ReadLine();

    Console.WriteLine("Input last name:");
    string lastName = Console.ReadLine();

    if (rowId.HasValue)
    {
        UpdateCsvRow(rowId.Value, "firstName", firstName);
        UpdateCsvRow(rowId.Value, "lastName", lastName);
        return;
    }

    AddToCsv(new[] { email1, firstName, lastName });

    Console.WriteLine("Email/Name successfully added to CSV");
}

string[][] rows = ReadAllCsvRow().ToArray();

if (rows.Length == 0)
{
    return;
}

int[] rowMaxLengths = new int[rows[0].Length];

foreach (string[] row in rows)
{
    foreach (
        var (index, cell)
            in row.Select(
                (arrayValue, Arrayindex)
                    => (Arrayindex, arrayValue)
            ))
    {
        rowMaxLengths[index] = Math.Max(
            rowMaxLengths[index],
            cell.Length
        );
    }
}

int totalLength = rowMaxLengths.Sum();

Console.WriteLine(
    ' ' +
    new string('-', totalLength + 3 + rowMaxLengths.Length)
);

foreach (
    var (rowId, row)
        in rows.Select(
            (arrayValue, Arrayindex)
                => (Arrayindex, arrayValue)
        )
)
{

    Console.Write("| ");
    foreach (
        var (index, cell)
            in row.Select(
                (arrayValue, Arrayindex)
                    => (Arrayindex, arrayValue)
            )
    )
    {
        int padding = rowMaxLengths[index] - cell.Length;
        Console.Write(cell + new string(' ', padding) + " |");
    }
    Console.Write("\n");

    if (rowId == 0) {
        Console.WriteLine(
            new string(
                '-',
                totalLength
                + 5
                + rowMaxLengths.Length
            )
        );
    }
}




Console.WriteLine(
    ' ' +
    new string('-', totalLength + 3 + rowMaxLengths.Length)
);

while (true)
{

    Console.WriteLine("Press 's' to search; 'a' to add; ESC to exit");
    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

    if (keyInfo.KeyChar == 's')
    {
        SearchRoutine();
        continue;
    }

    
    if (keyInfo.KeyChar == 'a')
    {
        AddRowRoutine();
        continue;
    }



    if (keyInfo.Key == ConsoleKey.Escape)
    {
        Console.WriteLine("\nEscape pressed. Exiting...");
        break;
    }

    Console.WriteLine("Invalid command");
}