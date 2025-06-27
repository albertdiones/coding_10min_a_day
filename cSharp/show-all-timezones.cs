
using System;

foreach (var tz in TimeZoneInfo.GetSystemTimeZones())
{
    Console.WriteLine(tz.Id);
}