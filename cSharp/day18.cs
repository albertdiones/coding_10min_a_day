
using System;


Console.WriteLine("Input a string: ");
string name = Console.ReadLine();

char[] nameChars = name.ToUpper().ToCharArray();

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