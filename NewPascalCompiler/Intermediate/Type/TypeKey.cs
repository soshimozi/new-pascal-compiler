using System;

namespace NewPascalCompiler.Intermediate.Type
{
    public enum TypeKey
    {
        EnumerationConstants,

        // Subrange
        SubrangeBaseType, SubrangeMinValue, SubrangeMaxValue,

        // Array
        ArrayIndexType, ArrayElementType, ArrayElementCount,

        // Record
        RecordSymbolTable
    }
}

