
using System;

string[] lyrics = [
    "bayang",
    "magiliw",
    "perlas",
    "ng",
    "silanganan",
    "alab",
    "ng",
    "pugo" // wrong lyrics!!
];

Console.WriteLine(
    String.Join(" ",lyrics)
);

Stack<string> lyricsStack = new Stack<string>(
    lyrics
);
lyricsStack.Pop();

string[] poppedLyrics = lyricsStack.ToArray();
Array.Reverse(poppedLyrics);

Console.WriteLine(
    String.Join(" ", poppedLyrics)
);

lyricsStack.Push("puso");


string[] correctedLyrics = lyricsStack.ToArray();
Array.Reverse(correctedLyrics);


Console.WriteLine(
    String.Join(" ", correctedLyrics)
);

