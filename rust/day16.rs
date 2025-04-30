fn main() {
    // "sitaw" is &str, here, 
    // we convert it to String object(???)
    let sitaw: String = "sitaw".to_string(); 
    
    let mut vegetables = vec![
        String::from("singkamas"),
        String::from("talong"),
        String::from("sigarilyas"),
        String::from("mani"),
        sitaw   
    ];

    println!("{:?}", vegetables);

    // You can now push another item
    vegetables.push(String::from("bataw"));

    vegetables.push(String::from("patani"));

    println!("{:?}", vegetables);
}