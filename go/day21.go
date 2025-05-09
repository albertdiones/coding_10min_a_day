package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

func main() {
	fmt.Print("Input a number: ")

	reader := bufio.NewReader(os.Stdin)
	input, _ := reader.ReadString('\n')
	input = strings.TrimSpace(input)

	num, err := strconv.Atoi(input)

	if err == nil {
		if (num >= 0 && num<=100) {
			fmt.Println(
				"Valid number accepted"
				);
		}
		else {
			return null, errors.New(
				"number not within the range"
			);
		}
	} else {
		fmt.Println("Not a number!");
	}
}