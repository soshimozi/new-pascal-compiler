using ZoeyDesktopNavigationParser.FrontEnd.Pascal.Tokens;

namespace NewPascalCompiler.FrontEnd;

public abstract class Scanner
{
    protected Source Source;
    private Token? _currentToken;

    protected Scanner(Source source)
    {
        Source = source;
    }

    public Token CurrentToken => _currentToken;

    public Token NextToken()
    {
        _currentToken = ExtractToken();
        return _currentToken;
    }

    protected abstract Token ExtractToken();

    public char CurrentChar()
    {
        return Source.CurrentChar();
    }

    public char NextChar()
    {
        return Source.NextChar();
    }

    public IEnumerable<Token> Scan()
    {
        Token token;
        do
        {
            token = ExtractToken();
            if (token is EOFToken) continue;

            yield return token;

        } while (token is not EOFToken);
    }


}