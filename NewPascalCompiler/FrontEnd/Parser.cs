using NewPascalCompiler.Intermediate.IntermediateCode.Interface;
using NewPascalCompiler.Intermediate.SymbolTable;
using NewPascalCompiler.Intermediate.SymbolTable.Interface;

namespace NewPascalCompiler.FrontEnd;

public abstract class Parser
{
    protected static readonly ISymbolTableStack SymbolTableStack;        // symbol table stack

    protected IIntermediateCode? IntermediateCode = null;
    protected Scanner Scanner;


    static Parser() 
    {
        SymbolTableStack = new SymbolTableStack();
    }

public event EventHandler<ParseCompletedEventArgs> ParseCompleted;
    protected Parser(Scanner scanner)
    {
        Scanner = scanner;
    }

    public abstract void Parse();
    public abstract int GetErrorCount();

    public Token CurrentToken => Scanner.CurrentToken;
    public Token GetNextToken()
    {
        return Scanner.NextToken();
    }

    protected virtual void OnParseCompleted(ParseCompletedEventArgs e)
    {
        ParseCompleted?.Invoke(this, e);
    }
}

public record ParseCompletedEventArgs(int LineNumber, int ErrorCount, double ElapsedTime);
