
using System;

string emailAddress = "juan.delacruz@gmail.com";

string[] emailParts = emailAddress.Split('@');

Console.WriteLine(
    emailParts[0]
);

Console.WriteLine(
    emailParts[1]
);



