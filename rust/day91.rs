use std::fs::{OpenOptions, read_to_string};
use std::io::{Write, BufRead, BufReader};
use std::error::Error;

const DB_FILE: &str = "__db.csv";

fn set_csv_to_blank() {
    // Open file with truncate flag to clear contents (create if not exists)
    let _file = OpenOptions::new()
        .create(true)
        .write(true)
        .truncate(true)
        .open(DB_FILE);
    return;
}



pub fn add_to_csv(row_values: &mut Vec<String>) -> usize {
    // Replace inner quotes and wrap each value in quotes
    for value in row_values.iter_mut() {
        *value = format!("\"{}\"", value.replace("\"", "\\\""));
    }

    // Join row values into CSV format
    let row = format!("{}\n", row_values.join(","));

    // Open file in append/create mode
    let mut file = OpenOptions::new()
        .create(true)
        .append(true)
        .open(DB_FILE)
        .expect("Failed to open file");

    // Write row
    file.write_all(row.as_bytes()).expect("Failed to write row");

    // Read entire file to count number of rows
    let rows = read_to_string(DB_FILE).expect("Failed to read file");
    rows.matches('\n').count()
}

pub fn trim_quotes(values: Vec<String>) -> Vec<String> {
    values
        .into_iter()
        .map(|v| v.trim_matches('"').to_string())
        .collect()
}

pub fn read_csv_row(row_id: usize) -> Vec<String> {
    let file = std::fs::File::open(DB_FILE).expect("Failed to open file");
    let reader = BufReader::new(file);

    let rows: Vec<String> = reader.lines().map(|l| l.unwrap()).collect();

    if row_id >= rows.len() {
        panic!("Row ID out of range");
    }

    let row = &rows[row_id];
    let split: Vec<String> = row.split(',').map(|s| s.to_string()).collect();
    trim_quotes(split)
}

pub fn get_csv_row_count() -> usize {
    let file = std::fs::File::open(DB_FILE).expect("Failed to open file");
    let reader = BufReader::new(file);

    reader.lines().count()
}

pub fn read_all_csv_row() -> Vec<Vec<String>> {
    let num_rows = get_csv_row_count();
    println!("numRows {}", num_rows);

    let mut rows: Vec<Vec<String>> = Vec::with_capacity(num_rows);
    for i in 0..num_rows {
        rows.push(read_csv_row(i));
    }
    rows
}

pub fn get_csv_column_number(column_name: &str) -> usize {
    let columns = read_csv_row(0);
    for (i, column) in columns.iter().enumerate() {
        if column.trim() == column_name {
            return i + 1; // 1-based index like Go/C#
        }
    }
    panic!("Column not found");
}

pub fn get_csv_cell_value(row_id: usize, column_number:usize) -> String {
    let row = read_csv_row(row_id);

    let colIndex = column_number - 1;

    return row[column_number-1].clone();
}

fn delete_csv_row(delete_row_id: usize) {
    let mut rows = read_all_csv_row();

    set_csv_to_blank();

    for (row_id, row) in rows.iter_mut().enumerate() {
        if row_id == delete_row_id {
            continue;
        }

        add_to_csv(row);

    }
}

fn main() {
    delete_csv_row(6);
}
