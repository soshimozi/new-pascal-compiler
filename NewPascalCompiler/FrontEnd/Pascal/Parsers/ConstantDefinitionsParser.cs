using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewPascalCompiler.Intermediate.SymbolTable.Interface;

namespace NewPascalCompiler.FrontEnd.Pascal.Parsers
{
    public class ConstantDefinitionsParser : DeclarationsParser
    {
        public ConstantDefinitionsParser(PascalParser parser) : base(parser)
        {
        }

        public ISymbolTableEntry? Parse(Token token, ISymbolTableEntry? parentId)
        {
            return null;
        }
    }
}
