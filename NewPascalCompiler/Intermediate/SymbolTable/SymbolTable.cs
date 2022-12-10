using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewPascalCompiler.Intermediate.SymbolTable.Interface;

namespace NewPascalCompiler.Intermediate.SymbolTable
{
    public class SymbolTable : SortedDictionary<string, ISymbolTableEntry?>, ISymbolTable
    {

        private int _slotNumber;         // local variables array slot number

        public SymbolTable(int nestingLevel)
        {
            NestingLevel = nestingLevel;
            _slotNumber = -1;
            MaxSlotNumber = 0;
        }

        public int NestingLevel { get; }

        public ISymbolTableEntry Enter(string name)
        {
            var entry = SymbolTableFactory.CreateSymTabEntry(name, this);
            this[name] = entry;
            return entry;
        }

        public ISymbolTableEntry? Lookup(string name)
        {
            TryGetValue(name, out var value);
            return value;
        }

        public List<ISymbolTableEntry> SortedEntries
        {
            get
            {
                var entries = Values;
                var list = new List<ISymbolTableEntry>();

                // Iterate over the sorted entries and append them to the list.
                foreach(var entry in entries)
                {
                    if (entry is { } tableEntry)
                    {
                        list.Add(tableEntry);
                    }
                }

                return list;  // sorted list of entries
            }
        }

        public int GetNextSlotNumber()
        {
            MaxSlotNumber = ++_slotNumber;
            return _slotNumber;
        }

        public int MaxSlotNumber { get; private set; }
    }
}
