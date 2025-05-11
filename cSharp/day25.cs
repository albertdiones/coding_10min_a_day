
using System;

public static int add(
    int x,
    int y
) {
    return x + y;
}

Console.WriteLine("Input 2 numbers");


string input1 = Console.ReadLine();
string input2 = Console.ReadLine();

int.TryParse(input1, out int numInput1);
int.TryParse(input2, out int numInput2);

Console.WriteLine(
    "Sum: " +
    add(numInput1, numInput2).ToString()
);