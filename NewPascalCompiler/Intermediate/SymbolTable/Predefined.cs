using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewPascalCompiler.Intermediate.SymbolTable.Interface;
using NewPascalCompiler.Intermediate.Type;
using NewPascalCompiler.Intermediate.Type.Interface;

namespace NewPascalCompiler.Intermediate.SymbolTable
{
    public static class Predefined
    {
        public static ITypeSpec? IntegerType;
        public static ITypeSpec? BooleanType;
        public static ITypeSpec? CharType;
        public static ITypeSpec? RealType;
        public static ITypeSpec? UndefinedType;

        public static ISymbolTableEntry? IntegerId;
        public static ISymbolTableEntry? RealId;
        public static ISymbolTableEntry? BooleanId;
        public static ISymbolTableEntry? CharId;
        public static ISymbolTableEntry? FalseId;
        public static ISymbolTableEntry? TrueId;
        public static ISymbolTableEntry? ReadId;
        public static ISymbolTableEntry? ReadlnId;
        public static ISymbolTableEntry? WriteId;
        public static ISymbolTableEntry? WritelnId;
        public static ISymbolTableEntry? AbsId;
        public static ISymbolTableEntry? ArctanId;
        public static ISymbolTableEntry? ChrId;
        public static ISymbolTableEntry? CosId;
        public static ISymbolTableEntry? EofId;
        public static ISymbolTableEntry? EolnId;
        public static ISymbolTableEntry? ExpId;
        public static ISymbolTableEntry? LnId;
        public static ISymbolTableEntry? OddId;
        public static ISymbolTableEntry? OrdId;
        public static ISymbolTableEntry? PredId;
        public static ISymbolTableEntry? RoundId;
        public static ISymbolTableEntry? SinId;
        public static ISymbolTableEntry? SqrId;
        public static ISymbolTableEntry? SqrtId;
        public static ISymbolTableEntry? SuccId;
        public static ISymbolTableEntry? TruncId;

        public static void Initialize(ISymbolTableStack symTabStack)
        {
            InitializeTypes(symTabStack);
            InitializeConstants(symTabStack);
            InitializeStandardRoutines(symTabStack);
        }

        private static void InitializeStandardRoutines(ISymbolTableStack symTabStack)
        {
            ReadId = EnterStandard(symTabStack, Definition.Procedure, "read", RoutineCode.Read);
            ReadlnId = EnterStandard(symTabStack, Definition.Procedure, "readln", RoutineCode.ReadLn);
            WriteId = EnterStandard(symTabStack, Definition.Procedure, "write", RoutineCode.Write);
            WritelnId = EnterStandard(symTabStack, Definition.Procedure, "writeln", RoutineCode.WriteLn);

            AbsId = EnterStandard(symTabStack, Definition.Function, "abs", RoutineCode.Abs);
            ArctanId = EnterStandard(symTabStack, Definition.Function, "arctan", RoutineCode.Arctan);
            ChrId = EnterStandard(symTabStack, Definition.Function, "chr", RoutineCode.Chr);
            CosId = EnterStandard(symTabStack, Definition.Function, "cos", RoutineCode.Cos);
            EofId = EnterStandard(symTabStack, Definition.Function, "eof", RoutineCode.Eof);
            EolnId = EnterStandard(symTabStack, Definition.Function, "eoln", RoutineCode.Eoln);
            ExpId = EnterStandard(symTabStack, Definition.Function, "exp", RoutineCode.Exp);
            LnId = EnterStandard(symTabStack, Definition.Function, "ln", RoutineCode.Ln);
            OddId = EnterStandard(symTabStack, Definition.Function, "odd", RoutineCode.Odd);
            OrdId = EnterStandard(symTabStack, Definition.Function, "ord", RoutineCode.Ord);
            PredId = EnterStandard(symTabStack, Definition.Function, "pred", RoutineCode.Pred);
            RoundId = EnterStandard(symTabStack, Definition.Function, "round", RoutineCode.Round);
            SinId = EnterStandard(symTabStack, Definition.Function, "sin", RoutineCode.Sin);
            SqrId = EnterStandard(symTabStack, Definition.Function, "sqr", RoutineCode.Sqr);
            SqrtId = EnterStandard(symTabStack, Definition.Function, "sqrt", RoutineCode.Sqrt);
            SuccId = EnterStandard(symTabStack, Definition.Function, "succ", RoutineCode.Succ);
            TruncId = EnterStandard(symTabStack, Definition.Function, "trunc", RoutineCode.Trunc);
        }

        private static void InitializeConstants(ISymbolTableStack symTabStack)
        {
            // Boolean enumeration constant false.
            FalseId = symTabStack.EnterLocal("false");
            FalseId.Definition = Definition.EnumerationConstant;
            FalseId.TypeSpec = BooleanType!;
            FalseId[SymbolTableKey.CONSTANT_VALUE] = 0;

            // Boolean enumeration constant true.
            TrueId = symTabStack.EnterLocal("true");
            TrueId.Definition = Definition.EnumerationConstant;
            TrueId.TypeSpec = BooleanType!;
            TrueId[SymbolTableKey.CONSTANT_VALUE] = 1;

            // Add false and true to the boolean enumeration type.
            var constants = new List<ISymbolTableEntry>
            {
                FalseId,
                TrueId
            };
            BooleanType![TypeKey.EnumerationConstants] = constants;
        }

        private static void InitializeTypes(ISymbolTableStack symTabStack)
        {
            // Type integer.
            IntegerId = symTabStack.EnterLocal("integer");

            IntegerType = TypeFactory.CreateType(TypeForm.Scalar);
            IntegerType.Identifier = IntegerId;
            IntegerId.Definition = Definition.Type;
            IntegerId.TypeSpec = IntegerType;

            // Type real.
            RealId = symTabStack.EnterLocal("real");
            RealType = TypeFactory.CreateType(TypeForm.Scalar);
            RealType.Identifier = RealId;
            RealId.Definition = Definition.Type;
            RealId.TypeSpec = RealType;

            // Type boolean.
            BooleanId = symTabStack.EnterLocal("boolean");
            BooleanType = TypeFactory.CreateType(TypeForm.Enumeration);
            BooleanType.Identifier = BooleanId;
            BooleanId.Definition = Definition.Type;
            BooleanId.TypeSpec = BooleanType;

            // Type char.
            CharId = symTabStack.EnterLocal("char");
            CharType = TypeFactory.CreateType(TypeForm.Scalar);
            CharType.Identifier = CharId;
            CharId.Definition = Definition.Type;
            CharId.TypeSpec = CharType;

            // Undefined type.
            UndefinedType = TypeFactory.CreateType(TypeForm.Scalar);
        }

        private static ISymbolTableEntry EnterStandard(ISymbolTableStack symTabStack,
            Definition defn, string name,
            RoutineCode routineCode)
        {
            var procId = symTabStack.EnterLocal(name);
            procId.Definition = defn;
            procId[SymbolTableKey.ROUTINE_CODE] = routineCode;

            return procId;
        }
    }

}
