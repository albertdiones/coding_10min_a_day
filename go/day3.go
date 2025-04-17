package main

import "fmt"
import "strings"

func main() {
	var str1 = "                 Hello            ";
    fmt.Println(str1 + " World");
	fmt.Println(strings.Trim(str1, " ") + " World");
}