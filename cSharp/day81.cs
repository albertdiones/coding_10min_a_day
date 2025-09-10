using System;
using System.Text.RegularExpressions;


Console.WriteLine("Please input your card number:");


string cardNumber1 = Console.ReadLine();

Regex cardPattern1 = new Regex("^\\d{16}$");

if (cardPattern1.Match(cardNumber1).Success) {
    Console.WriteLine("Valid card number");
}
else {
    Console.WriteLine("Invalid card number");
}
