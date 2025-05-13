fn deduct(x: f32, y: f32) -> f32 {
    let diff: f32 = x - y;
    return diff.max(0.0);
}


fn main() {   
    use std::io;
    
    let mut number1 = String::new();
    println!("Input first number: ");
    io::stdin()
        .read_line(&mut number1)
        .expect("Failed to read number");
    let n1 = number1.trim().parse().expect("Not a number");
    

    
    
    let mut number2 = String::new();
    println!("Input second number: ");
    io::stdin()
        .read_line(&mut number2)
        .expect("Failed to read number");
    let n2 = number2.trim().parse().expect("Not a number");


    let result:f32 = deduct(n1,n2);



    println!(
        "{}",
        "Result: ".to_owned() + 
        &result.to_string()
    );

}
