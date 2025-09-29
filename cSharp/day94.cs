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
    char contentLastCharacter = contents[^1];

    if (contentLastCharacter != '\n') {
        row = '\n' + row;
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

Console.WriteLine("Please input your email");

string email1 = Console.ReadLine();

int? rowId = FindFirstCsvRow("Email", email1);

if (rowId != null)
{
    Console.WriteLine("Error: duplicate email");
}
else
{
    Console.WriteLine("Please input name");

    string name1 = Console.ReadLine();
    
    AddToCsv([email1, name1]);
}