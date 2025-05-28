
fn main() {   
    use std::time::{SystemTime, UNIX_EPOCH};
    let start = SystemTime::now();
    match start.duration_since(UNIX_EPOCH) {
        Ok(duration) => {
            let timestamp: u64 = duration
                .as_secs();

            let microSecondsSubSec: u64 = duration
                .subsec_micros();

            let remainingUs: u64 = 1 - microSecondsSubSec;


            let secondsElapsed: u64 = timestamp % 86400;


            let remainingSeconds: u64 = 86400 - secondsElapsed;
            
            println!("Timestamp: {:?}", timestamp);
        }
        Err(e) => {
            println!("Error: {:?}", e);
        }
    }
}
