
using System;


string name = "Peter Dela Rosa";

char[] nameChars = name.ToCharArray();

IDictionary<string, int> characterCounts 
    = new Dictionary<string, int>(); 

foreach(char char1 in nameChars) {
    string charString = new string([char1]);
    if (
        !characterCounts
            .ContainsKey(charString)
    ) {
        characterCounts[charString] = 1;
    }
    else {
        characterCounts[charString] += 1;
    }
    
}



foreach (
    KeyValuePair<string, int> kvp 
        in characterCounts
) {
    Console.WriteLine(
        $"{kvp.Key}: {kvp.Value}"
    );
}