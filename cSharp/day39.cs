
using System;


Console.WriteLine("Input a valid date:");
string input1 = Console.ReadLine();

DateTime inputDate = DateTime.Parse(input1);


long unixTimestamp = ((DateTimeOffset)inputDate).ToUnixTimeSeconds();


Console.WriteLine(
    "Timestamp: " +
    unixTimestamp
);