package main

import (
	"fmt"
	"time"
)

func main() {
	var currentTime = time.Now()
	fmt.Print(
		"Current Date/Time: " +
		currentTime.Format("Mon Jan 02 2006") +
		" " +
		currentTime.Format("15:04:05") +
		"\n",
	)
}