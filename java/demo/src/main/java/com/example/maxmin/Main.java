package com.example.maxmin;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.List;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import java.lang.reflect.Type;
import java.util.ArrayList;
import java.time.Duration;
import java.time.Instant;

class Point {
    private double x;
    private double y;
    private double z;

    // Constructors, getters, and setters might be required for deserialization and access
    public Point() {
    }

    public Point(double x, double y, double z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public double getX() {
        return x;
    }

    public void setX(double x) {
        this.x = x;
    }

    public double getY() {
        return y;
    }

    public void setY(double y) {
        this.y = y;
    }

    public double getZ() {
        return z;
    }

    public void setZ(double z) {
        this.z = z;
    }

    // Dist calculates the distance between two points
    public double dist(Point other) {
        double xd = this.x - other.x;
        double yd = this.y - other.y;
        double zd = this.z - other.z;
        return Math.sqrt(xd * xd + yd * yd + zd * zd);
    }
}

public class Main {
    
    // MaxMin calculates the maximum of minimum distances between points in two lists
    static double maxMin(List<Point> path1, List<Point> path2) {
        double max = 0.0;
        for (Point p1 : path1) {
            double min = Double.MAX_VALUE;
            for (Point p2 : path2) {
                double dist = p1.dist(p2);
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

    public static void main(String[] args) {
        try {
            // Read the JSON files and deserialize into lists of Points
            String p1Json = new String(Files.readAllBytes(Paths.get("../../path.json")));
            String p2Json = new String(Files.readAllBytes(Paths.get("../../path1.json")));

            Type listType = new TypeToken<ArrayList<Point>>(){}.getType();
            List<Point> p1Vec = new Gson().fromJson(p1Json, listType);
            List<Point> p2Vec = new Gson().fromJson(p2Json, listType);

            double _v1= maxMin(p1Vec, p2Vec);
            double _v2= maxMin(p1Vec, p2Vec);
            double _v3= maxMin(p1Vec, p2Vec);
            // Measure the time taken for the maxMin function
            Instant startTime = Instant.now();

            double val = maxMin(p1Vec, p2Vec);
            System.out.printf("res is %f\n", val);

            Duration elapsed = Duration.between(startTime, Instant.now());
            System.out.printf("spend time is: %d ms\n", elapsed.toMillis());
            
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}