fn greatest(x: f32, y: f32, z: f32) -> f32 {

    if x > y && x > z {
        return x;
    }
    
    if y > x && y > z {
        return y;
    }
    
    if z > x && z > y {
        return z;
    }

    return x;
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

    
    
    let mut number3 = String::new();
    println!("Input third number: ");
    io::stdin()
        .read_line(&mut number3)
        .expect("Failed to read number");
    let n3 = number3.trim().parse().expect("Not a number");

    let result:f32 = greatest(n1,n2,n3);



    println!(
        "{}",
        "Greatest: ".to_owned() + 
        &result.to_string()
    );

}
