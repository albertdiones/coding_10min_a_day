
using System;

string name1 = "john";

// ['j','o','h','n']
char[] chars = name1.ToCharArray();

chars[0] = Char.ToUpper(chars[0]);

string string2 = new string(chars);

Console.WriteLine(
    string2
);



