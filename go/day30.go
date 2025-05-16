package main

import (
	"fmt"
	"strconv"
	"math/rand"
	"time"
)

func roll() []int {
	return []int{rand.Intn(6)+1, rand.Intn(6)+1}
}

func main() {

	rand.Seed(time.Now().UnixNano())
	
	result := roll();
	fmt.Print(
		"Dice: " + 
		strconv.Itoa(result[0]) +
		"," +
		strconv.Itoa(result[1]) ,
	);

}