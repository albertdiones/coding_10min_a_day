using System;
using System.Text.RegularExpressions;

Console.WriteLine("Please input your credit/debit card number:");

string cardNumber1 = Console.ReadLine();

Regex cardNumberPattern = new Regex("^[\\d ]{13,16}$");

if (cardNumberPattern.Match(cardNumber1).Success)
{
    char[] chars = cardNumber1.ToCharArray();

    Array.Reverse(chars);

    int sum1 = 0;

    for (int i = 0; i < chars.Length; i++)
    {
        int mod1 = i % 2;
        int charAsInt = Convert.ToInt32(new string(chars[i], 1)); ;
        if (mod1 == 1)
        {
            charAsInt = charAsInt * 2;
        }

        int subTotal = (charAsInt % 10) + (int) Math.Floor( (double) (charAsInt / 10));

        sum1 += subTotal;
    }

    if ((sum1 % 10) == 0)
    {
        Console.WriteLine("Valid credit card number!!" );
    }
    else
    {
        Console.WriteLine("Card number combination is invalid !!!! luhn sum: " + sum1);
    }
}
else
{
    Console.WriteLine("Card number length is invalid !!!!");
}