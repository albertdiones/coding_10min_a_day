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
    if (keyInfo.Key == ConsoleKey.Backspace)
    {
        Console.Write("\b");
        Console.Write(" ");
        Console.Write("\b");
        password1 = password1.Substring(0, password1.Length - 1);
        continue;
    }
    if (keyInfo.KeyChar > 30)
    {
        password1 += keyInfo.KeyChar;
        Console.Write(keyInfo.KeyChar);
    }
}



char[] passwordChars = password1.ToCharArray();


bool hasLowercase = false;
bool hasUppercase = false;
bool hasDigit = false;
bool hasSymbol = false;

foreach (char passwordChar in passwordChars) {
    if (passwordChar >= 97 && passwordChar <= 122) {
        hasLowercase = true;
        continue;
    }

    if (passwordChar >= 65 && passwordChar <= 90) {
        hasUppercase = true;
        continue;
    }

    if (passwordChar >= 48 && passwordChar <= 57) {
        hasDigit = true;
        continue;
    }
    hasSymbol = true;
}

int matches = 0;
if (hasLowercase) {
    matches += 1;
}
if (hasUppercase) {
    matches += 1;
}
if (hasDigit) {
    matches += 1;
}
if (hasSymbol) {
    matches += 1;
}



Console.WriteLine("");
if (
    matches >= 3
) {
    Console.WriteLine("Your password is valid");
}
else {
    Console.WriteLine("Password is too weak");
}

