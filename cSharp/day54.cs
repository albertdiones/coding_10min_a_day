using System;
using System.Text.RegularExpressions;

Console.WriteLine("Please input your desired password");
string password = Console.ReadLine();

Regex hasDigit = new Regex("\\d+");
Regex hasSpecialChar = new Regex("[^A-Za-z0-9]");
Regex hasUppercase = new Regex("[A-Z]");
Regex hasLowercase = new Regex("[a-z]");


if (
    password.Length >= 10
    && hasDigit.Match(password).Success
    && hasSpecialChar.Match(password).Success
    && hasUppercase.Match(password).Success
    && hasLowercase.Match(password).Success
) {
    Console.WriteLine("Password is valid");
}
else {
    Console.WriteLine("Password is INVALID");
}