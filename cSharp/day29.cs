
using System;
public static float greatest(
    float x,
    float y,
    float z
) {
    if (x > y && x > z) {
        return x;
    }
    
    if (y > x && y > z) {
        return y;
    }
    
    if (z > x && z > y) {
        return z;
    }

    return x;
}


Console.WriteLine("Input 3 numbers(to know which one is greatest):");
string input1 = Console.ReadLine();
float.TryParse(input1, out float numberInput1);

string input2 = Console.ReadLine();
float.TryParse(input2, out float numberInput2);

string input3 = Console.ReadLine();
float.TryParse(input3, out float numberInput3);

Console.WriteLine(
    "Result: " +
    greatest(
        numberInput1,
        numberInput2,
        numberInput3
    ).ToString()
);