using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPascalCompiler.Intermediate.IntermediateCode.Interface;

public interface IIntermediateCode
{
    IntermediateCodeType CodeType { get; }
    IIntermediateCode Parent { get; }

}
