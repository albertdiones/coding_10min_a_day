using System;
using System.Text.RegularExpressions;

Console.WriteLine("Please input your phone number");
string password = Console.ReadLine();

Regex hasCorrectNumberOfDigits = new Regex("^\\d{8}$");

if (
    hasCorrectNumberOfDigits.Match(password).Success
) {
    Console.WriteLine("phone number is valid");
}
else {
    Console.WriteLine("phone number is INVALID");
}