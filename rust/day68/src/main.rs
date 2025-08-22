use md5;
use crossterm::event::{read, Event, KeyEvent, KeyCode, KeyEventKind};
use std::io;
use std::io::Write;
use std::fs;

fn main() {
    println!("Please input your password");

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

    let md5_password1 = format!("{:x}", md5::compute(password1));



    let _ = fs::write("rust-day68.txt", md5_password1);

}
