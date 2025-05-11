package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

func add(x int, y int) int {
	return x + y
}

func main() {
	fmt.Print("Input 2 numbers: ")

	reader := bufio.NewReader(os.Stdin)
	input, _ := reader.ReadString('\n')
	input = strings.TrimSpace(input)

	num1, _ := strconv.Atoi(input);

	
	reader2 := bufio.NewReader(os.Stdin)
	input2, _ := reader2.ReadString('\n')
	input2 = strings.TrimSpace(input2)

	num2, _ := strconv.Atoi(input2)

	fmt.Print(
		"Sum: " + 
		strconv.Itoa(add(num1, num2)),	
	);

}