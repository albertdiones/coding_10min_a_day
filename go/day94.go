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


func trimRowValues(values []string) []string {
	unquoted := values;
	for i,value := range values {
		unquoted[i] = strings.Trim(
			strings.TrimSpace(value), 
			`"`,
		) // removes leading & trailing "
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
		column = strings.TrimSpace(column);
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


func main() {

	fmt.Println(
		"input the email address:",
	);

	reader := bufio.NewReader(os.Stdin)
	email1, _ := reader.ReadString('\n')
	email1 = strings.TrimSpace(email1)

	rowId := findFirstCsvRow("Email", email1);

	if rowId != nil {
		panic("Email is duplicate(error)");
	}

	fmt.Println(
		"input name:",
	);


	name1, _ := reader.ReadString('\n')
	name1 = strings.TrimSpace(name1)

	addToCsv([]string{email1, name1});

	fmt.Println("Email/Name successfully added to csv")
	
}