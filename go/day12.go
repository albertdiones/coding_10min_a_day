package main

import "fmt"

func main() {
	var names = []string{
		"John",
		"Clark",
		"Tanggol",
		"Michael",
		"Raull",
		"Sahur",
		"Abby",
		"Marky",
		"Gab",
		"Raide",
	}

	for _,name := range names {
        fmt.Printf("Name: %s \n", name)
	}
}