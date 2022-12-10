namespace NewPascalCompiler.FrontEnd;

public class EOFToken : Token
{
    public EOFToken(Source source) : base(source)
    {
    }

    public override void Extract()
    {
    }
}