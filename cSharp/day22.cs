
using System;


Console.WriteLine("Input a number: ");
string stringNum1 = Console.ReadLine();

bool validNumber = 
    int.TryParse(stringNum1, out int num1);

if (validNumber) {
    if (num1 <= 100 && num1 >= 0) {
        Console.WriteLine("Valid number accepted");
    }
    else {
        Console.WriteLine("Not within the range!");
    }
}
else {
    Console.WriteLine("Not a number!");
}

Console.ReadKey();