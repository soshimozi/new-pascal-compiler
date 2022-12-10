using NewPascalCompiler.FrontEnd.Pascal.Tokens;

namespace NewPascalCompiler.FrontEnd.Pascal;

public enum CharacterType
{
    Letter,
    Digit,
    Special,
    Quote,
    WhiteSpace,
    EndOfFile,
    Error
}

public record ReservedWord(string Text, TokenType TokenType);
public class PascalScanner : Scanner
{
    public static Dictionary<char, CharacterType> CharacterTypes = new Dictionary<char, CharacterType>();
    public static Dictionary<string, ReservedWord> ReservedWords = new Dictionary<string, ReservedWord>();
    public static Dictionary<string, ReservedWord> SpecialSymbols = new Dictionary<string, ReservedWord>();

    public PascalScanner(Source source) : base(source)
    {
        InitializeCharacterTypes();
        InitializeReservedWords();
        InitializeSpecialSymbols();
    }

    public static bool TryGetReservedWord(string word, out TokenType type)
    {
        type = TokenType.Error;

        var key = word.ToLower();
        if (!ReservedWords.ContainsKey(key)) return false;
        type = ReservedWords[key].TokenType;
        return true;

    }

    protected override Token ExtractToken()
    {
        SkipWhiteSpace();

        Token? token;
        var currentChar = CurrentChar();
        if (currentChar == Source.EOF) token = new EOFToken(Source);
        else
        if (char.IsLetter(currentChar)) token = new PascalWordToken(Source);
        else
        if (char.IsDigit(currentChar)) token = new PascalNumberToken(Source);
        else
        if (currentChar == '\'') token = new PascalStringToken(Source);
        else
        if (SpecialSymbols.ContainsKey(currentChar.ToString())) token = new PascalSpecialSymbolToken(Source);
        else
        {
            NextChar();
            token = new PascalErrorToken(Source, PascalErrorCode.InvalidCharacter, currentChar.ToString());
        }

        if (token is not PascalErrorToken && token is not EOFToken)
            token.Extract();

        return token;
    }

    private static void InitializeSpecialSymbols()
    {
        AddSpecialSymbol("+", TokenType.Plus);
        AddSpecialSymbol("-", TokenType.Minus);
        AddSpecialSymbol("*", TokenType.Star);
        AddSpecialSymbol("/", TokenType.Slash);
        AddSpecialSymbol(":=", TokenType.ColonEquals);
        AddSpecialSymbol(".", TokenType.Dot);
        AddSpecialSymbol(",", TokenType.Comma);
        AddSpecialSymbol(";", TokenType.SemiColon);
        AddSpecialSymbol(":", TokenType.Colon);
        AddSpecialSymbol("\"", TokenType.Quote);
        AddSpecialSymbol("=", TokenType.Equals);
        AddSpecialSymbol("<>", TokenType.NotEquals);
        AddSpecialSymbol("<", TokenType.LessThan);
        AddSpecialSymbol(">", TokenType.GreaterThan);
        AddSpecialSymbol("<=", TokenType.LessEquals);
        AddSpecialSymbol(">=", TokenType.GreaterEquals);
        AddSpecialSymbol("(", TokenType.LeftParen);
        AddSpecialSymbol(")", TokenType.RightParen);
        AddSpecialSymbol("{", TokenType.LeftBracket);
        AddSpecialSymbol("}", TokenType.RightBracket);
        AddSpecialSymbol("[", TokenType.LeftBrace);
        AddSpecialSymbol("]", TokenType.RightBrace);
        AddSpecialSymbol("^", TokenType.UpArrow);
        AddSpecialSymbol("..", TokenType.DotDot);
    }

    private static void InitializeReservedWords()
    {
        AddReservedWord("Do", TokenType.Do);
        AddReservedWord("If", TokenType.If);
        AddReservedWord("In", TokenType.In);
        AddReservedWord("Of", TokenType.Of);
        AddReservedWord("Or", TokenType.Or);
        AddReservedWord("To", TokenType.To);
        AddReservedWord("And", TokenType.And);
        AddReservedWord("Div", TokenType.Div);
        AddReservedWord("End", TokenType.End);
        AddReservedWord("For", TokenType.For);
        AddReservedWord("Mod", TokenType.Mod);
        AddReservedWord("Nil", TokenType.Nil);
        AddReservedWord("Not", TokenType.Not);
        AddReservedWord("Set", TokenType.Set);
        AddReservedWord("Var", TokenType.Var);
        AddReservedWord("Case", TokenType.Case);
        AddReservedWord("Else", TokenType.Else);
        AddReservedWord("File", TokenType.File);
        AddReservedWord("Goto", TokenType.Goto);
        AddReservedWord("Then", TokenType.Then);
        AddReservedWord("Type", TokenType.Type);
        AddReservedWord("With", TokenType.With);
        AddReservedWord("Array", TokenType.Array);
        AddReservedWord("Begin", TokenType.Begin);
        AddReservedWord("Const", TokenType.Const);
        AddReservedWord("Label", TokenType.Label);
        AddReservedWord("Until", TokenType.Until);
        AddReservedWord("While", TokenType.While);
        AddReservedWord("Downto", TokenType.Downto);
        AddReservedWord("Packed", TokenType.Packed);
        AddReservedWord("Record", TokenType.Record);
        AddReservedWord("Repeat", TokenType.Repeat);
        AddReservedWord("program", TokenType.Program);
        AddReservedWord("Function", TokenType.Function);
        AddReservedWord("Procedure", TokenType.Procedure);
        AddReservedWord("integer", TokenType.Integer);
    }

    private static void AddSpecialSymbol(string symbol, TokenType tokenCode)
    {
        var rw = new ReservedWord(symbol, tokenCode);
        SpecialSymbols.Add(symbol.ToLower(), rw);
    }

    private static void AddReservedWord(string tokenString, TokenType tokenCode)
    {
        ReservedWord rw = new ReservedWord(tokenString, tokenCode);
        ReservedWords.Add(tokenString.ToLower(), rw);
    }

    private static void InitializeCharacterTypes()
    {

        //--Initialize the character code map.
        for (var i = 0; i < 127; i++) CharacterTypes[(char)i] = CharacterType.Error;
        for (var i = 'a'; i <= 'z'; i++) CharacterTypes[i] = CharacterType.Letter;
        for (var i = 'A'; i <= 'Z'; i++) CharacterTypes[i] = CharacterType.Letter;
        for (var i = '0'; i <= '9'; i++) CharacterTypes[i] = CharacterType.Digit;

        CharacterTypes['?'] = CharacterType.Special;
        CharacterTypes['!'] = CharacterType.Special;
        CharacterTypes['+'] = CharacterTypes['-'] = CharacterType.Special;
        CharacterTypes['*'] = CharacterTypes['/'] = CharacterType.Special;
        CharacterTypes['='] = CharacterTypes['^'] = CharacterType.Special;
        CharacterTypes['.'] = CharacterTypes[','] = CharacterType.Special;
        CharacterTypes['<'] = CharacterTypes['>'] = CharacterType.Special;
        CharacterTypes['('] = CharacterTypes[')'] = CharacterType.Special;
        CharacterTypes['['] = CharacterTypes[']'] = CharacterType.Special;
        CharacterTypes['{'] = CharacterTypes['}'] = CharacterType.Special;
        CharacterTypes[':'] = CharacterTypes[';'] = CharacterType.Special;
        CharacterTypes[' '] = CharacterTypes['\t'] = CharacterType.WhiteSpace;
        CharacterTypes['\n'] = CharacterTypes['\r'] = CharacterType.WhiteSpace;
        CharacterTypes['\''] = CharacterType.Quote;
        CharacterTypes['\0'] = CharacterType.EndOfFile;
    }



    private void SkipWhiteSpace()
    {
        var currentChar = CurrentChar();
        while (char.IsWhiteSpace(currentChar) || currentChar == '{')
        {
            if (currentChar == '{')
            {
                do
                {
                    currentChar = NextChar();
                } while (currentChar != '}' && currentChar != Source.EOF);
            }
            else
            {
                currentChar = NextChar();
                ;
            }
        }
    }
}