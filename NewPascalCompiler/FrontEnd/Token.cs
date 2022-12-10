namespace NewPascalCompiler.FrontEnd;

public class Token
{
    protected TokenType TokenType;
    protected string? Text;
    protected object? Value;
    protected Source Source;
    protected int LineNumber;
    protected int Position;

    public Token(Source source)
    {
        Source = source;
        LineNumber = source.LineNumber;
        Position = source.CurrentPosition;

        //Extract();
    }

    public virtual void Extract()
    {
        Text = char.ToString(CurrentChar);
        Value = null;

        NextChar();
    }

    public TokenType GetTokenType()
    {
        return TokenType;
    }

    public string? GetText()
    {
        return Text;
    }

    public object? GetValue()
    {
        return Value;
    }

    public int GetLineNumber()
    {
        return LineNumber;
    }

    public int GetPosition()
    {
        return Position;
    }

    protected char CurrentChar => Source.CurrentChar();

    protected char NextChar()
    {
        return Source.NextChar();
    }
    protected char Peek()
    {
        return Source.Peek();
    }
}