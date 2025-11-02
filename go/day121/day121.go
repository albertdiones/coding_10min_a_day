package main

import (
	"fmt"
	"os"
	"strings"
	"bytes"
	"bufio"
	"strconv"
	"math"
    "os/exec"
    "runtime"

	"github.com/eiannone/keyboard"
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
		//fmt.Println("column: '" + column + "'");
		if column == columnName {
			return i+1;
		}
	}
	return -1;
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



func searchCsv(
	columnName string,
	 value string,
	partialMatch bool,
	caseInsentive bool,
) []int {
	rows := readAllCsvRow()

	columnNumber := getCsvColumnNumber(columnName)
	columnIndex := columnNumber - 1
	var rowIds []int

	for rowId := 1; rowId < len(rows); rowId++ {
		row := rows[rowId]


		hayStack := row[columnIndex];
		needle := value;

		
		if (caseInsentive) {
			hayStack = strings.ToLower(row[columnIndex]); 
			needle =  strings.ToLower(value);
		}

		match := hayStack == needle;

		if (partialMatch) {
			match = strings.Contains(hayStack, needle);
		}



		if match {
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

	rowIds1 := searchCsv("FirstName", query1, true, true)
	rowIds2 := searchCsv("LastName", query1, true, true)
	rowIds3 := searchCsv("Email", query1, true, true)

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

func deleteRoutine() {
	reader := bufio.NewReader(os.Stdin)

	fmt.Println("Please input rowId to delete:")

	rowIdInput, _ := reader.ReadString('\n')
	rowIdInput = strings.TrimSpace(rowIdInput)

	rowId, _ := strconv.Atoi(rowIdInput)

	fmt.Println("Are you sure you want to delete this row?")

	confirmationInput, _ := reader.ReadString('\n')
	confirmationInput = strings.TrimSpace(confirmationInput)

	confirmed := confirmationInput == "yes" ||
		confirmationInput == "y" ||
		confirmationInput == "Y" ||
		confirmationInput == "YES"

	if confirmed {
		deleteCSVRow(rowId)
		fmt.Println("Successfully Deleted Row")
	} else {
		fmt.Println("Delete cancelled")
	}
}

func printTable(rows [][]string, selectedRowId int) {
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

			if selectedRowId != 0 && 
				selectedRowId == rowId {
				printWithColor(cell,"\033[42m");
			} else {
				fmt.Print(cell);
			}

			fmt.Print(strings.Repeat(" ", padding) + " |")
		}
		fmt.Print("\n")

		if rowId == 0 {
			fmt.Println(strings.Repeat("-", totalLength+5+len(rowMaxLengths)))
		}
	}

	fmt.Printf(" %s\n", strings.Repeat("-", totalLength+3+len(rowMaxLengths)))
}


func printWithColor(message string, foreground string) {
	color := foreground
	fmt.Print(color + message +  "\033[0m")
}


func updateAskForField(
	field string, 
	currentValue string,
) string {
	reader := bufio.NewReader(os.Stdin)

	
	fmt.Printf(
		"Enter new: %s current: %s; press enter to not update\n", 
		field, 
		currentValue,
	)


	valueInput, _ := reader.ReadString('\n')
	valueInput = strings.TrimSpace(valueInput)

	if valueInput == "" {
		valueInput = currentValue
	}

	return valueInput
}


func updateRowRoutine(rowId int) {
	if rowId == 0 {
		fmt.Println("Error: no row selected")
		return
	}

	row := readCsvRow(rowId)
	email := updateAskForField("Email", row[0])
	firstName := updateAskForField("FirstName", row[1])
	lastName := updateAskForField("LastName", row[2])

	updateCsvRow(rowId, "Email", email)
	updateCsvRow(rowId, "FirstName", firstName)
	updateCsvRow(rowId, "LastName", lastName)

	fmt.Println("Successfully updated row")
}


func clearScreen() {
    var cmd *exec.Cmd
    switch runtime.GOOS {
    case "windows":
        cmd = exec.Command("cmd", "/c", "cls")
    default:
        cmd = exec.Command("clear")
    }
    cmd.Stdout = os.Stdout
    cmd.Run()
}

func addColumn(columnName string) {
	rows := readAllCsvRow();
	rows[0] = append(rows[0], columnName);
	setCsvToBlank();
	// map-style replacement
	for rowId, row := range rows {
		if (rowId != 0) {
			row = append(row, "")
		}
		addToCsv(row);
	}	
}

func addColumnRoutine() {	
	reader := bufio.NewReader(os.Stdin)

	
	fmt.Println(
		"Please input the new column name:", 
	)


	columnNameInput, _ := reader.ReadString('\n')
	columnName := strings.TrimSpace(columnNameInput)

	if columnName == "" {
		panic("Invalid column name")
	}

	existingColumnIndex := getCsvColumnNumber(columnName);

	if (existingColumnIndex != -1) {
		panic("Column name already exists!");
	}

	addColumn(columnName);
}



// --- main() equivalent to the C# while loop section ---
func main() {

	selectedRowId := 0;
	
	for {
		rows := readAllCsvRow();
		
		clearScreen();
		printTable(rows, selectedRowId);
		
		fmt.Println("selectedRowId: ", selectedRowId)
		fmt.Println("'s' to search")
		fmt.Println("'a' to add")
		fmt.Println("'d' to add")
		fmt.Println("'c' to add column")
		fmt.Println("ESC to exit")

		
		err := keyboard.Open()
		if err != nil {
			panic(err)
		}
		defer keyboard.Close()
		
		char, key, err := keyboard.GetKey()
		if err != nil {
			panic(err)
		}

		lowerChar := strings.ToLower(string(char))

		if key == keyboard.KeyEsc {
			fmt.Println("Escape pressed. Exiting...")
			break
		}

		if lowerChar == "s" {
			keyboard.Close()
			searchRoutine()
			continue
		}

		if lowerChar == "a" {
			
			keyboard.Close()
			addNewRowRoutine()
			continue
		}

		if lowerChar == "u" {
			keyboard.Close()
			updateRowRoutine(selectedRowId)
			continue
		}

		if lowerChar == "d" {
			keyboard.Close()
			deleteRoutine()
			continue
		}

		

		if lowerChar == "c" {
			keyboard.Close()
			addColumnRoutine()
			continue
		}

		if key == keyboard.KeyArrowUp {
			keyboard.Close()
			selectedRowId = int(
				math.Max(
					0,
					float64(selectedRowId - 1),
				));
			continue
		}
		

		if key == keyboard.KeyArrowDown {
			keyboard.Close()
			selectedRowId = int(
				math.Min(
				float64(len(rows)),
				float64(selectedRowId + 1),
			));
			continue
		}
	}
}

