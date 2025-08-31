using System;
using System.Text.RegularExpressions;

public static void toTheLeft() {
    Console.SetCursorPosition(
        Math.Max(0,Console.CursorLeft - 1),
        Console.CursorTop
    );
}


Console.WriteLine("Input your password");

ConsoleKeyInfo keyInfo;


List<char> password1 = new List<char> { };

int index = 0;


while (true)
{
    keyInfo = Console.ReadKey(intercept: true); // intercept=true means don't display the key


    if (keyInfo.Key == ConsoleKey.Enter)
    {
        break;
    }
    if (keyInfo.Key == ConsoleKey.Backspace)
    {
        Console.Write("\b");
        Console.Write(" ");
        Console.Write("\b");
        
        password1.RemoveAt(password1.Count - 1);

        continue;
    }
    if (keyInfo.Key == ConsoleKey.LeftArrow)
    {
        index = Math.Max(0, index - 1);
        toTheLeft();
        // Console.Write("\b");
    }
    if (keyInfo.KeyChar > 30)
    {
        password1.Add(keyInfo.KeyChar);
        Console.Write(keyInfo.KeyChar);
    }
}

Console.WriteLine("");
Console.WriteLine("your password is: " + new string(password1.ToArray()));