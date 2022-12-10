using NewPascalCompiler.Intermediate.IntermediateCode.Interface;
using ZoeyDesktopNavigationParser.Symbols.Interface;

namespace NewPascalCompiler.FrontEnd;

public abstract class Parser
{
    protected static ISymbolTable? SymbolTable = null;
    protected IIntermediateCode? IntermediateCode = null;
    protected Scanner Scanner;

    public event EventHandler<ParseCompletedEventArgs> ParseCompleted;
    protected Parser(Scanner scanner)
    {
        Scanner = scanner;
    }

    public abstract void Parse();
    public abstract int GetErrorCount();

    public Token CurrentToken => Scanner.CurrentToken;
    public Token NextToken => Scanner.NextToken();

    protected virtual void OnParseCompleted(ParseCompletedEventArgs e)
    {
        ParseCompleted?.Invoke(this, e);
    }
}

public record ParseCompletedEventArgs(int LineNumber, int ErrorCount, double ElapsedTime);
