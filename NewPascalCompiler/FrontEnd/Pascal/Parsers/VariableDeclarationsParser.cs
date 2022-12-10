using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewPascalCompiler.Intermediate.SymbolTable.Interface;

namespace NewPascalCompiler.FrontEnd.Pascal.Parsers
{
    internal class VariableDeclarationsParser : DeclarationsParser
    {
        public VariableDeclarationsParser(PascalParser parent) : base(parent)
        {
        }

        public override ISymbolTableEntry? Parse(ISymbolTableEntry parentId, out Token token)
        {
            return base.Parse(parentId, out token);
        }

        protected List<ISymbolTableEntry> ParseIdentifierSublist(
            HashSet<TokenType> followSet,
            HashSet<TokenType> commaSet,
            out Token token)
        {
            throw new NotImplementedException();
        }
    }
}
