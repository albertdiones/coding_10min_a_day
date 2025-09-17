package main

import (
	"fmt"
	"os"
	"strings"
	"bytes"
)

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


func trimQuotes(values []string) []string {
	unquoted := values;
	for i,value := range values {
		unquoted[i] = strings.Trim(value, `"`) // removes leading & trailing "
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
	return trimQuotes(strings.Split(rows[rowId],","));
}

func main() {

	row := readCsvRow(8);

	fmt.Println(
		"row 8:", 
		row[0],
		row[1],
	);
}