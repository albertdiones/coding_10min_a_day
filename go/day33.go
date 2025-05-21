package main

import (
	"fmt"
	"time"
	"strconv"
)

func main() {
	var currentTime = time.Now()
	fmt.Print(
		"Current Timestamp: " +
		strconv.FormatInt(currentTime.Unix(),10),
	)
}