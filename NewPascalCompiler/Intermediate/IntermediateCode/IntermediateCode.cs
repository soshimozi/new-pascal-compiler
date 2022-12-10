using NewPascalCompiler.Intermediate.IntermediateCode.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPascalCompiler.Intermediate.IntermediateCode;

public class IntermediateCode : IIntermediateCode
{
    public IIntermediateCodeNode? Root { get; set; }
}