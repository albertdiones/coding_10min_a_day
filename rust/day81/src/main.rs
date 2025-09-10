use regex::Regex;
use std::io;


fn main() {
    println!(" ");
    println!(" ");
    
    println!("Please input your card number");


    let mut card_number = String::new();

    io::stdin()
        .read_line(&mut card_number)
        .expect("Failed to get card number input");

    card_number = card_number.trim().to_string();


    let card_number_regex = Regex::new(r"^\d{16}$").unwrap();

    let valid = card_number_regex.is_match(&card_number);

    if valid {
        println!("Card number is valid!");
    } else {
        println!("Card number is invalid!!!");
    }

}
