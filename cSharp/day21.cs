
using System;


Console.WriteLine("Input a number: ");
string stringNum1 = Console.ReadLine();

bool validNumber = 
    int.TryParse(stringNum1, out int num1);

if (validNumber) {
    Console.WriteLine("Success!");
}
else {
    Console.WriteLine("Not a number!");
}

Console.ReadKey();