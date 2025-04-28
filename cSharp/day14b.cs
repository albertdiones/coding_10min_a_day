
using System;

string name1 = "shit";

// ['j','o','h','n']
char[] chars = name1.ToCharArray();

chars[1] = '*'; // h
chars[2] = '*'; // i

string string2 = new string(chars);

Console.WriteLine(
    string2
);



