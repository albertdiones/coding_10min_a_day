package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
	"regexp"
)

func main() {
	fmt.Print("Input your phone number:\n");

	reader := bufio.NewReader(os.Stdin);
	phone1,_ := reader.ReadString('\n')
	phone1 = strings.TrimSpace(phone1);
	landlineMatch, _ := regexp.MatchString(
   "^([ \\-\\(\\)]*[0-9][ \\-\\(\\)]*){8}$",
		phone1,
	);

	cellphoneMatch, _ := regexp.MatchString(
		"^([ \\-\\(\\)]*0[ \\-\\(\\)]*)([ \\-\\(\\)]*\\d[ \\-\\(\\)]*){10}$",
		phone1,
	);

	internationalCellMatch, _ := regexp.MatchString(
		"^([ \\-\\(\\)]*\\+[ \\-\\(\\)]*)([ \\-\\(\\)]*\\d[ \\-\\(\\)]*){12}$",
		phone1,
	);


	fmt.Println("landlineMatch: ", landlineMatch);
	

	fmt.Println("cellphoneMatch: ", cellphoneMatch);
	

	fmt.Println("internationalCellMatch: ", internationalCellMatch);


	valid := landlineMatch || cellphoneMatch || internationalCellMatch;

	fmt.Println("Valid phone number?? ", valid);
}