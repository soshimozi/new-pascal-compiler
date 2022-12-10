using System.Text;

namespace NewPascalCompiler.FrontEnd.Pascal.Tokens;

public class PascalNumberToken : PascalToken
{
    private static readonly int _maxExponent = 37;

    /**
         * Constructor.
         * @param source the source from where to fetch the token's characters.
         * @throws Exception if an error occurred.
         */
    public PascalNumberToken(Source source) : base(source)
    {
    }

    public override void Extract()
    {
        var textBuffer = new StringBuilder();
        ExtractNumber(textBuffer);
        Text = textBuffer.ToString();
    }

    protected void ExtractNumber(StringBuilder textBuffer)
    {
        string? wholeDigits = null;
        string? fractionDigits = null;
        string? exponentDigits = null;
        var exponentSign = '+';
        var sawDotDot = false;

        // assume INTEGER for now
        TokenType = TokenType.Integer;
        wholeDigits = UnsignedIntegerDigits(textBuffer);
        if (TokenType == TokenType.Error) return;

        var currentChar = CurrentChar;
        if (currentChar == '.')
        {
            if (Peek() == '.')
            {
                sawDotDot = true; // don't consume .. token
            }
            else
            {
                TokenType = TokenType.Real;

                textBuffer.Append(currentChar);
                currentChar = NextChar();

                fractionDigits = UnsignedIntegerDigits(textBuffer);
                if (TokenType == TokenType.Error) return;
            }
        }

        currentChar = CurrentChar;
        if (!sawDotDot && (currentChar == 'E' || currentChar == 'e'))
        {
            TokenType = TokenType.Real;
            textBuffer.Append(currentChar);
            currentChar = NextChar();

            if (currentChar == '+' || currentChar == '-')
            {
                textBuffer.Append(currentChar);
                exponentSign = currentChar;
                currentChar = NextChar();
            }

            exponentDigits = UnsignedIntegerDigits(textBuffer);
        }

        if (TokenType == TokenType.Integer)
        {
            var integerValue = ComputeIntegerValue(wholeDigits);
            if (TokenType != TokenType.Error) Value = integerValue;
        }
        else if (TokenType == TokenType.Real)
        {
            var realValue = ComputeFloatValue(wholeDigits, fractionDigits, exponentDigits, exponentSign);
            if (TokenType != TokenType.Error) Value = realValue;
        }
    }

    private string? UnsignedIntegerDigits(StringBuilder textBuffer)
    {
        var currentChar = CurrentChar;

        if (!char.IsDigit(currentChar))
        {
            TokenType = TokenType.Error;
            Value = PascalErrorCode.InvalidNumber;
            return null;
        }

        // Extract the digits
        var digits = new StringBuilder();
        while (char.IsDigit(currentChar))
        {
            textBuffer.Append(currentChar);
            digits.Append(currentChar);
            currentChar = NextChar();
        }

        return digits.ToString();
    }

    private int ComputeIntegerValue(string? digits)
    {
        if (digits == null)
            return 0;

        var integerValue = 0;
        int prevValue = -1;
        int index = 0;

        while (index < digits.Length && integerValue >= prevValue)
        {
            prevValue = integerValue;
            integerValue = 10 * integerValue + (int)char.GetNumericValue(digits[index++]);
        }

        if (integerValue >= prevValue)
            return integerValue;

        TokenType = TokenType.Error;
        Value = PascalErrorCode.RangeInteger;

        return 0;
    }

    private double ComputeFloatValue(string? wholeDigits, string? fractionDigits,
        string? exponentDigits, char exponentSign)
    {
        var floatValue = 0.0;
        var exponentValue = ComputeIntegerValue(exponentDigits);
        var digits = wholeDigits;

        // Negate the exponent if the exponent sign is '-'
        if (exponentSign == '-') exponentValue = exponentValue * -1;

        // If there are any fraction digits, adjust the exponent value
        // and append the fraction digits.
        if (fractionDigits != null)
        {
            exponentValue -= fractionDigits.Length;
            digits += fractionDigits;
        }

        // Check for a real number out of range error.
        if (Math.Abs(exponentValue + wholeDigits.Length) > _maxExponent)
        {
            TokenType = TokenType.Error;
            Value = PascalErrorCode.RangeReal;
            return 0.0f;
        }

        // Loop over the digits to compute the float value.
        var index = 0;
        if (digits != null)
        {
            while (index < digits?.Length)
            {
                floatValue = 10 * floatValue + char.GetNumericValue(digits[index++]);
            }
        }

        // Adjust the float value based on the exponent value.
        if (exponentValue != 0) floatValue *= Math.Pow(10, exponentValue);
        return floatValue;
    }
}