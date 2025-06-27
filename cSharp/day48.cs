using System;
using System.Text.RegularExpressions;


Console.WriteLine("Please input email");

string emailAddress = Console.ReadLine();

Console.WriteLine("Valid email: ");

Regex pattern = new Regex("\\@.+\\.");

if (pattern.Match(emailAddress).Success)
{
    Console.WriteLine("True");
}
else
{
    Console.WriteLine("False");
}