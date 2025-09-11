using System;
using System.Text.RegularExpressions;

Console.WriteLine("Please input your credit/debit card number:");

string cardNumber1 = Console.ReadLine();

Regex cardNumberPattern = new Regex("^[\\d ]{13,19}$");

if (cardNumberPattern.Match(cardNumber1).Success) {
    Console.WriteLine("Card number is valid.");
}
else {
    Console.WriteLine("Card number is invalid !!!!");
}