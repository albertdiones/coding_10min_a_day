using System;


Console.WriteLine("Input password");

ConsoleKeyInfo keyInfo;

while (true)
{
    keyInfo = Console.ReadKey(intercept: true); // intercept=true means don't display the key

    if (keyInfo.Key == ConsoleKey.Enter)
    {
        break;
    }
    else
    {
        Console.Write('*');
    }
}