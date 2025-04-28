fn main() {
    let name = "shawn";
    
    // Split into characters
    let mut name_chars: Vec<char> = name
        .chars()
        .collect();
    
    name_chars[0] = name_chars[0]
        .to_uppercase()
        .next()
        .unwrap();

    let name2: String = name_chars
        .iter().collect();

    println!("{}", name2);
}
