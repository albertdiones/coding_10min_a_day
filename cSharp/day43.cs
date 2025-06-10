
using System;


DateTime dateUtc = DateTime.UtcNow;

string[] timezoneCodes = new string[]
    {
        "Hawaiian Standard Time",       // America/Hawaii
        "AUS Eastern Standard Time",    // Sydney
        "Tokyo Standard Time",          // Tokyo
        "Singapore Standard Time",      // Manila
        "Bangladesh Standard Time",     // Bangladesh
        "India Standard Time",          // New Delhi
        "Central Asia Standard Time",   // Kazakhstan
        "Iran Standard Time",           // Iran
        "Arab Standard Time",           // Saudi Arabia
        "Turkey Standard Time",         // Turkey
        "Central European Standard Time", // Poland
        "GMT Standard Time",            // London/UTC
        "GMT Standard Time",            // Portugal (same as London)
        "GMT Standard Time",            // Iceland (no DST)
        "Greenland Standard Time",      // Greenland
        "Eastern Standard Time",        // New York
        "Central Standard Time",        // Chicago
        "US Mountain Standard Time",    // Arizona
        "Mountain Standard Time",       // New Mexico
        "Pacific Standard Time",        // California
        "Alaskan Standard Time"         // Alaska
    };

foreach (string timezoneCode in timezoneCodes)
{
    TimeZoneInfo timezone  = TimeZoneInfo
        .FindSystemTimeZoneById(timezoneCode);
    DateTime timezoneTime = TimeZoneInfo
        .ConvertTimeFromUtc(dateUtc, timezone);
    
    Console.WriteLine(
        "Time in "+ timezoneCode + " " +
        timezoneTime.ToString("MMM dd yyyy HH:mm:ss tt")
    );

}

