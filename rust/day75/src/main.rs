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
                other => {
                }
            }
        }
    }

    let mut has_lowercase:bool = false;
    for password_byte in password1.bytes() {
        if password_byte >= 97 && password_byte <= 122 {
            has_lowercase = true;
        }
    }
    println!("");
    if !has_lowercase {
        println!("password is too weak!!");
    }
    else {
        println!("password is valid!!");
    }


}
