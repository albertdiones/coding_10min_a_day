package main

import (
	"fmt"
	"time"
)

func main() {
	var nyLocation, _  = time.LoadLocation("America/New_York");
	var nyTime = time.Now().In(nyLocation);

	fmt.Print(
		"\n\nCurrent Date/Time in NYC: " +
		nyTime.Format("Mon Jan 02 2006") +
		" " +
		nyTime.Format("15:04:05") +
		"\n",
	)
}