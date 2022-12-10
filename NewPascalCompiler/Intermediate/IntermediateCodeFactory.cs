using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewPascalCompiler.Intermediate.IntermediateCode;
using NewPascalCompiler.Intermediate.IntermediateCode.Interface;

namespace NewPascalCompiler.Intermediate
{
    public static class IntermediateCodeFactory
    {
        public static IIntermediateCode CreateICode()
        {
            return new IntermediateCode.IntermediateCode();
        }

        /**
         * Create and return a node implementation.
         * @param type the node type.
         * @return the node implementation.
         */
        public static IIntermediateCodeNode CreateICodeNode(IntermediateCodeType type)
        {
            return new IntermediateCodeNode(type);
        }
    }
}
