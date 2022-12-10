using System;

namespace NewPascalCompiler.Intermediate.Type
{
    public enum TypeKey
    {
        ENUMERATION_CONSTANTS,

        // Subrange
        SUBRANGE_BASE_TYPE, SUBRANGE_MIN_VALUE, SUBRANGE_MAX_VALUE,

        // Array
        ARRAY_INDEX_TYPE, ARRAY_ELEMENT_TYPE, ARRAY_ELEMENT_COUNT,

        // Record
        RECORD_SYMTAB
    }
}

