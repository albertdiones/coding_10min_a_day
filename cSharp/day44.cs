
using System;


Console.WriteLine("Input messag");
string message = Console.ReadLine();

string[] badWords = new string[]{
    "fuck",
    "dead",
    "suck"
};

foreach (string badWord in badWords)
{
    message = message.Replace(
        badWord,
        "*****"
    );
}

Console.WriteLine(
    "Processed message: " +
    message
);