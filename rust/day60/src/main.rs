use regex::Regex;
use std::io;

fn main() {
    println!("Please input your phone number");

    let mut phone1 = String::new();

    io::stdin()
        .read_line(&mut phone1)
        .expect("Invalid phone number");

    phone1 = phone1.trim().to_string();

    let phone_pattern = Regex::new("^(\\d{8}|\\d{11})$").unwrap();



    if phone_pattern.is_match( &phone1 ) {
        println!("Valid phone number!");
    }
    else {
        println!("Invalid phone number");
    }


}
