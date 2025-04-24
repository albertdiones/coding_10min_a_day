
using System;
using System.Globalization;

string str1 = "juan DELA cruz";
int int1 = 1111999;
double float1 = 764513.999;
bool bool1 = false;


System.Console.WriteLine(
    str1 + "-" + 
    int1.ToString()
    + "-" 
    + string.Format("{0:N3}",float1)  
    + "-" + bool1
);

