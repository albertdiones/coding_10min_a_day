fn get_year(x: u64) -> u32 {
    return (
        x as f64/(60.0*60.0*24.0*365.25)
    ).floor() as u32 
    + 1970;
}

fn get_month(x: u64) -> u32 {
    let year_days = ((
        (x-365-365) as f64/(60.0*60.0*24.0)
    ) % 365.25).floor() as u32;

    let year = get_year(x);
    let is_leap_year = year % 4 == 0;
    let mut leap_year_offset: u32 = 0;
    
    if is_leap_year {
        leap_year_offset = 1;
    } 

    if year_days <= 31 {
        return 1;
    }
    if year_days <= (31+28+leap_year_offset) {
        return 2;
    }
    if year_days <= (31+28+leap_year_offset+31) {
        return 3;
    }
    if year_days <= (31+28+leap_year_offset+31+30) {
        return 4;
    }
    
    if year_days <= (31+28+leap_year_offset+31+30+31) {
        return 5;
    }

    return 0;
}


fn main() {   
    use std::time::{SystemTime, UNIX_EPOCH};
    let start = SystemTime::now();
    match start.duration_since(UNIX_EPOCH) {
        Ok(duration) => {
            let timestamp: u64 = duration
                .as_secs();
            let year = get_year(timestamp);
            println!("Timestamp: {:?}", timestamp);
            println!("Year: {:?}", year);
            println!("Month: {:?}", get_month(timestamp));
        }
        Err(e) => {
            println!("Error: {:?}", e);
        }
    }
}
