using NewPascalCompiler.Intermediate.SymbolTable.Interface;
using System;

namespace NewPascalCompiler.Intermediate.Type.Interface
{
    public interface ITypeSpec
    {
        TypeForm? Form { get; }
        ISymbolTableEntry? Identifier { get; set; }
        object? this[TypeKey key]
        {
            get;
            set;
        }

        //void SetAttribute(TypeKey key, object? value)
        //{ }

        //object? GetAttribute(TypeKey key);

        bool IsPascalString { get; }

        ITypeSpec? BaseType { get; }

    }
}

