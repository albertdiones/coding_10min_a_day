package main

import "fmt"
import "strconv"

func main() {
	var str1 = "Juan DELA cruZ";
	var float1 = 123235.999998;
	fmt.Println(
		str1 + 
		"-" +
		strconv.FormatFloat(float1, 'f', -1, 64),
	);
}