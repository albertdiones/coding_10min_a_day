use regex::Regex;


fn main() {   
    use std::io;

    let mut password1 = String::new();
    println!("Input password: ");
    io::stdin()
        .read_line(&mut password1)
        .expect("Failed to read password");

    
    let password2 = password1.trim();

    let password_regex = Regex::new(r"^.*[^a-zA-Z0-9].*$").unwrap();


    let password_valid = password_regex.is_match(&password2) ;
    
    println!(
        "Password is strong enough: {:?}",
        password_valid
    );
}
