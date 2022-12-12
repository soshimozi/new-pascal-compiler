using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewPascalCompiler.Intermediate.SymbolTable.Interface;

namespace NewPascalCompiler.FrontEnd.Pascal.Parsers
{
    public class DeclarationsParser : PascalParser
    {
        private static readonly HashSet<TokenType> DeclarationStartSet = new ()
        {
            TokenType.Const,
            TokenType.Type,
            TokenType.Var,
            TokenType.Procedure,
            TokenType.Function,
            TokenType.Begin
        };

        private static readonly HashSet<TokenType> TypeStartSet = new()
        {
            TokenType.Type,
            TokenType.Var,
            TokenType.Procedure,
            TokenType.Function,
            TokenType.Begin
        };

        private static readonly HashSet<TokenType> VarStartSet = new()
        {
            TokenType.Var,
            TokenType.Procedure,
            TokenType.Function,
            TokenType.Begin
        };

        private static readonly HashSet<TokenType> RoutineStartSet = new()
        {
            TokenType.Procedure,
            TokenType.Function,
            TokenType.Begin
        };

        public DeclarationsParser(PascalParser parent) : base(parent)
        {
        }

        public virtual ISymbolTableEntry? Parse(ISymbolTableEntry parentId, out Token token)
        {
            token = Synchronize(DeclarationStartSet);

            if (token.TokenType == TokenType.Const)
            {
                token = GetNextToken();  // consume CONST

                var constantDefinitionsParser =
                    new ConstantDefinitionsParser(this);
                constantDefinitionsParser.Parse(token, null);
            }

            token = Synchronize(RoutineStartSet);
            TokenType tokenType = token.TokenType;

            while ((tokenType == TokenType.Procedure) || (tokenType == TokenType.Function))
            {
                var routineParser =
                    new DeclaredRoutineParser(this);
                routineParser.Parse(parentId, ref token);

                // Look for one or more semicolons after a definition.
                token = CurrentToken;
                if (token.TokenType == TokenType.SemiColon)
                {
                    while (token.TokenType == TokenType.SemiColon)
                    {
                        token = GetNextToken();  // consume the ;
                    }
                }

                token = Synchronize(RoutineStartSet);
                tokenType = token.TokenType;
            }

            return null;
        }

    }
}
