using System;

namespace NewPascalCompiler.Intermediate.IntermediateCode
{
    public enum IntermediateCodeType
    {
        // Program structure
        Program, Procedure, Function,

        // Statements
        Compound, Assign, Loop, Test, Call, Parameters,
        If, Select, SelectBranch, SelectConstants, NoOp,

        // Relational operators
        Eq, Ne, Lt, Le, Gt, Ge, Not,

        // Additive operators
        Add, Subtract, Or, Negate,

        // Multiplicative operators
        Multiply , IntegerDivide, FloatDivide, Mod, And,

        // Operands
        Variable, Subscripts, Field,
        IntegerConstant, RealConstant,
        StringConstant, BooleanConstant,

        // WRITE parameter
        WriteParam,
    }
}

