using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewPascalCompiler.Intermediate.SymbolTable.Interface;
using NewPascalCompiler.Intermediate.Type.Interface;

namespace NewPascalCompiler.Intermediate.SymbolTable
{
    public class SymbolTableEntry : Dictionary<SymbolTableKey, object?>, ISymbolTableEntry
    {
        private readonly List<int> _lineNumbers = new ();
        public SymbolTableEntry(string name, ISymbolTable symbolTable)
        {
            Name = name;
            SymbolTable = symbolTable;
        }

        public string Name { get; }
        public ISymbolTable SymbolTable { get; }
        public Definition? Definition { get; set; }
        public ITypeSpec? TypeSpec { get; set; }
        public void AppendLineNumber(int lineNumber)
        {
            _lineNumbers.Add(lineNumber);
        }

        public List<int> GetLineNumbers()
        {
            return _lineNumbers.ToList();
        }
    }
}
