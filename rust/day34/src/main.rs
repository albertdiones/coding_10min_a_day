use chrono::Local;

fn main() {
    let current_time = Local::now();
    println!(
        "Current Date/Time: {} {}",
        current_time.format("%a %b %d %Y"),
        current_time.format("%H:%M:%S")
    );
}