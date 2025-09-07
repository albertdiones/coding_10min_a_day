use crossterm::event::{read, Event, KeyEvent, KeyCode, KeyEventKind};
use std::io;
use std::io::Write;

fn main() {
    println!("\n\nPlease input your password");

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


    println!("");
    if has_lowercase && has_uppercase && has_digit && has_symbol {
        println!("password is valid!!");
    }
    else {
        println!("password is too weak!!");
    }


}
