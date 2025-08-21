package main

import (
	"fmt"
	"bufio"
	"os"
	"golang.org/x/term"
)

func main() {

	// Put terminal in raw mode (no echo, char-by-char input)
	fd := int(os.Stdin.Fd())
	oldState, err := term.MakeRaw(fd)
	if err != nil {
		panic(err)
	}
	// Always restore terminal on return
	defer term.Restore(fd, oldState)


	fmt.Println("Please input your password");

	password1 := "";

	for {
		reader := bufio.NewReader(os.Stdin);

		char1, _, err := reader.ReadRune();

		if err != nil {
			panic(err);
		}

		if (char1 == '\n' || char1 == '\r') {
			break;
		}

		if (char1 == 8 || char1 == 127) {
			fmt.Print("\b \b");
			password1 = password1[:len(password1)-1]
			continue;
		}

		if (char1 > 31) {
			password1 += string(char1);
			fmt.Print("*");
		}

	}
	fo, err := os.Create("day67-password.txt");

	if _, err := fo.Write([]byte(password1)); err != nil {
		panic(err)
	}

}