
using System;


DateTime dateUtc = DateTime.UtcNow;

TimeZoneInfo nyTimezone = TimeZoneInfo
    .FindSystemTimeZoneById("America/New_York");

DateTime nyTime = TimeZoneInfo
    .ConvertTimeFromUtc(dateUtc, nyTimezone);


Console.WriteLine(
    "Time in NYC: "+
    nyTime.ToString("MMM dd yyyy HH:mm:ss tt")
);

