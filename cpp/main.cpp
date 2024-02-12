#include <iostream>
#include <fstream>
#include <vector>
#include <cmath>
#include <chrono>
#include <nlohmann/json.hpp>

using json = nlohmann::json;
using namespace std;
using namespace std::chrono;

// Point represents a point in 3D space.
class Point {
public:
    double x, y, z;

    // Constructor
    Point(double x, double y, double z) : x(x), y(y), z(z) {}

    // Calculate the distance between two points.
    double Dist(const Point& other) const {
        double xd = x - other.x;
        double yd = y - other.y;
        double zd = z - other.z;
        return sqrt(xd * xd + yd * yd + zd * zd);
    }
};

// MaxMin calculates the maximum of minimum distances between points in two vectors.
double MaxMin(const vector<Point>& path1, const vector<Point>& path2) {
    double max = 0.0;
    for (const auto& p1 : path1) {
        double min = numeric_limits<double>::max();
        for (const auto& p2 : path2) {
            double dist = p1.Dist(p2);
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

double HausdorffDistance(const vector<Point>& path1, const vector<Point>& path2){
    double r1=MaxMin(path1,path2);
    double r2=MaxMin(path2,path1);
    if (r1<r2){
        return r2;
    }else{
        return r1;
    }
}

int main() {
    // Read the JSON files and deserialize into vectors of Points.
    ifstream p1File("../path.json"), p2File("../path1.json");
    vector<Point> p1Vec, p2Vec;

    if (p1File.fail() || p2File.fail()) {
        cerr << "Failed to open files." << endl;
        return 1;
    }

    json p1Json, p2Json;
    p1File >> p1Json;
    p2File >> p2Json;

    for (const auto& point : p1Json) {
        p1Vec.emplace_back(point["x"], point["y"], point["z"]);
    }
    for (const auto& point : p2Json) {
        p2Vec.emplace_back(point["x"], point["y"], point["z"]);
    }

    // Measure the time taken for the MaxMin function.
    auto startTime = high_resolution_clock::now();

    double val = HausdorffDistance(p1Vec, p2Vec);
    cout << "res is " << val << endl;

    auto elapsed = high_resolution_clock::now() - startTime;
    cout << "spend time is: " << duration_cast<milliseconds>(elapsed).count() << " ms" << endl;

    return 0;
}