namespace NewPascalCompiler.FrontEnd.Pascal;

public class PascalParser : Parser
{
    public PascalParser(Scanner scanner) : base(scanner)
    {
    }

    public override void Parse()
    {
        var startTime = DateTime.Now.TimeOfDay.TotalMilliseconds;

        var lastLineNumber = 0;
        foreach (var token in Scanner.Scan())
        {
            lastLineNumber = token.GetLineNumber();
        }

        var elapsedTime = DateTime.Now.TimeOfDay.TotalMilliseconds - startTime;
        OnParseCompleted(new ParseCompletedEventArgs(lastLineNumber, GetErrorCount(), elapsedTime));
    }

    public override int GetErrorCount()
    {
        return 0;
    }
}