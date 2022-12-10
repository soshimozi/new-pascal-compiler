using System;

namespace NewPascalCompiler.Intermediate.SymbolTable
{
    public enum SymbolTableKey
    {
        CONSTANT_VALUE,

        // Procedure or function.
        ROUTINE_CODE, ROUTINE_SYMTAB, ROUTINE_ICODE,
        ROUTINE_PARMS, ROUTINE_ROUTINES,

        // Variable or record field value.
        DATA_VALUE,

        // Local variables array slot numbers.
        SLOT, WRAP_SLOT
    }

}

