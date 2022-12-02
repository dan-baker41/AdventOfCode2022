using AdventOfCodeDay1Part2;

const string fileName = "input.txt";
const int numberOfElves = 3;

CalorieCounter counter = new CalorieCounter(fileName);
counter.DisplayHighestCounts(numberOfElves);