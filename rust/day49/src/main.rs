use regex::Regex;


fn main() {   
    use std::io;

    let mut email1 = String::new();
    println!("Input email address: ");
    io::stdin()
        .read_line(&mut email1)
        .expect("Failed to read email");

    let email_address_regex = Regex::new(r".+\@.+\..+").unwrap();


    let email_matched = email_address_regex.is_match(&email1) ;
    
    println!(
        "Email address is valid: {:?}",
        email_matched
    );
}
