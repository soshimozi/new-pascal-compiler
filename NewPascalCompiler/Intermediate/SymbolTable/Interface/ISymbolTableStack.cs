using System;

namespace NewPascalCompiler.Intermediate.SymbolTable.Interface
{
    public interface ISymbolTableStack
    {
        ISymbolTableEntry ProgramId
        {
            get;
            set;
        }

        int CurrentNestingLevel
        {
            get;
        }

        ISymbolTable LocalSymbolTable { get; }
        ISymbolTable Push(ISymbolTable symbolTable);
        ISymbolTable Pop();

        ISymbolTableEntry EnterLocal(string name);
        ISymbolTableEntry LookupLocal(string name);
        ISymbolTableEntry Lookup(string name);
    }
}

