fn main() {   
    use std::io::{stdin,stdout,Write};
    use std::collections::HashMap;
    let mut name=String::new();
    print!("Please enter text: ");
    let _=stdout().flush();
    stdin()
        .read_line(&mut name)
        .expect(
            "Invalid input"
        );
    
    // Split into characters
    let name_chars: Vec<char> = name
        .chars()
        .collect();

    let mut char_counts: HashMap<char, u32>
         = HashMap::new();



    for ch in name_chars {

        if char_counts.contains_key(&ch) {
            let count = char_counts
                .get_mut(&ch)
                .unwrap();
            *count+=1;
        }
        else {
            char_counts.insert(
                ch,
                1
            );
        }
    }


    for (ch, count) in char_counts.into_iter() {
        println!(
            "{}: {}", 
            ch.to_string(),
            count.to_string()
        )
    }

}
