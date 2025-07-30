use regex::Regex;
use std::io;

fn main() {
    let mut phone1 = String::new();

    println!("Please input your phone number");

    io::stdin()
        .read_line(&mut phone1)
        .expect("error!");

    phone1 = phone1.trim().to_string();

    let phone_regex = Regex::new(
        r"^\d{8,10}$"
    ).unwrap();

    let phone_is_valid = phone_regex.is_match(
        &phone1
    );

    if phone_is_valid {
        println!("Valid!!");
    }
    else {
        println!("Invalid!!");
    }
}
