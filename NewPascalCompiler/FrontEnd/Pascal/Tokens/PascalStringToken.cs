using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPascalCompiler.FrontEnd.Pascal.Tokens
{
    public class PascalStringToken : PascalToken
    {
        public PascalStringToken(Source source) : base(source)
        {
        }

        public override void Extract()
        {
            var textBuffer = new StringBuilder();
            var valueBuffer = new StringBuilder();

            var currentChar = NextChar();
            textBuffer.Append('\'');

            do
            {
                if (char.IsWhiteSpace(currentChar))
                {
                    currentChar = ' ';
                }

                if (currentChar != '\'' && currentChar != Source.EOF)
                {
                    textBuffer.Append(currentChar);
                    valueBuffer.Append(currentChar);
                    currentChar = NextChar();
                }

                if (currentChar == '\'')
                {
                    while (currentChar == '\'' && Peek() == '\'')
                    {
                        textBuffer.Append("''");
                        valueBuffer.Append(currentChar); // append single-quote

                        NextChar();
                        currentChar = NextChar();
                    }
                }
            } while (currentChar != '\'' && currentChar != Source.EOF);

            if (currentChar == '\'')
            {
                NextChar(); // consume teh final quote
                textBuffer.Append('\'');

                TokenType = TokenType.String;
                Value = valueBuffer.ToString();
            }
            else
            {
                TokenType = TokenType.Error;
                Value = PascalErrorCode.UnexpectedEOF;
            }
        }
    }
}
