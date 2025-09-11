package main

import (
	"fmt"
	"bufio"
	"os"
	"strings"
	"regexp"
)
func main() {
	fmt.Println("please input card number");


	reader := bufio.NewReader(os.Stdin)


	cardNumber, _ := reader.ReadString('\n');

	cardNumber = strings.TrimSpace(cardNumber);


	match, _ := regexp.MatchString("^[\\d ]{13,19}$", cardNumber)

	if match {
		fmt.Println("Card number is valid.")
	} else {
		fmt.Println("Card number is invalid!!!!")
	}
}