package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
	"regexp"
)

func main() {
	fmt.Println("Please input your phone number")

	reader := bufio.NewReader(os.Stdin)

	phone,_ := reader.ReadString('\n')
	phone = strings.TrimSpace(phone)

	match, _ := regexp.MatchString("^[0-9]{8}$", phone)

	fmt.Println("Valid phone number: ", match)
}