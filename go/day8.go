package main

import "fmt"
import "strconv"

func main() {
	var str1 = "Hello";
	var bool1 = true;
	var bool2 = false;
	fmt.Println(
		str1 + 
		"-" +
		strconv.FormatBool(bool1) +
		"-" +
		strconv.FormatBool(bool2),
	);
}