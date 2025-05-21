
fn main() {   
    use std::time::{SystemTime, UNIX_EPOCH};
    let start = SystemTime::now();
    match start.duration_since(UNIX_EPOCH) {
        Ok(duration) => {
            let timestamp: u64 = duration
                .as_secs();
            println!("Timestamp: {:?}", timestamp);
        }
        Err(e) => {
            println!("Error: {:?}", e);
        }
    }
}
