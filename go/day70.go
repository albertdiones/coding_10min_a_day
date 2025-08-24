package main

import (
	"fmt"
	"bufio"
	"os"
	"golang.org/x/term"
	"crypto/sha256"
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
	fo, err := os.Create("day70-password.txt");

	sha256Hasher := sha256.New();
	sha256Hasher.Write([]byte(password1));
	hash := fmt.Sprintf("%x", sha256Hasher.Sum(nil));

	if _, err := fo.Write([]byte(hash)); err != nil {
		panic(err)
	}

}