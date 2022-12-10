using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewPascalCompiler.Intermediate.SymbolTable.Interface;

namespace NewPascalCompiler.Intermediate.SymbolTable
{
    public static class SymbolTableFactory
    {
        public static ISymbolTableStack CreateSymTabStack()
        {
            return new SymbolTableStack();
        }

        /**
         * Create and return a symbol table implementation.
         * @param nestingLevel the nesting level.
         * @return the symbol table implementation.
         */
        public static ISymbolTable CreateSymTab(int nestingLevel)
        {
            return new SymbolTable(nestingLevel);
        }

        /**
         * Create and return a symbol table entry implementation.
         * @param name the identifier name.
         * @param symTab the symbol table that contains this entry.
         * @return the symbol table entry implementation.
         */
        public static ISymbolTableEntry CreateSymTabEntry(string name, ISymbolTable symTab)
        {
            return new SymbolTableEntry(name, symTab);
        }
    }
}
