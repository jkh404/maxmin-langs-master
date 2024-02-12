package main

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"math"
	"time"
)

// Point represents a point in 3D space.
type Point struct {
	X float64 `json:"x"`
	Y float64 `json:"y"`
	Z float64 `json:"z"`
}

// Dist calculates the distance between two points.
func (p Point) Dist(other Point) float64 {
	xd := p.X - other.X
	yd := p.Y - other.Y
	zd := p.Z - other.Z
	return math.Sqrt(xd*xd + yd*yd + zd*zd)
}

// MaxMin calculates the maximum of minimum distances between points in two slices.
func MaxMin(path1, path2 []Point) float64 {
	max := 0.0
	for _, p1 := range path1 {
		min := math.MaxFloat64
		for _, p2 := range path2 {
			dist := p1.Dist(p2)
			if dist < min {
				min = dist
			}
		}
		if min > max {
			max = min
		}
	}
	return max
}

func HausdorffDistance(path1, path2 []Point) float64 {
	r1 := MaxMin(path1, path2)
	r2 := MaxMin(path2, path1)
	if r1 < r2 {
		return r2
	} else {
		return r1
	}
}

func main() {
	// Read the JSON files and deserialize into slices of Points.
	p1Bytes, err := ioutil.ReadFile("../path.json")
	if err != nil {
		panic(err)
	}
	p2Bytes, err := ioutil.ReadFile("../path1.json")
	if err != nil {
		panic(err)
	}

	var p1Vec, p2Vec []Point
	err = json.Unmarshal(p1Bytes, &p1Vec)
	if err != nil {
		panic(err)
	}
	err = json.Unmarshal(p2Bytes, &p2Vec)
	if err != nil {
		panic(err)
	}

	// Measure the time taken for the MaxMin function.
	startTime := time.Now()

	val := HausdorffDistance(p1Vec, p2Vec)
	fmt.Printf("res is %v\n", val)

	elapsed := time.Since(startTime)
	fmt.Printf("spend time is: %v ms\n", elapsed.Milliseconds())
}
