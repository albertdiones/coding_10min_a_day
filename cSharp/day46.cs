using System;


Console.WriteLine("Please input your email");

string email = Console.ReadLine();


Console.WriteLine(
    "Contains @ sign: "
    + email.Contains("@").ToString()
);