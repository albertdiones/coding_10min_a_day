fn main() {
    let str1: String = "Hello".to_string(); // or use let str1 = "Hello";
    let int1: u32 = 11232532;
    let float1: f32 = 11232312.99999;
    let bool1: bool = false;

    println!(
        "{}",
        str1
        + "-" +
        &int1.to_string()
        + "-" +
        &float1.to_string()
        + "-" +
        &bool1.to_string()
    );
}
