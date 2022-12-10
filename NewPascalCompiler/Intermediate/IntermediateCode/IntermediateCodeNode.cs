using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using NewPascalCompiler.Intermediate.IntermediateCode.Interface;
using NewPascalCompiler.Intermediate.SymbolTable.Interface;

namespace NewPascalCompiler.Intermediate.IntermediateCode
{
    public class IntermediateCodeNode : Dictionary<IntermediateCodeKey, object?>, IIntermediateCodeNode
    {
        public IntermediateCodeNode(IntermediateCodeType type)
        {
            CodeType = type;
            Parent = null;
            Children = new List<IIntermediateCodeNode>();
        }

        public IntermediateCodeType CodeType { get; }
        public IIntermediateCode? Parent { get; private set; }
        public TypeSpecification? TypeSpecification { get; set; }
        public IIntermediateCodeNode AddChild(IIntermediateCodeNode node)
        {
            Children.Add(node);
            return node;
        }

        public List<IIntermediateCodeNode> Children { get; }

        public IIntermediateCodeNode Copy()
        {
            // Create a copy with the same type and type specification.
            var copy =
                IntermediateCodeFactory.CreateICodeNode(CodeType);
            copy.TypeSpecification = TypeSpecification;

            foreach (var entry in this)
            {
                copy[entry.Key] = entry.Value;
            }

            return copy;
        }


        public override string ToString()
        {
            return CodeType.ToString();
        }
    }
}
