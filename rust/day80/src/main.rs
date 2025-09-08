use crossterm::event::{read, Event, KeyEvent, KeyCode, KeyEventKind};
use std::io;
use std::io::Write;
use std::path::Path;
use sha2::{Digest, Sha256};
use std::fs;



fn main() {
    
    
    let password_file1 = "__rust-day80.txt";

    println!("");
    println!("");
    println!("");
    println!("");
    let file_path = Path::new(password_file1);;
    if file_path.exists() {
        println!("Please input the password:")
    }
    else {
        println!("Please input a new password:");
    }

    let mut password1 = String::new();
    loop {
        // Wait for an event
        if let Event::Key(KeyEvent { code, kind, .. }) = read().unwrap() {
            if kind != KeyEventKind::Press {
                continue;
            }
            match code {
                KeyCode::Enter => break,
                KeyCode::Char(c) => {
                    if (c as u32) > 31 {
                        password1 += &c.to_string();
                        io::stdout().flush().unwrap();
                    }
                }
                KeyCode::Backspace => {
                    password1.pop();
                    print!("\x08 \x08");
                    io::stdout().flush().unwrap();
                }
                _other => {
                }
            }
        }
    }

    let mut has_lowercase:bool = false;
    let mut has_uppercase:bool = false;
    let mut has_digit:bool = false;
    let mut has_symbol:bool = false;

    for password_byte in password1.bytes() {
        if password_byte >= 97 && password_byte <= 122 {
            has_lowercase = true;
            continue;
        }
        
        if password_byte >= 65 && password_byte <= 90 {
            has_uppercase = true;
            continue;
        }        
        
        if password_byte >= 48 && password_byte <= 57 {
            has_digit = true;
            continue;
        }

        has_symbol = true;
    }
    let sha2_password1 = format!("{:x}", Sha256::digest(password1.as_bytes()));

    if file_path.exists() {
        let correct_password_hash = fs::read_to_string(
            password_file1
        ).expect("Unable to read the stored password hash file");
        if sha2_password1 == correct_password_hash {
            println!("Successfully logged in");
        }
        else {
            println!("Access Denied");
        }
        return;
    }
    else {
        println!("");
        if has_lowercase && has_uppercase && has_digit && has_symbol {
            let _ = fs::write(password_file1, sha2_password1);
            println!("password is saved!!");
        }
        else {
            println!("password is too weak!!");
        }
        return;
    }
}
