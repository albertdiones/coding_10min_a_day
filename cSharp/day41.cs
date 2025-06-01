
using System;
using Microsoft.VisualBasic;

DateTime now = DateTime.Now;

DateTime oneHourLater
    = DateAndTime.DateAdd(
        DateInterval.Hour,
        1,
        now
    );

Console.WriteLine(
    "1 Hour Later: " + 
    oneHourLater.ToString("MMM dd yyyy HH:mm:ss K")
);