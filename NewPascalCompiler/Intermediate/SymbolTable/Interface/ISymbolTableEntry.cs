using NewPascalCompiler.Intermediate.Type.Interface;
using System;

namespace NewPascalCompiler.Intermediate.SymbolTable.Interface
{
    public interface ISymbolTableEntry
    {
        string Name { get; }
        ISymbolTable SymbolTable { get; }
        Definition? Definition { get; set; }
        ITypeSpec? TypeSpec { get; set; }
        void AppendLineNumber(int lineNumber);
        List<int> GetLineNumbers();
        object? this[SymbolTableKey key]
        {
            get;
            set;
        }
    }
}

