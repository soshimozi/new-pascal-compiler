using System;

namespace NewPascalCompiler.Intermediate.SymbolTable.Interface
{
    public interface ISymbolTable
    {
        int NestingLevel { get; }

        ISymbolTableEntry Enter(string name);
        ISymbolTableEntry Lookup(string name);
        List<int> SortedEntries { get; }
        int NextSlotNumber { get; }
        int MaxSlotNumber { get; }
    }
}

