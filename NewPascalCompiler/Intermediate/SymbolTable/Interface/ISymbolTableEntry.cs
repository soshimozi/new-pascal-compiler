using NewPascalCompiler.Intermediate.Type.Interface;
using System;

namespace NewPascalCompiler.Intermediate.SymbolTable.Interface
{
    public interface ISymbolTableEntry
    {
        string Name { get; }
        ISymbolTable SymbolTable { get; }
        Definition Definition { get; set; }
        ITypeSpec TypeSpec { get; set; }
        void AppendLineNumber(int lineNunber);
        List<int> GetLineNumbers();
        //void SetAttribute(SymbolTableKey key, object? value);
        //object? GetAttribute(SymbolTableKey key);
        object? this[SymbolTableKey key]
        {
            get;
            set;
        }
    }
}

