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

static int[] SearchCsvRows(
    string columnName,
    string value,
    bool partial = false,
    bool caseInsensitive = false
) {
    List<string[]> rows = ReadAllCsvRow();

    int columnNumber = GetCsvColumnNumber(columnName);
    int columnIndex = columnNumber - 1;

    Stack<int> result = new Stack<int>();

    for (int x = 1; x < rows.Count; x++)
    {
        string[] row = rows[x];

        string hayStack = caseInsensitive
            ? row[columnIndex].ToLower()
            : row[columnIndex];

        string needle = caseInsensitive
            ? value.ToLower()
            : value;

        bool rowIsMatch = partial
            ? hayStack.Contains(needle) // do something else
            : hayStack == value;
            
        if (rowIsMatch)
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

    int[] rowIds1 = SearchCsvRows("FirstName", query1, true, true);
    int[] rowIds2 = SearchCsvRows("LastName", query1, true, true);
    int[] rowIds3 = SearchCsvRows("Email", query1, true, true);


    //
    int[] rowIds = rowIds1.Concat(rowIds2).Concat(rowIds3).ToArray();

    Console.WriteLine(rowIds.Length);

    List<string[]> rows = new List<string[]>();

    string[] columns = (new string[] { "#" })
        .Concat(ReadCsvRow(0))
        .ToArray();

    rows.Add(columns);

    foreach (
        int rowId
        in rowIds
    )
    {
        string[] row = ReadCsvRow(rowId);
        rows.Add(
            (new string[] { rowId.ToString() })
                .Concat(row)
                .ToArray()
        );
    }
    PrettyPrintRows(rows.ToArray(), new int[] { }, 0);
}



List<int> searchRowIdsRoutine()
{
    Console.WriteLine("Please input your query:");

    string query1 = Console.ReadLine();

    int[] rowIds1 = SearchCsvRows("FirstName", query1, true, true);
    int[] rowIds2 = SearchCsvRows("LastName", query1, true, true);
    int[] rowIds3 = SearchCsvRows("Email", query1, true, true);


    //
    return rowIds1.Concat(rowIds2).Concat(rowIds3).ToList();
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

string CreateAskForField(string field)
{
    Console.WriteLine(
        "Enter"
        + field +
        " for the new row"
    );
    return Console.ReadLine();
}

void CreateRowRoutine()
{
    string[] columns = ReadCsvRow(0);
    string[] row = new string[columns.Length];

    for (int x = 0; x < columns.Length; x++) {
        string column = columns[x];
        row[x] = CreateAskForField(
            column
        );
    }

    AddToCsv(row);

    Console.WriteLine("Successfully added row");

}

void ConsoleWriteWithColor(
    string message,
    ConsoleColor foregroundColor,
    ConsoleColor? backgroundColor = null
) {
    Console.ForegroundColor = foregroundColor;

    if (backgroundColor != null) {
        Console.BackgroundColor = backgroundColor.Value;
    }

    Console.Write(message);
    Console.ResetColor();
}

void PrettyPrintRows(
    string[][] rows,
    int[] highlightRowIds,
    int selectedRowId,
    bool includeRowNumbers = false
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

    int tableWidth = totalLength + (4 * colMaxLengths.Length);

    if (includeRowNumbers)
    {
        tableWidth += 2;
    }

    int vacantSpace = Console.WindowWidth - tableWidth;

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

    tableWidth += vacantSpace -2;

    Console.WriteLine(
        ' ' +
        new string('-', tableWidth - 4)
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
                Console.Write(
                    rowId == 0
                    ? "#|"
                    : rowId + "|"
                );
            }

            int padding = columnWidths[index]
                - cell.Length;


            Console.Write(
                ' '
            );

            if (rowId == 0)
            {
                ConsoleWriteWithColor(
                    cell,
                    ConsoleColor.Green
                );
            }
            else if (
                highlightRowIds.Contains(rowId)
            )
            {
                ConsoleWriteWithColor(
                    cell,
                    ConsoleColor.Yellow
                );
            }

            else if (
                selectedRowId == rowId
            )
            {
                ConsoleWriteWithColor(
                    cell,
                    ConsoleColor.Green
                );
            }
            else
            {
                Console.Write(cell);
            }
            Console.Write(
                new string(' ', padding)
            );

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




    Console.WriteLine(
        ' ' +
        new string('-', tableWidth - 4)
    );
}

string UpdateAskForField(string field, string currentValue)
{
    Console.WriteLine(
        "Enter new: "
        + field
        + " current: "
        + currentValue
        + "; press enter to not update"
    );
    string input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
    {
        input = currentValue;
    }
    
    return input;
}

void UpdateRowRoutine(int rowId)
{
    if (rowId == 0) {
        Console.WriteLine("Error: no row selected");
    }
    string[] row = ReadCsvRow(rowId);
    string[] columns = ReadCsvRow(0);

    for (int x = 0; x < columns.Length; x++){
        string column = columns[x];
        UpdateCsvRow(
            rowId,
            column,
            UpdateAskForField(column, row[x])
        );
    }

    Console.WriteLine("Successfully update row");

}

void DeleteRowRoutine(int rowId)
{

    Console.WriteLine("Are you sure you want to delete this row?");
    string confirmationInput = Console.ReadLine();

    bool confirmed = confirmationInput == "yes"
        || confirmationInput == "y"
        || confirmationInput == "Y"
        || confirmationInput == "YES";

    if (confirmed)
    {
        DeleteCsvRow(rowId);
        Console.WriteLine("Successfully Deleted Row");
    }
    else
    {
        Console.WriteLine("Delete cancelled");
    }
}



List<int> highlightRowIds = new List<int>();

int selectedRowId = 0;

while (true)
{

    string[][] rows = ReadAllCsvRow().ToArray();

    Console.Clear();
    PrettyPrintRows(rows, highlightRowIds.ToArray(), selectedRowId, true);

    Console.WriteLine("Press 's' to search; 'a' to add; 'u' to update;ESC to exit");
    if (selectedRowId != 0)
    {
        Console.WriteLine("Selected Row: " + selectedRowId);
    }
    
    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);


    if (keyInfo.KeyChar == 's')
    {
        highlightRowIds = searchRowIdsRoutine();
        continue;
    }

    highlightRowIds = new List<int>();


    if (keyInfo.KeyChar == 'a')
    {
        CreateRowRoutine();
        continue;
    }



    if (keyInfo.KeyChar == 'u')
    {
        UpdateRowRoutine(selectedRowId);
        continue;
    }

    
    if (keyInfo.KeyChar == 'd')
    {
        DeleteRowRoutine(selectedRowId);
        continue;
    }


    if (keyInfo.Key == ConsoleKey.UpArrow)
    {
        selectedRowId = Math.Max(0, selectedRowId - 1);
        continue;
    }

    if (keyInfo.Key == ConsoleKey.DownArrow)
    {
        selectedRowId = Math.Min(
            rows.Length - 1,
            selectedRowId + 1
        );
        continue;
    }

    if (keyInfo.Key == ConsoleKey.Escape)
    {
        Console.WriteLine("\nEscape pressed. Exiting...");
        break;
    }

    Console.WriteLine("Invalid command");
}