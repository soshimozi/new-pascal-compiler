using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using NewPascalCompiler.Intermediate;
using NewPascalCompiler.Intermediate.SymbolTable;
using NewPascalCompiler.Intermediate.SymbolTable.Interface;

namespace NewPascalCompiler.FrontEnd.Pascal.Parsers
{
    internal class DeclaredRoutineParser : DeclarationsParser
    {
        private static int dummyCounter = 0;  // counter for dummy routine names

        public DeclaredRoutineParser(PascalParser parent) : base(parent)
        {
        }

        public ISymbolTableEntry? Parse(ISymbolTableEntry? parentId, ref Token token)
        {
            Definition? routineDefn = null;
            string? dummyName = null;
            ISymbolTableEntry? routineId = null;
            var routineType = token.TokenType;

            // Initialize.
            switch (routineType)
            {

                case TokenType.Program:
                    {
                        token = GetNextToken();  // consume PROGRAM
                        routineDefn = Definition.Program;
                        dummyName = "DummyProgramName".ToLower();
                        break;
                    }

                case TokenType.Procedure:
                    {
                        token = GetNextToken();  // consume PROCEDURE
                        routineDefn = Definition.Procedure;
                        dummyName = "DummyProcedureName_".ToLower() +
                                    string.Format("%03d", ++dummyCounter);
                        break;
                    }

                case TokenType.Function:
                    {
                        token = GetNextToken();  // consume FUNCTION
                        routineDefn = Definition.Function;
                        dummyName = "DummyFunctionName_".ToLower() +
                                    string.Format("%03d", ++dummyCounter);
                        break;
                    }

                default:
                    {
                        routineDefn = Definition.Program;
                        dummyName = "DummyProgramName".ToLower();
                        break;
                    }
            }

            // Parse the routine name.
            routineId = ParseRoutineName(token, dummyName);

            token = CurrentToken;
            if (routineId == null) return null;

            routineId.Definition = routineDefn;

            // Create new intermediate code for the routine.
            var iCode = IntermediateCodeFactory.CreateICode();
            routineId[SymbolTableKey.ROUTINE_ICODE] = iCode;

            routineId[SymbolTableKey.ROUTINE_ROUTINES] = new List<ISymbolTableEntry>();

            var routineCode = (RoutineCode?)routineId[SymbolTableKey.ROUTINE_CODE];

            // Push the routine's new symbol table onto the stack.
            // If it was forwarded, push its existing symbol table.
            if (routineCode is RoutineCode.Forward)
            {
                if (routineId[SymbolTableKey.ROUTINE_SYMTAB] is ISymbolTable symTab)
                    SymbolTableStack.Push(symTab);
            }
            else
            {
                routineId[SymbolTableKey.ROUTINE_SYMTAB] = SymbolTableStack.Push();
            }

            var routineCodeRoutineId = routineId[SymbolTableKey.ROUTINE_CODE];

            // Program: Set the program identifier in the symbol table stack.
            // Set the initial local variables array slot number to 1.
            if (routineDefn == Definition.Program)
            {
                SymbolTableStack.ProgramId = routineId;
                SymbolTableStack.LocalSymbolTable.GetNextSlotNumber();  // bump slot number
            }

            // Non-forwarded procedure or function: Append to the parent's list
            //                                      of routines.
            else if (routineCodeRoutineId != null && (RoutineCode)routineCodeRoutineId != RoutineCode.Forward)
            {
                if (parentId?[SymbolTableKey.ROUTINE_ROUTINES] == null) return null;
                var subroutines = parentId[SymbolTableKey.ROUTINE_ROUTINES] as List<ISymbolTableEntry>;
                subroutines?.Add(routineId);
            }

            // If the routine was forwarded, there should not be
            // any formal parameters or a function return type.
            // But parse them anyway if they're there.
            if (routineCodeRoutineId != null && (RoutineCode)routineCodeRoutineId == RoutineCode.Forward)
            {
                if (token.TokenType != TokenType.SemiColon)
                {
                    HandleSyntaxError(token, PascalErrorCode.AlreadyForwarded);
                    ParseHeader(ref token, routineId);
                }
            }

            // Parse the routine's formal parameters and function return type.
            else
            {
                ParseHeader(ref token, routineId);
            }

            // Look for the semicolon.
            token = CurrentToken;
            if (token.TokenType == TokenType.SemiColon)
            {
                do
                {
                    token = GetNextToken();  // consume ;
                } while (token.TokenType == TokenType.SemiColon);
            }
            else
            {
                HandleSyntaxError(token, PascalErrorCode.MissingSemicolon);
            }

            // Parse the routine's block or forward declaration.
            if ((token.TokenType == TokenType.Identifier) &&
                string.Compare(token.GetText(), "forward", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                token = GetNextToken();  // consume forward
                routineId[SymbolTableKey.ROUTINE_CODE] = RoutineCode.Forward;
            }
            else
            {
                routineId[SymbolTableKey.ROUTINE_CODE] = RoutineCode.Declared;

                var blockParser = new BlockParser(this);
                var rootNode = blockParser.Parse(routineId, out token);
                iCode.Root = rootNode;
            }

            // Pop the routine's symbol table off the stack.
            SymbolTableStack.Pop();

            return routineId;
        }

        protected ISymbolTableEntry? ParseRoutineName(Token token, string? dummyName)
        {
            ISymbolTableEntry? routineId = null;

            // Parse the routine name identifier
            if (token.TokenType == TokenType.Identifier)
            {
                string tokenText = token.GetText() ?? string.Empty;
                if (string.IsNullOrEmpty(tokenText))
                {
                    HandleSyntaxError(token, PascalErrorCode.MissingIdentifier);
                    return null;
                }

                routineId = SymbolTableStack.LookupLocal(tokenText);

                if (routineId == null)
                {
                    routineId = SymbolTableStack.EnterLocal(tokenText);
                }
                else
                {
                    var routineIdObject = routineId[SymbolTableKey.ROUTINE_CODE];
                    if (routineIdObject != null)
                    {
                        var routineCode = (RoutineCode)routineIdObject;

                        if (routineCode != RoutineCode.Forward)
                        {
                            HandleSyntaxError(token, PascalErrorCode.IdentifierRedefined);
                            routineId = null;
                        }
                    }
                }

                token = GetNextToken();

            }
            else
            {
                HandleSyntaxError(token, PascalErrorCode.MissingIdentifier);
            }

            // if necessary, create a dummy routine name symbol table entry
            if (routineId == null)
            {
                routineId = SymbolTableStack.EnterLocal(dummyName ?? string.Empty);
            }
            return routineId;
        }

        private void ParseHeader(ref Token token, ISymbolTableEntry routineId)
        {
            // Parse the routine's formal parameters.
            ParseFormalParameters(ref token, routineId);
            token = CurrentToken;

            if(routineId.Definition == Definition.Function)
            {
                var variableDelcarationsParser = new VariableDeclarationsParser(this);

                variableDelcarationsParser.Definitition = Definition.Function;
            }
            /*
             *         // Parse the routine's formal parameters.
        parseFormalParameters(token, routineId);
        token = currentToken();

        // If this is a function, parse and set its return type.
        if (routineId.getDefinition() == DefinitionImpl.FUNCTION) {
            VariableDeclarationsParser variableDeclarationsParser =
                new VariableDeclarationsParser(this);
            variableDeclarationsParser.setDefinition(DefinitionImpl.FUNCTION);
            TypeSpec type = variableDeclarationsParser.parseTypeSpec(token);

            token = currentToken();

            // The return type cannot be an array or record.
            if (type != null) {
                TypeForm form = type.getForm();
                if ((form == TypeFormImpl.ARRAY) ||
                    (form == TypeFormImpl.RECORD))
                {
                    errorHandler.flag(token, INVALID_TYPE, this);
                }
            }

            // Missing return type.
            else {
                type = Predefined.undefinedType;
            }

            routineId.setTypeSpec(type);
            token = currentToken();
        }
             * 
             */
        }



        private static readonly HashSet<TokenType> ParameterSet = new()
        {
            TokenType.Const,
            TokenType.Type,
            TokenType.Var,
            TokenType.Procedure,
            TokenType.Function,
            TokenType.Begin,
            TokenType.Var,
            TokenType.Identifier,
            TokenType.RightParen
        };

        // Synchronization set for the opening left parenthesis.
        private static readonly HashSet<TokenType> LeftParenSet = new()
        {
            TokenType.Const,
            TokenType.Type,
            TokenType.Var,
            TokenType.Procedure,
            TokenType.Function,
            TokenType.Begin,
            TokenType.LeftParen,
            TokenType.SemiColon,
            TokenType.Colon
        };


        // Synchronization set for the closing right parenthesis.
        private static readonly HashSet<TokenType> RightParenSet = new()
        {
            TokenType.Const,
            TokenType.Type,
            TokenType.Var,
            TokenType.Procedure,
            TokenType.Function,
            TokenType.Begin,
            TokenType.RightParen,
            TokenType.SemiColon,
            TokenType.Colon
        };

        private void ParseFormalParameters(ref Token token, ISymbolTableEntry routineId)
        {
            throw new NotImplementedException();
        }

        // Synchronization set to follow a formal parameter identifier.
        private static readonly HashSet<TokenType> ParameterFollowSet = new()
        {
            TokenType.Colon,
            TokenType.RightParen,
            TokenType.SemiColon,
            TokenType.Const,
            TokenType.Type,
            TokenType.Var,
            TokenType.Procedure,
            TokenType.Function,
            TokenType.Begin
        };

        // Synchronization set for the , token.
        private static readonly HashSet<TokenType> CommaSet = new()
        {
            TokenType.Comma,
            TokenType.Colon,
            TokenType.Identifier,
            TokenType.RightParen,
            TokenType.SemiColon,
            TokenType.Const,
            TokenType.Type,
            TokenType.Var,
            TokenType.Procedure,
            TokenType.Function,
            TokenType.Begin
        };

        private List<ISymbolTableEntry> ParseParmSublist(ref Token token,
                                                        ISymbolTableEntry routineId)
        {
            return new List<ISymbolTableEntry>();
        }

    }
}
