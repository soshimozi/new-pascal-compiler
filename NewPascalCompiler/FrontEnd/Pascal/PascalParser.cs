using NewPascalCompiler.Intermediate.SymbolTable;

namespace NewPascalCompiler.FrontEnd.Pascal;

public class PascalParser : Parser
{
    protected static readonly int MaxErrors = 25;

    private static int _errorCount = 0;   // count of syntax errors

    public event EventHandler<SyntaxErrorEventArgs> SyntaxError;
    public PascalParser(Scanner scanner) : base(scanner)
    {
    }

    public PascalParser(PascalParser parent) : base(parent.Scanner)
    {

    }

    public override void Parse()
    {
        var startTime = DateTime.Now.TimeOfDay.TotalMilliseconds;

        Predefined.Initialize(SymbolTableStack);

        try
        {
            var token = GetNextToken();

            // Parse a program.
            //ProgramParser programParser = new ProgramParser(this);
            //programParser.parse(token, null);
            //token = currentToken();

            //// Send the parser summary message.
            //float elapsedTime = (System.currentTimeMillis() - startTime) / 1000f;
            //sendMessage(new Message(PARSER_SUMMARY,
            //    new Number[] {token.getLineNumber(),
            //        getErrorCount(),
            //        elapsedTime}));
        }
        catch (IOException ex)
        {
            //errorHandler.abortTranslation(IO_ERROR, this);
        }

        var lastLineNumber = 0;
        foreach (var token in Scanner.Scan())
        {
            lastLineNumber = token.GetLineNumber();
        }

        var elapsedTime = DateTime.Now.TimeOfDay.TotalMilliseconds - startTime;
        OnParseCompleted(new ParseCompletedEventArgs(lastLineNumber, GetErrorCount(), elapsedTime));
    }

    public Token Synchronize(HashSet<TokenType> syncSet)
    {
        var token = CurrentToken;

        // If the current token is not in the synchronization set,
        // then it is unexpected and the parser must recover.
        if (syncSet.Contains(token.TokenType)) return token;

        // Flag the unexpected token.
        HandleSyntaxError(token.GetLineNumber(), token.GetPosition(), token.GetText(), PascalErrorCode.UnexpectedToken);

        // Recover by skipping tokens that are not
        // in the synchronization set.
        do
        {
            token = GetNextToken();
        } while (token is not EOFToken && !syncSet.Contains(token.TokenType));

        return token;
    }

    public override int GetErrorCount()
    {
        return _errorCount;
    }


    protected void HandleSyntaxError(Token token, PascalErrorCode errorCode)
    {
        HandleSyntaxError(token.GetLineNumber(), token.GetPosition(), token.GetText(), errorCode);
    }

    protected void HandleSyntaxError(int line, int position, string? text, PascalErrorCode errorCode)
    {
        //parser.sendMessage(new Message(SYNTAX_ERROR,
        //    new Object[] {token.getLineNumber(),
        //        token.getPosition(),
        //        token.getText(),
        //        errorCode.toString()}));

        OnSyntaxError(new SyntaxErrorEventArgs(line, position, text, errorCode.ToString()));

        if (++_errorCount > MaxErrors)
        {
            //abortTranslation(TOO_MANY_ERRORS, parser);
        }

    }

    protected virtual void OnSyntaxError(SyntaxErrorEventArgs e)
    {
        SyntaxError?.Invoke(this, e);
    }
}

public record SyntaxErrorEventArgs(int LineNumber, int Position, string? Text, string ErrorCode);