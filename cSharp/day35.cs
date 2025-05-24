
using System;
long unixTimestamp =
    DateTimeOffset.Now.ToUnixTimeSeconds();

long secondsElapsed =
    unixTimestamp % 86400;


long offset = 8;

long secondsRemaining = 86400 - secondsElapsed - (offset*3600);






Console.WriteLine(
    "Seconds remaining: " +
    secondsRemaining.ToString()
);
