using NewPascalCompiler.Intermediate.Type.Interface;
using System;
using NewPascalCompiler.Intermediate.SymbolTable;

namespace NewPascalCompiler.Intermediate.Type
{
    public static class TypeChecker
    {
        public static bool IsInteger(ITypeSpec? type)
        {
            return (type != null) && (type.BaseType == Predefined.IntegerType);
        }

        public static bool AreBothInteger(ITypeSpec? type1, ITypeSpec? type2)
        {
            return IsInteger(type1) && IsInteger(type2);
        }

        public static bool IsReal(ITypeSpec? type)
        {
            return (type != null) && (type.BaseType == Predefined.RealType);
        }

        public static bool IsIntegerOrReal(ITypeSpec? type)
        {
            return IsInteger(type) || IsReal(type);
        }

        public static bool IsAtLeastOneReal(ITypeSpec? type1, ITypeSpec? type2)
        {
            return (IsReal(type1) && IsReal(type2)) ||
                   (IsReal(type1) && IsInteger(type2)) ||
                   (IsInteger(type1) && IsReal(type2));
        }

        public static bool IsBoolean(ITypeSpec? type)
        {
            return (type != null) && (type.BaseType == Predefined.BooleanType);
        }

        public static bool AreBothBoolean(ITypeSpec? type1, TypeSpec type2)
        {
            return IsBoolean(type1) && IsBoolean(type2);
        }

        public static bool IsChar(ITypeSpec? type)
        {
            return (type != null) && (type.BaseType == Predefined.CharType);
        }

        public static bool AreAssignmentCompatible(ITypeSpec? targetType,
            ITypeSpec? valueType)
        {
            if ((targetType == null) || (valueType == null))
            {
                return false;
            }

            targetType = targetType.BaseType;
            valueType = valueType.BaseType;

            var compatible = false;

            // Identical types.
            if (targetType == valueType)
            {
                compatible = true;
            }

            // real := integer
            else if (IsReal(targetType) && IsInteger(valueType))
            {
                compatible = true;
            }

            // string := string
            else
            {
                compatible =
                    targetType?.IsPascalString == true && valueType?.IsPascalString == true;
            }

            return compatible;
        }

        public static bool AreComparisonCompatible(ITypeSpec? type1,
            ITypeSpec? type2)
        {
            if ((type1 == null) || (type2 == null))
            {
                return false;
            }

            type1 = type1.BaseType;
            type2 = type2.BaseType;
            var form = type1?.Form;

            var compatible = false;

            // Two identical scalar or enumeration types.
            if ((type1 == type2) && ((form == TypeForm.Scalar) || (form == TypeForm.Scalar)))
            {
                compatible = true;
            }

            // One integer and one real.
            else if (IsAtLeastOneReal(type1, type2))
            {
                compatible = true;
            }

            // Two strings.
            else
            {
                compatible = type1?.IsPascalString == true && type2?.IsPascalString == true;
            }

            return compatible;
        }

    }
}

