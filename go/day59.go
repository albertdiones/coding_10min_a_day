package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
	"regexp"
)

func main() {
	fmt.Print("Input your phone number:\n");

	reader := bufio.NewReader(os.Stdin);
	phone1,_ := reader.ReadString('\n')
	phone1 = strings.TrimSpace(phone1);
	valid, _ := regexp.MatchString(
   "^[\\-\\+ \\(\\)]?(([0-9][\\-\\+ \\(\\)]*){7}|([0-9][\\-\\+ \\(\\)]*){9})[0-9]$",
		phone1,
	);

	fmt.Println("Valid phone number?? ", valid);
}