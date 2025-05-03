package main
import "strings"

import (
	"fmt"
)

func main() {
	fmt.Println("input text:")
    var name string;
    fmt.Scanln(&name)
	name = strings.ToUpper(name); // case insensitive
	seen := make(map[rune]bool)
	var uniqueChars []rune

	for _, char := range name {
		if !seen[char] {
			seen[char] = true
			uniqueChars = append(uniqueChars, char)
		}
	}

	for _, char := range uniqueChars {
		fmt.Println(string(char))
	}
}
