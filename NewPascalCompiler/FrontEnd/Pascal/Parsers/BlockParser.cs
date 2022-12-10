using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewPascalCompiler.Intermediate.IntermediateCode.Interface;
using NewPascalCompiler.Intermediate.SymbolTable.Interface;

namespace NewPascalCompiler.FrontEnd.Pascal.Parsers
{
    public class BlockParser : DeclarationsParser
    {
        public BlockParser(PascalParser parent) : base(parent)
        {
        }

        public new IIntermediateCodeNode Parse(ISymbolTableEntry parentId, out Token token)
        {
            throw new NotImplementedException();
        }
    }
}
