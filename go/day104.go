package main

import (
	"fmt"
	"os"
	"strings"
	"bytes"
	"bufio"
)


func setCsvToBlank() {
	dbFile := "__db.csv"

	// Open file with truncate flag to clear contents
	fileHandle, err := os.OpenFile(dbFile, os.O_CREATE|os.O_WRONLY|os.O_TRUNC, 0644)
	if err != nil {
		panic(err)
	}
	defer fileHandle.Close()

	// Nothing to write, file is now blank
}

func addToCsv(rowValues []string) int {
	dbFile := "__db.csv"

	// Open or create file in append mode
	fileHandle, err := os.OpenFile(dbFile, os.O_APPEND|os.O_CREATE|os.O_WRONLY, 0644)
	if err != nil {
		panic(err)
	}
	defer fileHandle.Close()

	// map-style replacement
	for i, value := range rowValues {
		rowValues[i] = `"` + strings.ReplaceAll(value, `"`, `\"`) + `"`
	}


	// Join row values into CSV format
	row := strings.Join(rowValues, ",") + "\n"

	// Write row
	if _, err := fileHandle.Write([]byte(row)); err != nil {
		panic(err)
	}

	// Read entire file to count lines
	rows, err := os.ReadFile(dbFile)
	if err != nil {
		panic(err)
	}

	// Count number of newline characters = number of rows
	return bytes.Count(rows, []byte("\n"))
}

func trimRowValue(value string) string {
	return strings.Trim(
		strings.TrimSpace(value), 
		`"`,
	)
}


func trimRowValues(values []string) []string {
	unquoted := values;
	for i,value := range values {
		unquoted[i] = trimRowValue(value) // removes leading & trailing "
	}
	return unquoted;
}

func readCsvRow(rowId int) []string {
	dbFile := "__db.csv"
	// Read entire file to count lines
	rowBytes, err := os.ReadFile(dbFile)
	if err != nil {
		panic(err)
	}
	rows := strings.Split(string(rowBytes), "\n")
	return trimRowValues(strings.Split(rows[rowId],","));
}

func getCsvRowCount() int {
	dbFile := "__db.csv"
	// Read entire file to count lines
	rowBytes, err := os.ReadFile(dbFile)
	if err != nil {
		panic(err)
	}
	rows := strings.Split(string(rowBytes), "\n")
	return len(rows);
}

func readAllCsvRow() [][]string {

	numRows := getCsvRowCount()

	rows := make([][]string, numRows);
	for x := 0; x < numRows; x++ {
		rows[x] = readCsvRow(x);
	}
	return rows;
}

func getCsvColumnNumber(columnName string) int {
	columns := readCsvRow(0);

	for i,column := range columns {
		column = trimRowValue(column);
		if column == columnName {
			return i+1;
		}
	}
	panic("Column not found");
}

func updateCsvRow( rowId int, columnName string, value string) {
	rows := readAllCsvRow();

	columnNumber := getCsvColumnNumber(columnName);

	rows[rowId][columnNumber-1] = value;

	setCsvToBlank();
	// map-style replacement
	for _, row := range rows {
		addToCsv(row);
	}
}



func searchCsv(columnName string, value string) []int {
	rows := readAllCsvRow()

	columnNumber := getCsvColumnNumber(columnName)
	columnIndex := columnNumber - 1
	var rowIds []int

	for rowId := 1; rowId < len(rows); rowId++ {
		row := rows[rowId]
		if row[columnIndex] == value {
			rowIds = append(rowIds, rowId)
		}
	}
	return rowIds
}

func deleteCSVRow(deleteRowID int) {
    rows := readAllCsvRow()
    setCsvToBlank()

    for rowID, row := range rows {
        if rowID == deleteRowID {
            continue
        }
        addToCsv(row)
    }
}

func searchRoutine() {
	reader := bufio.NewReader(os.Stdin)
	fmt.Println("Please input your query:")

	query1, _ := reader.ReadString('\n')
	query1 = trimRowValue(query1)

	rowIds1 := searchCsv("FirstName", query1)
	rowIds2 := searchCsv("LastName", query1)
	rowIds3 := searchCsv("Email", query1)

	// Combine all row IDs
	rowIds := append(append(rowIds1, rowIds2...), rowIds3...)

	fmt.Println(len(rowIds))

	for _, rowId := range rowIds {
		row := readCsvRow(rowId)
		fmt.Println(strings.Join(row, ","))
	}
}



func findFirstCsvRow(columnName string, value string) *int {
	rows := readAllCsvRow()

	columnNumber := getCsvColumnNumber(columnName)
	columnIndex := columnNumber - 1

	for rowId := 1; rowId < len(rows); rowId++ {
		row := rows[rowId]
		if row[columnIndex] == value {
			return &rowId
		}
	}
	return nil
}



func addNewRowRoutine() {

	fmt.Println(
		"Input email:",
	);

	reader := bufio.NewReader(os.Stdin)
	email1, _ := reader.ReadString('\n')
	email1 = strings.TrimSpace(email1)


	rowId := findFirstCsvRow("Email", email1);

	fmt.Println(
		"input firstname:",
	);


	name1, _ := reader.ReadString('\n')
	name1 = strings.TrimSpace(name1)
	
	fmt.Println(
		"input lastname:",
	);


	lastName1, _ := reader.ReadString('\n')
	lastName1 = strings.TrimSpace(lastName1)

	if rowId != nil {
		updateCsvRow(*rowId, "FirstName", name1)
		updateCsvRow(*rowId, "LastName", lastName1)
		return;
	}

	addToCsv([]string{email1, name1, lastName1});

	fmt.Println("Email/Name successfully added to csv")
	
}

// --- main() equivalent to the C# while loop section ---
func main() {
	rows := readAllCsvRow()

	if len(rows) == 0 {
		return
	}

	rowMaxLengths := make([]int, len(rows[0]))

	// Compute max cell width per column
	for _, row := range rows {
		for i, cell := range row {
			if len(cell) > rowMaxLengths[i] {
				rowMaxLengths[i] = len(cell)
			}
		}
	}

	totalLength := 0
	for _, l := range rowMaxLengths {
		totalLength += l
	}

	fmt.Printf(" %s\n", strings.Repeat("-", totalLength+3+len(rowMaxLengths)))

	for rowId, row := range rows {
		fmt.Print("| ")
		for i, cell := range row {
			padding := rowMaxLengths[i] - len(cell)
			fmt.Print(cell + strings.Repeat(" ", padding) + " |")
		}
		fmt.Print("\n")

		if rowId == 0 {
			fmt.Println(strings.Repeat("-", totalLength+5+len(rowMaxLengths)))
		}
	}

	fmt.Printf(" %s\n", strings.Repeat("-", totalLength+3+len(rowMaxLengths)))

	reader := bufio.NewReader(os.Stdin)
	for {
		fmt.Print("Press 's' to search, 'a' to add or ESC to exit: ")
		input, _ := reader.ReadString('\n')
		input = strings.TrimSpace(input)

		if input == "s" {
			searchRoutine()
			continue
		}

		if input == "a" {
			addNewRowRoutine()
			continue
		}

		if strings.ToLower(input) == "esc" {
			fmt.Println("Escape pressed. Exiting...")
			break
		}

		fmt.Println("Invalid command")
	}
}
