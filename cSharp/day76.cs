using System;
using System.Text.RegularExpressions;



Console.WriteLine("Input your password");

ConsoleKeyInfo keyInfo;

string password1 = "";

while (true)
{
    keyInfo = Console.ReadKey(intercept: true); // intercept=true means don't display the key


    if (keyInfo.Key == ConsoleKey.Enter)
    {
        break;
    }
    if (keyInfo.Key == ConsoleKey.Backspace) {
        Console.Write("\b");
        Console.Write(" ");
        Console.Write("\b");
        password1 = password1.Substring(0,password1.Length -1);
        continue;
    }
    if (keyInfo.KeyChar > 30) {
        password1 += keyInfo.KeyChar;
        Console.Write(keyInfo.KeyChar);
    }
}

Console.WriteLine("");
if (
    password1 == password1.ToLower()
) {
    Console.WriteLine("Your password is invalid");
    Console.WriteLine("Must contain at least one uppercase letter");
}
else {
    Console.WriteLine("Password is valid");
}

