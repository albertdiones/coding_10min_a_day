package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

func main() {
	fmt.Print("Input a year: ")

	reader := bufio.NewReader(os.Stdin)
	yearInput, _ := reader.ReadString('\n')
	yearInput = strings.TrimSpace(yearInput)

	yearInt, _ := strconv.Atoi(yearInput);

	isLeapYear := yearInt % 4 == 0;

	fmt.Print(
		"Is it a leap year? " + 
		strconv.FormatBool(isLeapYear),	
	);

}