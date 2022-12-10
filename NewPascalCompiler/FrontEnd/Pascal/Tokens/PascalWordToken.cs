using System.Text;

namespace NewPascalCompiler.FrontEnd.Pascal.Tokens;

public class PascalWordToken : PascalToken
{
    public PascalWordToken(Source source) : base(source)
    {
    }

    public override void Extract()
    {
        var stringBuilder = new StringBuilder();
        var currentChar = CurrentChar;

        while (char.IsLetterOrDigit(currentChar))
        {
            stringBuilder.Append(currentChar);
            currentChar = NextChar();
        }

        Text = stringBuilder.ToString();

        // assume it's an identifier
        TokenType = TokenType.Identifier;
        if (PascalScanner.TryGetReservedWord(Text, out var tokenType))
        {
            TokenType = tokenType;
        }
    }
}