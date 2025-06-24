package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
)

func main() {
	fmt.Println("Please input your email")

	reader := bufio.NewReader(os.Stdin)
	emailAddress, _ := reader.ReadString('\n')
	emailAddress = strings.TrimSpace(emailAddress)

	var hasAtSign = strings.Contains(emailAddress, "@");
	var hasPeriod = strings.Contains(emailAddress, ".");

	fmt.Println("Contains @ sign:", hasAtSign);
	fmt.Println("Contains period:", hasPeriod);
}
