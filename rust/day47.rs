
fn main() {   
    use std::io;

    let mut email1 = String::new();
    println!("Input email address: ");
    io::stdin()
        .read_line(&mut email1)
        .expect("Failed to read email");


    let has_period = email1.contains(".");
    
    println!(
        "String has period: {:?}",
        has_period
    );

    

    let has_at_sign = email1.contains("@");
    
    println!(
        "String has @ sign: {:?}",
        has_at_sign
    );
}
