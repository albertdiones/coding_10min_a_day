fn main() {
    use std::io;
    let mut message_input1 = String::new();
    println!("Input your message: ");
    io::stdin()
        .read_line(&mut message_input1)
        .expect("Failed to read message");
    let mut message1 = String::from(message_input1.trim());

    
    let bad_words = vec![
        "fuck",
        "suck",
        "faggot",
        "asshole",
    ];

    for bad_word in bad_words {

        let char_count = bad_word.len();

        message1 = message1.replace(
            &bad_word,
            &std::iter::repeat("*").take(char_count).collect::<String>()
        ).to_string()
    }

    println!("Message: {:?}", message1);
}