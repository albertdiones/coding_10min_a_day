
using System;


Console.WriteLine("Input messag");
string message = Console.ReadLine();

string[] badWords = new string[]{
    "fuck",
    "dead",
    "suck",
    "asshole",
    "faggot"
};

foreach (string badWord in badWords)
{
    message = message.Replace(
        badWord,
        new string('*', badWord.Length)
    );
}

Console.WriteLine(
    "Processed message: " +
    message
);