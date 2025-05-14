package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

func square(x float64) float64 {
	return x * x
}

func main() {
	fmt.Print("Input number(to be squared): ")

	reader := bufio.NewReader(os.Stdin)
	input, _ := reader.ReadString('\n')
	input = strings.TrimSpace(input)

	num1, _ := strconv.ParseFloat(input,32);
	
	fmt.Print(
		"Result: " + 
		strconv.FormatFloat(
			square(num1), 
			'f', 
			-1,
			32,
		) +
		"\n\n",
	);

}