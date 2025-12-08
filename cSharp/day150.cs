                                                                                            using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading;

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
        /*        
        var row = ReadCsvRow(x);
        var newRow = new[] { x.ToString() }.Concat(row).ToArray();
        if (x == 0)
        {
            var row = ReadCsvRow(x);
            var newRow = new[] { "#" }.Concat(row).ToArray();
            
        }
        rows.Add(newRow);
        rows.Add(ReadCsvRow(x));
        */
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
    return -1;
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

    Stack<int> result = new Stack<int>();

    if (columnNumber == -1)
    {
        return result.ToArray();
    }

    int columnIndex = columnNumber - 1;

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

    int[] rowIds1 = SearchCsvRows("Name", query1, true, true);
    int[] rowIds3 = SearchCsvRows("Email", query1, true, true);


    //
    int[] rowIds = rowIds1.Concat(rowIds3).ToArray();

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

void PrettyPrintRows(
    string[][] rows,
    int[] highlightRowIds,
    int selectedRowId,
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


            if (
                selectedColumnIndex != null
                && selectedColumnIndex.Value == index
                && selectedRowId == rowId
            )
            {
                ConsoleWriteWithColor(
                    cellDisplay,
                    ConsoleColor.Yellow
                );
            }
            else if (
                selectedColumnIndex != null
                && selectedColumnIndex.Value == index
                && selectedRowId == 0
            )
            {
                ConsoleWriteWithColor(
                    cellDisplay,
                    ConsoleColor.Cyan
                );
            }
            else if (rowId == 0)
            {
                ConsoleWriteWithColor(
                    cellDisplay,
                    ConsoleColor.Green
                );
            }
            else if (
                highlightRowIds.Contains(rowId)
            )
            {
                ConsoleWriteWithColor(
                    cellDisplay,
                    ConsoleColor.Yellow
                );
            }
            else if (
                selectedRowId == rowId
                && selectedColumnIndex == null
            )
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

void AddNewColumnRoutine()
{
    string[][] rows = ReadAllCsvRow().ToArray();


    Console.WriteLine(
        "Please input the name of the new column"
    );
    string columnNameInput = Console.ReadLine();

    string[] newColumns = new string[rows[0].Length + 1];

    rows[0].CopyTo(newColumns, 0);

    newColumns[newColumns.Length - 1] = columnNameInput;

    SetCsvToBlank();
    AddToCsv(newColumns);

    for (int x = 1; x < rows.Length; x++)
    {
        string[] newRow = new string[rows[x].Length + 1];
        rows[x].CopyTo(newRow, 0);
        newRow[newRow.Length - 1] = "";
        AddToCsv(newRow);
    }

}

void DeleteColumn(string columnName)
{
    string[][] rows = ReadAllCsvRow().ToArray();

    int columnIndex = GetCsvColumnNumber(columnName) - 1;
    SetCsvToBlank();
    for (int x = 0; x < rows.Length; x++)
    {
        var newRowList = rows[x].ToList();
        newRowList.RemoveAt(columnIndex);

        string[] newRow = newRowList.ToArray();
        AddToCsv(newRow);
    }
}

void DeleteColumnRoutine(string selectedColumnName)
{

    Console.WriteLine(
        "Are you sure you want to delete this column? "
        + selectedColumnName
    );
    string confirmationInput = Console.ReadLine();

    bool confirmed = confirmationInput == "yes"
        || confirmationInput == "y"
        || confirmationInput == "Y"
        || confirmationInput == "YES";

    if (confirmed)
    {
        DeleteColumn(selectedColumnName);
        Console.WriteLine("Successfully Deleted Column");
    }
    else
    {
        Console.WriteLine("Delete column cancelled");
    }
}

void renameColumn(string oldName, string newName)
{
    UpdateCsvRow(0, oldName, newName);
}

void RenameColumnRoutine(string selectedColumnName)
{

    Console.WriteLine("Input the new name of the column " + selectedColumnName);

    string newColumnNameInput = Console.ReadLine();

    int columnNumber = GetCsvColumnNumber(newColumnNameInput);

    if (columnNumber > 0)
    {
        throw new Exception("Column name already exists!");
    }

    renameColumn(selectedColumnName, newColumnNameInput);
}

string[][] sortRows(string[][] rows, string columnName, bool isAscending = true)
{
    int columnNumber = GetCsvColumnNumber(columnName);
    int columnIndex = columnNumber - 1;

    IOrderedEnumerable<string[]> rowsEnum;
    string[][] actualRows;

    if (isAscending == false)
    {
        actualRows = rows
        .Skip(1) // skip header
        .OrderBy(
            r =>
                r[columnIndex], StringComparer.OrdinalIgnoreCase
        ).Reverse().ToArray();
    }
    else
    {
        actualRows = rows
        .Skip(1) // skip header
        .OrderBy(
            r =>
                r[columnIndex], StringComparer.OrdinalIgnoreCase
        ).ToArray();
    }


    return (new[] { rows[0] }).Concat(actualRows).ToArray();
}



List<int> highlightRowIds = new List<int>();

int selectedRowId = 0;

int? selectedColumnIndex = null;

bool columnSelectionMode = false;

string selectedColumnName = "";


//renameColumn("xxxxxx", "FullName");

int perPage = 15;
int page = 1;
int pageOffset = 0;


bool? sortAscending = null;

string[] clipboardRow = [];
int? deleteRowWhenPasted = null;


while (true)
{


    bool cellSelectionMode = selectedRowId != 0
        && selectedColumnIndex != null;
    bool rowSelectionMode = selectedRowId != 0
        && selectedColumnIndex == null;
    /*
    bool columnSelectionMode = selectedRowId == 0
        && selectedColumnIndex != null;*/

    string[][] allRows = ReadAllCsvRow().ToArray();


    if (sortAscending != null) {
        if (sortAscending.Value)
        {
            allRows = sortRows(allRows, "Name", true);
        }
        else
        {

            allRows = sortRows(allRows, "Name", false);
        }
    }

    string[] columns = allRows[0];
    int numRows = allRows.Length - 1;
    
    int rowNumberOffset = pageOffset + ((page - 1) * 15);

    string[][] pageRows = allRows
        .Skip(1 + rowNumberOffset)
        .Take(perPage)
        .ToArray();

    string[][] rows = (new[] { columns }).Concat(pageRows).ToArray();


    Console.Clear();
    PrettyPrintRows(
        rows,
        highlightRowIds.ToArray(),
        selectedRowId,
        selectedColumnIndex,
        true,
        rowNumberOffset
    );

    int maxPages = (int)Math.Ceiling((double)numRows / perPage);

    Console.WriteLine("Page 1/" + maxPages + " (" + numRows + " total)");

    Console.Write("Options:");

    if (columnSelectionMode == false)
    {
        Console.Write("  [S] Search");
        Console.Write("  [A] Add new row");
        Console.Write("  [U] Update selected row");
        Console.Write("  [c] Add new column");
        Console.Write("  [Del] Delete selected row");
        Console.Write("  [Esc] Exit program \n");
    }
    else
    {
        Console.Write("  [R] rename column");
        Console.Write("  [Esc] Exit column selection\n");
    }
    if (selectedRowId != 0)
    {
        Console.WriteLine("Selected Row: " + selectedRowId);
    }
    if (clipboardRow != null)
    {
        Console.WriteLine("Copied Row: " + string.Join(",", clipboardRow));
    }

    DateTime start = DateTime.Now;
    ConsoleKeyInfo keyInfo = default;
    bool keyPressed = false;
    while ((DateTime.Now - start).TotalSeconds < 2)
    {
        if (Console.KeyAvailable)
        {
            keyInfo = Console.ReadKey(intercept: true);
            keyPressed = true;
            break;
        }
        Thread.Sleep(30);
    }

    if (!keyPressed)
    {
        continue;
    }


    if (selectedColumnIndex != null && columnSelectionMode)
    {
        selectedColumnName = rows[0][selectedColumnIndex.Value];
    }
    if (cellSelectionMode)
    {
         if (keyInfo.Key == ConsoleKey.Delete)
        {
            UpdateCsvRow(selectedRowId, selectedColumnName, "");
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

        if (
            keyInfo.Key == ConsoleKey.LeftArrow
        )
        {
            selectedColumnIndex = Math.Max(0, selectedColumnIndex.Value - 1);
            continue;
        }

        if (
            keyInfo.Key == ConsoleKey.RightArrow
            ||
            (
               keyInfo.Key == ConsoleKey.Tab
            )
        )
        {
            selectedColumnIndex = Math.Min(
                rows[0].Length - 1,
                selectedColumnIndex.Value + 1
            );
            continue;
        }

        
        string cellValue = rows[selectedRowId][selectedColumnIndex.Value];

        

        if (keyInfo.Key == ConsoleKey.Backspace)
        {
            int newLength = cellValue.Length - 1;
            cellValue = cellValue.Substring(0, newLength);
            
            UpdateCsvRow(
                selectedRowId,
                selectedColumnName,
                cellValue
            );
            continue;
        }

        

        if (
            keyInfo.Key == ConsoleKey.Escape
            || keyInfo.Key == ConsoleKey.Enter
        ) {
            selectedColumnIndex = null;
            selectedColumnName = null;
            cellSelectionMode = false;
            continue;
        }


        // new tech debt, doesn't work
        if (keyInfo.KeyChar == null)
        {
            continue;
        }


        UpdateCsvRow(
            selectedRowId,
            selectedColumnName,
            cellValue + keyInfo.KeyChar
        );

    }
    // column navigation mode
    else if (selectedColumnIndex != null && columnSelectionMode)
    {
        if (keyInfo.Key == ConsoleKey.LeftArrow)
        {
            selectedColumnIndex = Math.Max(0, selectedColumnIndex.Value - 1);
            continue;
        }

        if (keyInfo.Key == ConsoleKey.RightArrow)
        {
            selectedColumnIndex = Math.Min(
                rows[0].Length - 1,
                selectedColumnIndex.Value + 1
            );
            continue;
        }

        if (keyInfo.KeyChar == 'R')
        {
            RenameColumnRoutine(rows[0][selectedColumnIndex.Value]);
            continue;
        }



        if (keyInfo.KeyChar == 'D')
        {
            DeleteColumnRoutine(rows[0][selectedColumnIndex.Value]);
            continue;
        }


        if (keyInfo.Key == ConsoleKey.Escape)
        {
            columnSelectionMode = false;
            selectedColumnIndex = null;
            selectedColumnName = "";
            continue;
        }


        if (keyInfo.Key == ConsoleKey.UpArrow)
        {
            if (sortAscending == null)
            {
                sortAscending = false;
            }
            else if (sortAscending.Value == true)
            {
                sortAscending = null;
            }
            else
            {
                sortAscending = true;
            }
            continue;
        }

        if (keyInfo.Key == ConsoleKey.DownArrow)
        {
            if (sortAscending == null)
            {
                sortAscending = true;
            }
            else if (sortAscending.Value == true)
            {
                sortAscending = false;
            }
            else
            {
                sortAscending = null;
            }
            continue;
        }
    }
    else if (rowSelectionMode)
    {

        if (keyInfo.KeyChar == 'u')
        {
            UpdateRowRoutine(selectedRowId);
            continue;
        }
        

        if (keyInfo.KeyChar == 'I')
        {
            allRows = ReadAllCsvRow().ToArray();
            string[][] newAllRows = new string[allRows.Length + 1][];

            // Copy elements before index
            Array.Copy(allRows, 0, newAllRows, 0, selectedRowId);

            // Insert the new value
            newAllRows[selectedRowId] = new string[]{"",""};

            // Copy the rest (shifted by 1)
            Array.Copy(
                allRows,
                 selectedRowId,
                  newAllRows,
                   selectedRowId + 1,
                    allRows.Length - selectedRowId
            );


            SetCsvToBlank();
            foreach (var row in newAllRows)
            {
                AddToCsv(row);
            }            

            continue;
        }
        
        
        if (keyInfo.Key == ConsoleKey.UpArrow)
        {
            int targetRowId = selectedRowId - 1;

            if (targetRowId <= 0)
            {
                targetRowId = 1;
                pageOffset = Math.Max(0, pageOffset - 1);
            }
            selectedRowId = targetRowId;
            continue;
        }

        if (keyInfo.Key == ConsoleKey.DownArrow)
        {

            int targetRowId = selectedRowId + 1;

            if (targetRowId >= rows.Length)
            {
                targetRowId = rows.Length - 1;
                pageOffset = Math.Min(allRows.Length - perPage, pageOffset + 1);
            }
            selectedRowId = targetRowId;


            continue;
        }

        if (keyInfo.Key == ConsoleKey.Escape)
        {
            selectedRowId = 0;
            continue;
        }



        if (keyInfo.Key == ConsoleKey.Delete)
        {
            DeleteRowRoutine(selectedRowId);
            continue;
        }


        if (keyInfo.Key == ConsoleKey.F2)
        {
            columnSelectionMode = true;
            selectedColumnIndex = 0;
            continue;
        }

        if (keyInfo.Key == ConsoleKey.LeftArrow)
        {
            columnSelectionMode = true;
            if (selectedColumnIndex == null)
            {
                selectedColumnIndex = 0;
            }
            else {
                selectedColumnIndex = Math.Max(0, selectedColumnIndex.Value - 1);
            }
            continue;
        }

        if (keyInfo.Key == ConsoleKey.RightArrow)
        {
            columnSelectionMode = true;
            if (selectedColumnIndex == null)
            {
                selectedColumnIndex = 1;
            }
            else {
                selectedColumnIndex = Math.Min(
                    rows[0].Length - 1,
                    selectedColumnIndex.Value + 1
                );
            }
            continue;
        }

        

        if (keyInfo.Key == ConsoleKey.PageUp)
        {
            page = Math.Max(1, page - 1);
            selectedRowId = 0;
            rowSelectionMode = false;
            continue;
        }

        if (keyInfo.Key == ConsoleKey.PageDown)
        {
            page = Math.Min(
                maxPages,
                page + 1
            );
            
            selectedRowId = 0;
            rowSelectionMode = false;

            continue;
        }

        

        if (keyInfo.KeyChar == 'C' 
            && ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0)
        )
        {
            clipboardRow = allRows[selectedRowId];
            deleteRowWhenPasted = null;
            continue;
        }

        

        if (keyInfo.KeyChar == 'X' 
            && ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0)
        )
        {
            clipboardRow = allRows[selectedRowId];
            deleteRowWhenPasted = selectedRowId;
            continue;
        }


        

        if (
            clipboardRow.Length > 0 &&
            keyInfo.KeyChar == 'V' 
            && ((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0)
        )
        {

            if (deleteRowWhenPasted != null)
            {
                DeleteCsvRow(deleteRowWhenPasted.Value);
                
                if (selectedRowId > deleteRowWhenPasted.Value)
                {
                    selectedRowId--;
                }
            }

            allRows = ReadAllCsvRow().ToArray();
            string[][] newAllRows = new string[allRows.Length + 1][];

            // Copy elements before index
            Array.Copy(allRows, 0, newAllRows, 0, selectedRowId);

            // Insert the new value
            newAllRows[selectedRowId] = new string[]{"",""};

            // Copy the rest (shifted by 1)
            Array.Copy(
                allRows,
                 selectedRowId,
                  newAllRows,
                   selectedRowId + 1,
                    allRows.Length - selectedRowId
            );


            SetCsvToBlank();
            foreach (var row in newAllRows)
            {
                AddToCsv(row);
            }

            
            for (
                int y = 0;
                y < columns.Length;
                y++
            ) {
                UpdateCsvRow(selectedRowId, columns[y], clipboardRow[y]);
            }
            
            continue;
        }
    }
    // default mode
    else
    {




        if (keyInfo.Key == ConsoleKey.F2)
        {
            columnSelectionMode = true;
            selectedColumnIndex = 0;
            rowSelectionMode = true;
            selectedRowId = 1;
            continue;
        }


        if (
            keyInfo.KeyChar == 's'
            ||
            (
                (keyInfo.Key == ConsoleKey.F)
                && ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0)
            )
        )
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

        if (keyInfo.KeyChar == 'c')
        {
            AddNewColumnRoutine();
            continue;
        }


        if (keyInfo.KeyChar == 'C')
        {
            columnSelectionMode = true;
            selectedColumnIndex = 0;
            selectedRowId = 0;
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



        if (keyInfo.Key == ConsoleKey.PageUp)
        {
            page = Math.Max(1, page - 1);
            continue;
        }

        if (keyInfo.Key == ConsoleKey.PageDown)
        {
            page = Math.Min(
                maxPages,
                page + 1
            );
            continue;
        }
    }

    if (keyInfo.Key == ConsoleKey.Escape)
    {
        Console.WriteLine("\nEscape pressed. Exiting...");
        break;
    }

    Console.WriteLine("Invalid command");
}