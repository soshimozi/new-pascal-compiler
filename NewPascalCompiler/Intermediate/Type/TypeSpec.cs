using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewPascalCompiler.Intermediate.SymbolTable;
using NewPascalCompiler.Intermediate.SymbolTable.Interface;
using NewPascalCompiler.Intermediate.Type.Interface;

namespace NewPascalCompiler.Intermediate.Type
{
    public class TypeSpec : Dictionary<TypeKey, object?>, ITypeSpec
    {
        public TypeForm? Form { get; }

        public ISymbolTableEntry? Identifier { get; set; }

        public bool IsPascalString
        {
            get
            {
                if (Form == TypeForm.Array)
                {
                    var elmtType = this[TypeKey.ArrayElementType] as TypeSpec;
                    var indexType = this[TypeKey.ArrayIndexType] as TypeSpec;

                    return (elmtType?.BaseType == Predefined.IntegerType) &&
                           (indexType?.BaseType == Predefined.IntegerType);
                }
                else
                {
                    return false;
                }
            }
        }

        public ITypeSpec? BaseType =>
            Form == TypeForm.SubRange ? this[TypeKey.SubrangeBaseType] as TypeSpec
                : this;

        public TypeSpec(TypeForm form)
        {
            Form = form;
            Identifier = null;
        }

        public TypeSpec(string value)
        {
            Form = TypeForm.Array;

            var indexType = new TypeSpec(TypeForm.SubRange)
            {
                [TypeKey.SubrangeBaseType] = Predefined.IntegerType,
                [TypeKey.SubrangeMinValue] = 1,
                [TypeKey.SubrangeMaxValue] = value.Length
            };

            this[TypeKey.ArrayIndexType] = indexType;
            this[TypeKey.ArrayElementType] = Predefined.CharType;
            this[TypeKey.ArrayElementCount] = value.Length;
        }
    }
}
