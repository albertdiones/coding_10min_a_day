use chrono::{DateTime, TimeZone, Utc};

fn main() {
    use std::time::{SystemTime, UNIX_EPOCH};
    let start = SystemTime::now();
    match start.duration_since(UNIX_EPOCH) {
        Ok(duration) => {
    
            let yesterday: DateTime<Utc> = Utc.timestamp_opt(
                (duration.as_secs()-86400).try_into().unwrap(), 
                0
            ).unwrap();
            println!("Yesterday: {}", yesterday);
        }
        Err(e) => {
            println!("Error: {:?}", e);
        }
    }
}