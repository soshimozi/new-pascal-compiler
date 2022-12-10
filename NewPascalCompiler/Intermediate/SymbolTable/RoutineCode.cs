using System;

namespace NewPascalCompiler.Intermediate.SymbolTable
{
    public enum RoutineCode
    {
        Declared, Forward,
        Read, ReadLn, Write, WriteLn,
        Abs, Arctan, Chr, Cos, Eof, Eoln, Exp, Ln, Odd, Ord,
        Pred, Round, Sin, Sqr, Sqrt, Succ, Trunc,
    }
}

