using System;
using System.Text.RegularExpressions;

Console.WriteLine("Please input your desired password");
string password = Console.ReadLine();


Regex hasDigits = new Regex("[A-Z]");


if (hasDigits.Match(password).Success) {
    Console.WriteLine("Password is valid");
}
else {
    Console.WriteLine("Password is INVALID");
}
