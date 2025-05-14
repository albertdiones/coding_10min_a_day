
using System;
public static float square(
    float x
) {
    return x*x;
}


Console.WriteLine("Input number(to be squared):");
string input = Console.ReadLine();

float.TryParse(input, out float numberInput);

Console.WriteLine(
    "Result: " +
    square(numberInput).ToString()
);