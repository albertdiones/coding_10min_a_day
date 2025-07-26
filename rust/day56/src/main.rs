use regex::Regex;
use std::io;

fn main() {
    let mut phone1 = String::new();

    println!("Input desired phone");

    io::stdin()
        .read_line(&mut phone1)
        .expect("Failed to read input");

    phone1 = phone1.trim().to_string();

    let phone_regex = Regex::new(r"^(\d\-?){7}\d$").unwrap();

    let phone_valid = phone_regex.is_match(&phone1);

    if phone_valid {
        println!(
            "Phone is valid!"
        );
    }
    else {
        println!(
            "Phone is invalid!!!"
        );
    }
}
