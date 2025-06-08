
using System;
using Microsoft.VisualBasic;

DateTime now = DateTime.Now;

DateTime yesterday
    = DateAndTime.DateAdd(
        DateInterval.Day,
        -1,
        now
    );

Console.WriteLine(
    "Yesterday: " + 
    yesterday.ToString("MMM dd yyyy")
);