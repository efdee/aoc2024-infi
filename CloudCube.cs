public class CloudCube
{
    private readonly int[,,] _cells = new int[30, 30, 30];
    
    public void ReadData(string[] lines)
    {
        var vm = new VirtualMachine();
        _cells.ForEach3D((_, x, y, z) => _cells[x, y, z] = vm.Run(lines, x, y, z));
    }

    public int GetDataSum() => _cells.Sum3D((cell, _, _, _) => cell);
    public int CountClouds() => _cells.Sum3D((_, x, y, z) => TestForCloudAndWipeCells(x, y, z) ? 1 : 0);

    private bool TestForCloudAndWipeCells(int x, int y, int z)
    {
        if (_cells[x, y, z] <= 0) return false;
        
        _cells[x, y, z] = 0;
        if (x + 1 < 30) TestForCloudAndWipeCells(x + 1, y, z);
        if (x > 0) TestForCloudAndWipeCells(x - 1, y, z);
        if (y + 1 < 30) TestForCloudAndWipeCells(x, y + 1, z);
        if (y > 0) TestForCloudAndWipeCells(x, y - 1, z);
        if (z + 1 < 30) TestForCloudAndWipeCells(x, y, z + 1);
        if (z > 0) TestForCloudAndWipeCells(x, y, z - 1);
        return true;
    }
}