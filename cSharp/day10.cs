
using System;

string emailAddress = "juan.delacruz@gmail.com";

string[] emailParts = emailAddress.Split('@');

System.Console.WriteLine(
    emailParts[0]
);

System.Console.WriteLine(
    emailParts[1]
);



