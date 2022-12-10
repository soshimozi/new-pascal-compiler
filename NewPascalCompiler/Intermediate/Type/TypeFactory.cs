using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewPascalCompiler.Intermediate.Type.Interface;

namespace NewPascalCompiler.Intermediate.Type
{
    public static class TypeFactory
    {
        public static ITypeSpec CreateType(TypeForm form)
        {
            return new TypeSpec(form);
        }

        public static ITypeSpec CreateStringType(string value)
        {
            return new TypeSpec(value);
        }
    }
}
