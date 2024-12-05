var lines = File.ReadAllLines("input-infi.txt");

var cube = new CloudCube();
cube.ReadData(lines);
Console.WriteLine("Sum is " + cube.GetDataSum());
Console.WriteLine("Number of clouds is " + cube.CountClouds());