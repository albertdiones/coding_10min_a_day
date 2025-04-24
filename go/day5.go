package main

import "fmt"
import "strings"

func main() {
	var str1 = "Juan DELA cruZ";
	fmt.Println(strings.Title(strings.ToLower(str1)));
}