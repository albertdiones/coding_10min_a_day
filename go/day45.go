package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
)

func square(x float64) float64 {
	return x * x
}

func main() {
	fmt.Print("Input message: ")

	reader := bufio.NewReader(os.Stdin)
	input, _ := reader.ReadString('\n')
	var message = strings.TrimSpace(input);

	var badWords = []string{
		"fuck",
		"dead",
		"suck",
		"asshole",
		"faggot",
	}

	
	for _, badWord := range badWords {
		message = strings.Replace(
			message,
			badWord,
			strings.Repeat("*",len(badWord)),
			-1,
		);
	}

	
	fmt.Print(
		"Result: " + 
		message +
		"\n\n",
	);

}