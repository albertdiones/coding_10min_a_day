
using System;

string[] names = [
    "John", // 0
    "Clark", // 1
    "Tanggol", // 2
    "Michael",
    "Raull",
    "Sahur",
    "Abby",
    "Marky",
    "Gab",
    "Raide"
];

foreach (
    var (index, name) 
    in names.Select(
        (arrayValue, Arrayindex) 
            => (Arrayindex, arrayValue)
    )
) {
    Console.WriteLine(index + ": " + name);
}