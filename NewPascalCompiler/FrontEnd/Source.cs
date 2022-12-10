namespace NewPascalCompiler.FrontEnd;

public class Source : IDisposable
{
    public static readonly char EOL = '\n'; // end of line character
    public static readonly char EOF = '\0'; // end of file
    private readonly BufferedStream _stream;
    private readonly StreamReader _reader;

    private string? _line;
    private int _lineNumber;
    private int _currentPosition;

    public Source(BufferedStream stream)
    {
        _reader = new StreamReader(stream);
        _stream = stream;
        _lineNumber = 0;
        _currentPosition = -2;

    }

    public int LineNumber => _lineNumber;
    public int CurrentPosition => _currentPosition;

    public char CurrentChar()
    {
        if (_currentPosition == -2)
        {
            ReadLine();
            return NextChar();
        }

        if (_line == null)
        {
            return EOF;
        }

        if (_currentPosition == -1 || _currentPosition == _line.Length)
        {
            return EOL;
        }

        if (_currentPosition <= _line.Length) return _line[_currentPosition];

        ReadLine();
        return NextChar();

    }

    public char NextChar()
    {
        _currentPosition++;
        return CurrentChar();
    }

    public char Peek()
    {
        CurrentChar();
        if (_line == null) return EOF;

        var nextPosition = _currentPosition + 1;
        return nextPosition < _line.Length ? _line[_currentPosition] : EOL;
    }

    private void ReadLine()
    {
        _line = _reader.ReadLine();
        _currentPosition = -1;

        if (_line != null)
        {
            _lineNumber++;
        }
    }

    public void Dispose()
    {
        _stream.Dispose();
        _reader.Dispose();
    }
}