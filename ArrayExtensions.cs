public static class ArrayExtensions
{
    public static void ForEach3D(this int[,,] cells, Action<int, int, int, int> action)
    {
        var lx = cells.GetLength(0);
        var ly = cells.GetLength(1);
        var lz = cells.GetLength(2);

        for (var x = 0; x < lx; x++)
        for (var y = 0; y < ly; y++)
        for (var z = 0; z < lz; z++)
            action(cells[x, y, z], x, y, z);
    }

    public static int Sum3D(this int[,,] cells, Func<int, int, int, int, int> action)
    {
        var sum = 0;
        cells.ForEach3D((cell, x, y, z) => sum += action(cell, x, y, z));
        return sum;
    }
}