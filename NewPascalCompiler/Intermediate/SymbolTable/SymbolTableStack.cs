using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewPascalCompiler.Intermediate.SymbolTable.Interface;

namespace NewPascalCompiler.Intermediate.SymbolTable
{
    public class SymbolTableStack : List<ISymbolTable>, ISymbolTableStack
    {
        public SymbolTableStack()
        {
            CurrentNestingLevel = 0;
            Add(SymbolTableFactory.CreateSymTab(CurrentNestingLevel));
        }

        public ISymbolTableEntry? ProgramId { get; set; }
        public int CurrentNestingLevel { get; private set; }

        public ISymbolTable LocalSymbolTable => this[CurrentNestingLevel];

        public ISymbolTable Push()
        {
            var symTab = SymbolTableFactory.CreateSymTab(++CurrentNestingLevel);
            Add(symTab);

            return symTab;
        }

        public ISymbolTable Push(ISymbolTable symbolTable)
        {
            var symTab = SymbolTableFactory.CreateSymTab(++CurrentNestingLevel);
            Add(symTab);

            return symTab;
        }

        public ISymbolTable Pop()
        {
            var symTab = this[CurrentNestingLevel];
            RemoveAt(CurrentNestingLevel--);

            return symTab;
        }

        public ISymbolTableEntry? EnterLocal(string name)
        {
            return this[CurrentNestingLevel].Enter(name);
        }

        public ISymbolTableEntry? LookupLocal(string name)
        {
            return this[CurrentNestingLevel].Lookup(name);
        }

        public ISymbolTableEntry? Lookup(string name)
        {
            ISymbolTableEntry? foundEntry = null;

            // Search the current and enclosing scopes.
            for (var i = CurrentNestingLevel; (i >= 0) && (foundEntry == null); --i)
            {
                foundEntry = this[i].Lookup(name);
            }

            return foundEntry;
        }
    }
}
