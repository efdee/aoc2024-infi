public class VirtualMachine
{
    private readonly Dictionary<string, Action<string>> _commands;

    private int _programCounter;
    private Dictionary<string, int> _vars;
    private bool _halted = false;
    private Stack<int> _stack;

    public VirtualMachine()
    {
        _commands = new Dictionary<string, Action<string>>
        {
            ["push"] = Push,
            ["add"] = Add,
            ["jmpos"] = Jmpos,
            ["ret"] = Ret
        };
    }

    #region Opcodes
    
    private void Ret(string obj)
    {
        _halted = true;
    }

    private void Jmpos(string offset)
    {
        if (!int.TryParse(offset, out var o))
            throw new InvalidOperationException("Unknown offset value: " + offset);
        var v = _stack.Pop();
        if (v >= 0)
            _programCounter += o; // -1 because the counter is incremented by 1 already
    }

    private void Add(string obj)
    {
        var v1 = _stack.Pop();
        var v2 = _stack.Pop();
        var sum = v1 + v2;
        _stack.Push(sum);
    }

    private void Push(string value)
    {
        if (_vars.TryGetValue(value.ToLower(), out var i1))
            _stack.Push(i1);
        else if (int.TryParse(value, out var i2))
            _stack.Push(i2);
        else throw new InvalidOperationException("Unknown value: " + value);
    }

    #endregion
    
    public int Run(string[] lines, int x, int y, int z)
    {
        _halted = false;
        _programCounter = 0;
        _stack = new();
        _vars = new() { ["x"] = x, ["y"] = y, ["z"] = z };
        while (!_halted) 
            ExecuteLine(lines[_programCounter++]);
        return _stack.Peek();
    }
    
    private void ExecuteLine(string line)
    {
        var parts = line.Split(' ');
        _commands[parts[0]].Invoke(parts.Length > 1 ? parts[1] : "");
    }
}