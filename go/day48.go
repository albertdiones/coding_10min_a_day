package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
	"regexp"
)

func main() {
	fmt.Println("Please input your email")

	reader := bufio.NewReader(os.Stdin)
	emailAddress, _ := reader.ReadString('\n')
	emailAddress = strings.TrimSpace(emailAddress)

	match, _ := regexp.MatchString("\\@.+\\.",emailAddress)

	fmt.Println("Valid Email:", match);
}
