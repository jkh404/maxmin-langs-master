const fs = require('fs');
const path = require('path');

// Point represents a point in 3D space.
class Point {
  constructor(x, y, z) {
    this.x = x;
    this.y = y;
    this.z = z;
  }

  // Dist calculates the distance between two points.
  dist(other) {
    const xd = this.x - other.x;
    const yd = this.y - other.y;
    const zd = this.z - other.z;
    return Math.sqrt(xd * xd + yd * yd + zd * zd);
  }
}

// MaxMin calculates the maximum of minimum distances between points in two arrays.
function maxMin(path1, path2) {
  let max = 0.0;
  for (const p1 of path1) {
    let min = Number.MAX_VALUE;
    for (const p2 of path2) {
      const dist = p1.dist(p2);
      if (dist < min) {
        min = dist;
      }
    }
    if (min > max) {
      max = min;
    }
  }
  return max;
}

function hausdorffDistance(path1,path2){
  let r1=maxMin(path1,path2);
  let r2=maxMin(path2,path1);
  if(r1<r2){
    return r2;
  }else{
    return r1;
  }
}

async function main() {
  try {
    // Read the JSON files and deserialize into arrays of Points.
    const p1Content = await fs.promises.readFile(path.join(__dirname, '../path.json'), 'utf8');
    const p2Content = await fs.promises.readFile(path.join(__dirname, '../path1.json'), 'utf8');

    const p1Vec = JSON.parse(p1Content).map(p => new Point(p.x, p.y, p.z));
    const p2Vec = JSON.parse(p2Content).map(p => new Point(p.x, p.y, p.z));

    const _a= hausdorffDistance(p1Vec, p2Vec);
    const _b= hausdorffDistance(p1Vec, p2Vec);
    const _c= hausdorffDistance(p1Vec, p2Vec);
    // Measure the time taken for the MaxMin function.
    const startTime = process.hrtime.bigint();

    const val = hausdorffDistance(p1Vec, p2Vec);
    console.log(`res is ${val}`);

    const elapsed = process.hrtime.bigint() - startTime;
    console.log(`spend time is: ${elapsed / 1000000n} ms`);
  } catch (err) {
    console.error(err);
    process.exit(1);
  }
}

main();