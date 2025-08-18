using System;
using System.Text.RegularExpressions;



Console.WriteLine("Input password");

ConsoleKeyInfo keyInfo;

Regex isCharacter = new Regex("^.$");

while (true)
{
    keyInfo = Console.ReadKey(intercept: true); // intercept=true means don't display the key


    if (keyInfo.Key == ConsoleKey.Enter)
    {
        break;
    }
    if (keyInfo.KeyChar > 30) {
        Console.Write('*');
    }
}