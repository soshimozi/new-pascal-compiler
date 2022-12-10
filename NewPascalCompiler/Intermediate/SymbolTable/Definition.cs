using System;

namespace NewPascalCompiler.Intermediate.SymbolTable
{
    public class Definition
    {
        public static readonly Definition Constant = new Definition("constant");
        public static readonly Definition EnumerationConstant = new Definition("enumeration constant");
        public static readonly Definition Type = new("type");
        public static readonly Definition Variable = new("variable");
        public static readonly Definition ValueParam = new("value parameter");
        public static readonly Definition VarParam = new("VAR parameter");
        public static readonly Definition ProgramParam = new("program parameter");
        public static readonly Definition Program = new("program");
        public static readonly Definition Procedure = new("procedure");
        public static readonly Definition Function = new("function");
        public static readonly Definition Undefined = new("undefined");

        private readonly string _text;
        private Definition(string text)
        {
            _text = text;
        }

        public string GetText()
        {
            return _text;
        }
    }
}

