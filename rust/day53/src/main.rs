use regex::Regex;
use std::io;

fn main() {
    let mut password1 = String::new();

    println!("Input desired password");

    io::stdin()
        .read_line(&mut password1)
        .expect("Failed to read input");

    password1 = password1.trim().to_string();

    let password_regex = Regex::new(r"[a-z]").unwrap();

    let password_valid = password_regex.is_match(&password1);

    if password_valid {
        println!(
            "Password is valid!"
        );
    }
    else {
        println!(
            "Password is invalid!!!"
        );
    }
}
