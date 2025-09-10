package main

import (
	"fmt"
	"bufio"
	"os"
	"strings"
	"regexp"
)

func main() {
	fmt.Println("Please input your card number")


	reader := bufio.NewReader(os.Stdin)

	cardNumber,_ := reader.ReadString('\n')

	cardNumber = strings.TrimSpace(cardNumber)

	match,_ := regexp.MatchString("^\\d{16}$",cardNumber)


	if match {
		fmt.Println("Valid card number!")
	} else {
		fmt.Println("Invalid card number!!!")
	}
}