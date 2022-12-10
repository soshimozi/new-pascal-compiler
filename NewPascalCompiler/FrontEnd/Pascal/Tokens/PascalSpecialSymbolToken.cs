using NewPascalCompiler.FrontEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPascalCompiler.FrontEnd.Pascal.Tokens
{
    public class PascalSpecialSymbolToken : PascalToken
    {
        public PascalSpecialSymbolToken(Source source) : base(source)
        {
        }

        public override void Extract()
        {
            var currentChar = CurrentChar;

            Text = currentChar.ToString();
            TokenType = TokenType.Error;

            switch (currentChar)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                case ',':
                case ';':
                case '\'':
                case '=':
                case '(':
                case ')':
                case '[':
                case ']':
                case '{':
                case '}':
                case '^':
                    NextChar();
                    break;

                case ':':
                    currentChar = NextChar();

                    if (currentChar == '=')
                    {
                        Text += currentChar;
                        NextChar();
                    }

                    break;

                case '<':
                    currentChar = NextChar();
                    if (currentChar == '=')
                    {
                        Text += currentChar;
                        NextChar();
                    }
                    else if (currentChar == '>')
                    {
                        Text += currentChar;
                        NextChar();
                    }

                    break;

                case '>':
                    currentChar = NextChar();
                    if (currentChar == '=')
                    {
                        Text += currentChar;
                        NextChar();
                    }

                    break;


                case '.':
                    currentChar = NextChar();
                    if (currentChar == '.')
                    {
                        Text += currentChar;
                        NextChar();
                    }

                    break;


                default:
                    NextChar();
                    TokenType = TokenType.Error;
                    Value = PascalErrorCode.InvalidCharacter;
                    break;
            }

            if (TokenType == TokenType.Error) return;

            if (PascalScanner.TryGetReservedWord(Text, out var type))
            {
                TokenType = type;
            }
        }
    }
}
