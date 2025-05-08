
using System;


Console.WriteLine("Input a number in word to translate: ");
string stringNum1 = Console.ReadLine();

Dictionary<string, string> tagalog = 
    new Dictionary<string, string>(){
        {"one", "isa"},
        {"two", "dalawa"},
        {"three", "tatlo"},
        {"four", "apat"},
        {"five", "lima"},
        {"six", "anim"},
        {"seven", "pito"},
        {"eight", "walo"},
        {"nine", "siyam"},
        {"ten", "sampu"}
    };


Dictionary<string, string> bisaya = 
    new Dictionary<string, string>(){
        {"one", "usa"},
        {"two", "duha"},
        {"three", "tulo"},
        {"four", "upat"},
        {"five", "lima"},
        {"six", "unom"},
        {"seven", "pito"},
        {"eight", "walo"},
        {"nine", "siyam"},
        {"ten", "napo"}
    };


Console.WriteLine(
    "Translation: " + 
    tagalog[stringNum1]
);



Console.WriteLine(
    "Translation2: " + 
    bisaya[stringNum1]
);