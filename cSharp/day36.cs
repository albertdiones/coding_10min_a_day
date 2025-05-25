
using System;
long unixTimestamp =
    DateTimeOffset.Now.ToUnixTimeMilliseconds();

long secondsElapsed =
    unixTimestamp % 86400000;


long offset = 8;

long secondsRemaining =
    86400000
    - secondsElapsed
    - (offset * 3600000);






Console.WriteLine(
    "MilliSeconds remaining: " +
    secondsRemaining.ToString()
);
