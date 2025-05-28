
fn main() {   
    use std::time::{SystemTime, UNIX_EPOCH};
    let start = SystemTime::now();
    match start.duration_since(UNIX_EPOCH) {
        Ok(duration) => {
            let timestamp: u64 = duration
                .as_secs() + (8*3600);

            let micro_seconds_subsec: u32 = duration
                .subsec_micros();

            let remaining_us: u32 = 1000000 - micro_seconds_subsec;


            let seconds_elapsed: u64 = timestamp % 86400;


            let remaining_seconds: u64 = 86400 - seconds_elapsed;


            let remaining_seconds_for_the_day = 
                (remaining_seconds*1000000)
                + (remaining_us as u64);
            
            println!("Remaining Microseconds: {:?}", remaining_seconds_for_the_day);
        }
        Err(e) => {
            println!("Error: {:?}", e);
        }
    }
}
