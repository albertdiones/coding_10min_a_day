use md5;
use crossterm::event::{read, Event, KeyEvent, KeyCode, KeyEventKind};
use std::io;
use std::io::Write;
use std::fs;
use std::env;
use std::path::Path;
use sha2::{Digest, Sha256};

fn main() {
    
    let password_file1 = "__rust-day73.txt";

    let file_path = Path::new(password_file1);;
    if file_path.exists() {
        println!("Please input the proper password:")
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
                        print!("*");
                        io::stdout().flush().unwrap();
                    }
                }
                KeyCode::Backspace => {
                    password1.pop();
                    print!("\x08 \x08");
                    io::stdout().flush().unwrap();
                }
                other => {
                }
            }
        }
    }
    println!("");

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
    }
    else {
        let _ = fs::write(password_file1, sha2_password1);
    }


    

    
    

}
