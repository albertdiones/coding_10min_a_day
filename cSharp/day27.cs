
using System;
public static float deduct(
    float x,
    float y
) {
    return Math.Max(x - y, 0);
}


Console.WriteLine("Input first number(to be deducted):");
string input3 = Console.ReadLine();

Console.WriteLine("Input second number(deductor):");
string input4 = Console.ReadLine();

float.TryParse(input3, out float numInput3);
float.TryParse(input4, out float numInput4);

Console.WriteLine(
    "Result: " +
    deduct(numInput3, numInput4).ToString()
);