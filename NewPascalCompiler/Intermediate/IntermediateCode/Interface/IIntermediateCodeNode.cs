using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace NewPascalCompiler.Intermediate.IntermediateCode.Interface
{
    public interface IIntermediateCodeNode
    {
        IntermediateCodeType CodeType { get; }
        IIntermediateCode? Parent { get; }

        TypeSpecification? TypeSpecification { get; set; }
        IIntermediateCodeNode AddChild(IIntermediateCodeNode node);
        List<IIntermediateCodeNode> Children { get; }
        object? this[IntermediateCodeKey key] { get; set; }

        IIntermediateCodeNode Copy();
    }
}
