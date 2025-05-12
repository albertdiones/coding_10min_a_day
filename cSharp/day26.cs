
using System;

public static float add(
    float x,
    float y
) {
    return x + y;
}

public static float subtract(
    float x,
    float y
) {
    return x - y;
}

public static float divide(
    float x,
    float y
) {
    return x / y;
}

public static float multiply(
    float x,
    float y
) {
    return x * y;
}

Console.WriteLine("Input 2 numbers to add");
string input1 = Console.ReadLine();
string input2 = Console.ReadLine();

float.TryParse(input1, out float numInput1);
float.TryParse(input2, out float numInput2);

Console.WriteLine(
    "Sum: " +
    add(numInput1, numInput2).ToString()
);



Console.WriteLine("Input 2 numbers to subtract");
string input3 = Console.ReadLine();
string input4 = Console.ReadLine();

float.TryParse(input3, out float numInput3);
float.TryParse(input4, out float numInput4);

Console.WriteLine(
    "Difference: " +
    subtract(numInput3, numInput4).ToString()
);



Console.WriteLine("Input 2 numbers to divide");
string input5 = Console.ReadLine();
string input6 = Console.ReadLine();

float.TryParse(input5, out float numInput5);
float.TryParse(input6, out float numInput6);

Console.WriteLine(
    "Quotient: " +
    divide(numInput5, numInput6).ToString()
);



Console.WriteLine("Input 2 numbers to multiply");
string input7 = Console.ReadLine();
string input8 = Console.ReadLine();

float.TryParse(input7, out float numInput7);
float.TryParse(input8, out float numInput8);

Console.WriteLine(
    "Product: " +
    multiply(numInput7, numInput8).ToString()
);