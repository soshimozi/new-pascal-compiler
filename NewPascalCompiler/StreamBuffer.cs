namespace ZoeyDesktopNavigationParser;

public class StreamBuffer
{
    private int _start;
    private int _current;
    private int _line;

    private Stack<int> _indentLevels = new Stack<int>();
    private int _indentLevel = 0;
    private int _currentIndentCount = 0;

    private bool _indentChecked = false;
    private bool _indentMode = false;
    private bool? _usingTabs = null;
    private readonly string _text;

    public StreamBuffer(string text)
    {
        _text = text;
        _start = 0;
        _current = 0;
        _line = 1;
    }

    public StreamBuffer(StreamReader streamReader) : this(streamReader.ReadToEnd())
    {
    }

    public int LineNumber => _line;

    public bool IsAtEnd()
    {
        return _current >= _text.Length;
    }

    public void Advance()
    {
        _current++;
    }

    public char Peek()
    {
        return _current >= _text.Length ? '\0' : _text[_current];
    }

    public char CurrentChar => Peek();

    public char PeekNext()
    {
        if (IsAtEnd()) return '\0';
        return _text[_current + 1];
    }

    public char PeekPrev()
    {
        return _current - 1 < 0 ? '\0' : _text[_current - 1];
    }

    public char GoBack()
    {
        if (_current - 1 < 0) return '\0';
        _current--;
        return Peek();
    }

    public void SkipWhitespace()
    {
        for (; ; )
        {
            var c = Peek();
            switch (c)
            {
                case ' ':
                case '\r':
                case '\t': 
                    Advance();
                    break;
                case '\n':
                    _line++;
                    Advance();
                    break;
                case '#':
                    while (Peek() != '\n' && !IsAtEnd()) Advance();
                    break;

                default:
                    return;

            }
        }
    }
}