using NewPascalCompiler.FrontEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPascalCompiler.FrontEnd.Pascal.Tokens
{
    public class PascalErrorToken : PascalToken
    {
        public PascalErrorToken(Source source, PascalErrorCode errorCode, string tokenText) : base(source)
        {
            Text = tokenText;
            TokenType = TokenType.Error;
            Value = errorCode;
        }

        public override void Extract()
        {
        }
    }
}
