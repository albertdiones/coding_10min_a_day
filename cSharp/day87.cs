using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

const string DbFile = "__db.csv";

public static int AddToCsv(string[] rowValues)
{
    // Replace inner quotes and wrap each value in quotes
    for (int i = 0; i < rowValues.Length; i++)
    {
        rowValues[i] = "\"" + rowValues[i].Replace("\"", "\\\"") + "\"";
    }

    // Join row values into CSV format
    string row = string.Join(",", rowValues) + "\n";

    // Append row to file (create if not exists)
    File.AppendAllText(DbFile, row, Encoding.UTF8);

    // Read entire file and count number of newline characters
    string rows = File.ReadAllText(DbFile);
    return rows.Count(c => c == '\n');
}

public static string[] TrimQuotes(string[] values)
{
    return values.Select(v => v.Trim('"')).ToArray();
}

public static string[] ReadCsvRow(int rowId)
{
    string[] rows = File.ReadAllLines(DbFile);
    if (rowId < 0 || rowId >= rows.Length)
        throw new IndexOutOfRangeException("Row ID out of range");

    return TrimQuotes(rows[rowId].Split(","));
}

public static int GetCsvRowCount()
{
    return File.ReadAllLines(DbFile).Length;
}

public static List<string[]> ReadAllCsvRow()
{
    int numRows = GetCsvRowCount();
    Console.WriteLine("numRows " + numRows);

    var rows = new List<string[]>(numRows);
    for (int x = 0; x < numRows; x++)
    {
        rows.Add(ReadCsvRow(x));
    }
    return rows;
}

public static int GetCsvColumnNumber(string columnName)
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

public static string GetCSVCellValue(int rowId, int columnNumber) {
    string[] row = ReadCsvRow(rowId);


    return row[columnNumber - 1];
}

Console.WriteLine(GetCSVCellValue(3, 2));