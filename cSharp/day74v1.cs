using System;
using System.Text.RegularExpressions;

public static void clearLine() {
    // Go up one line
        Console.SetCursorPosition(0, Console.CursorTop);

        // Overwrite with spaces
        Console.Write(new string(' ', Console.WindowWidth));

        // Reset cursor to beginning of that line
        Console.SetCursorPosition(0, Console.CursorTop);
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
        password1.RemoveAt(password1.Count - 1);
        continue;
    }
    if (keyInfo.Key == ConsoleKey.LeftArrow)
    {
        index = Math.Max(0, index - 1);
        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
    }
    if (keyInfo.KeyChar > 30)
    {
        password1.Add(keyInfo.KeyChar);
    }
    if (password1.Count > 1) {
        clearLine();
    }
    Console.Write(
        new string('*', password1.Count)
    );
}


Console.WriteLine("");
Console.WriteLine("your password is: " + new string(password1.ToArray()));