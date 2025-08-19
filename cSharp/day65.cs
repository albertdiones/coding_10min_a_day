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
    if (keyInfo.Key == ConsoleKey.Backspace) {
        Console.Write("\b");
        Console.Write(" ");
        Console.Write("\b");
        continue;
    }
    if (keyInfo.KeyChar > 30) {
        Console.Write('*');
    }
}