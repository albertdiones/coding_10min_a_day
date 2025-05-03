package main

import (
	"fmt"
)

func main() {
	name := "Peter"
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
