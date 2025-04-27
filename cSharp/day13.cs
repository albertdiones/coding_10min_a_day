
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
    "Raide",
    "Byre",
    "Sebby",
    "Jv",
    "Raul",
    "VmosPro547",
    "Zura",
    "Kanor",
    "Kiko",
    "Diwata",
    "Los",
    "Mik",
    "Andy",
    "Umaay",
    "Cayatano",
    "Darry",
    "Imee",
    "Almagro"
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