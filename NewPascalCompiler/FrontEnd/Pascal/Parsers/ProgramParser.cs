using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewPascalCompiler.Intermediate.SymbolTable.Interface;

namespace NewPascalCompiler.FrontEnd.Pascal.Parsers
{
    public class ProgramParser : DeclarationsParser
    {
        private static readonly HashSet<TokenType> ProgramStartSet = new()
        {
            TokenType.Program, 
            TokenType.SemiColon,
            TokenType.Const,
            TokenType.Type,
            TokenType.Var,
            TokenType.Procedure,
            TokenType.Function,
            TokenType.Begin
        };

        public ProgramParser(PascalParser parser) : base(parser)
        {
        }

        public override ISymbolTableEntry? Parse(ISymbolTableEntry? parentId, out Token token)
        {
            token = Synchronize(ProgramStartSet);

            // Parse the program.
            var routineParser = new DeclaredRoutineParser(this);
            routineParser.Parse(token, parentId);

            // Look for the final period.
            token = CurrentToken;
            if (token.TokenType != TokenType.Dot)
            {
                HandleSyntaxError(token, PascalErrorCode.MissingPeriod);
            }

            return null;
        }
    }
}
