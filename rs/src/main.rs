use serde::{Deserialize, Serialize};
use serde_json;
use std::f64;
use std::{fs, time};

#[derive(Serialize, Deserialize, Debug)]
struct Point {
    x: f64,
    y: f64,
    z: f64,
}

impl Point {
    fn dist(&self, other: &Point) -> f64 {
        let xd = self.x - other.x;
        let yd = self.y - other.y;
        let zd = self.z - other.z;
        (xd * xd + yd * yd + zd * zd).sqrt()
    }
}

fn max_min(path1: &[Point], path2: &[Point]) -> f64 {
    let mut max_val = 0_f64;
    for p1 in path1.iter() {
        let mut min_val = f64::MAX;
        for p2 in path2.iter() {
            let dis = p1.dist(p2);
            if dis < min_val {
                min_val = dis;
            }
        }
        if min_val > max_val {
            max_val = min_val;
        }
    }
    return max_val;
}

fn hausdorff_distance(path1: &[Point], path2: &[Point]) -> f64 {
    let (d1, d2) = (max_min(path1, path2), max_min(path2, path1));
    return if d1 < d2 { d2 } else { d1 };
}

fn main() {
    let p1_str = fs::read_to_string("../path.json").expect("Failed to read path.json");
    let p2_str = fs::read_to_string("../path1.json").expect("Failed to read path1.json");

    let p1_vec: Vec<Point> = serde_json::from_str(&p1_str).expect("Failed to parse path.json");
    let p2_vec: Vec<Point> = serde_json::from_str(&p2_str).expect("Failed to parse path1.json");

    let start_time = time::Instant::now();

    let val = hausdorff_distance(&p1_vec, &p2_vec);
    println!("res is {}", val);

    let elapsed = start_time.elapsed();
    println!("spend time is: {} ms", elapsed.as_millis());
}
