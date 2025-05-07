package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
	"errors"
)

func main() (bool) {
	fmt.Print("Input a number: ")

	reader := bufio.NewReader(os.Stdin)
	input, _ := reader.ReadString('\n')
	input = strings.TrimSpace(input)

	num, err := strconv.Atoi(input)

	if err == nil {
		if num >= 0 && num<=100 {
			fmt.Println("Valid number accepted");
			return true;
		} else {
			fmt.Println("number not within the range");
			return false;
		}
	} else {
		fmt.Println("Not a number!");
		return false;
	}
	return false;
}