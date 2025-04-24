
using System;
using System.Globalization;

string str1 = "juan DELA cruz";


System.Console.WriteLine(str1);

TextInfo myTI = new CultureInfo("en-US",false).TextInfo;

// Juan Dela Cruz
System.Console.WriteLine(
    myTI.ToTitleCase(str1.ToLower())
);

// JUAN DELA CRUZ
System.Console.WriteLine(str1.ToUpper());

// juan dela cruz
System.Console.WriteLine(str1.ToLower());
