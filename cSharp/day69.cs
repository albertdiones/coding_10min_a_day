using System;
using System.Text.RegularExpressions;
using System.Security.Cryptography;



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
        Console.Write('*');
    }
}
string hash64 = "";
using (var sha1 = SHA1.Create())
{
    byte[] inputBytes = Encoding.UTF8.GetBytes(password1);
    byte[] hashBytes = sha1.ComputeHash(inputBytes);

    hash64 = Convert.ToBase64String(hashBytes);
}
Console.WriteLine("Hash: " + hash64);



File.WriteAllText("day69-password.txt", hash64);

