package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
	"regexp"
)

func main() {
	fmt.Println("Please input your password")

	reader := bufio.NewReader(os.Stdin)
	password1, _ := reader.ReadString('\n')
	password1 = strings.TrimSpace(password1)

	match, _ := regexp.MatchString("\\d+",password1)

	fmt.Println("Valid Password:", match);
}
