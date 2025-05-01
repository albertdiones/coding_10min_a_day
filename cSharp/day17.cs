
using System;


string name = "Peter";

char[] nameChars = name.ToCharArray();

char[] uniqueChars 
    = nameChars.Distinct().ToArray();//.toUnique(*)"??

string uniqueCharacterListString 
    =  String.Join(
    '\n',
    uniqueChars
);

Console.WriteLine(
    uniqueCharacterListString
);