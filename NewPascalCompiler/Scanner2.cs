using ZoeyDesktopNavigationParser.ParserToken;
using ZoeyDesktopNavigationParser.Tokens;

namespace ZoeyDesktopNavigationParser;

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

public record ReservedWord(string TokenString, TokenCode Code);
public class Scanner2
{
    private readonly StreamBuffer _buffer;

    public static Dictionary<char, CharacterType> CharacterTypes = new Dictionary<char, CharacterType>();
    public static Dictionary<string, ReservedWord> ReservedWords = new Dictionary<string, ReservedWord>();

    static Scanner2()
    {
        InitializeCharacterTypes();
        InitializeReservedWords();
    }

    private static void InitializeReservedWords()
    {
        AddReservedWord("Do", TokenCode.Do);
        AddReservedWord("If", TokenCode.If);
        AddReservedWord("In", TokenCode.In);
        AddReservedWord("Of", TokenCode.Of);
        AddReservedWord("Or", TokenCode.Or);
        AddReservedWord("To", TokenCode.To);
        AddReservedWord("And", TokenCode.And);
        AddReservedWord("Div", TokenCode.Div);
        AddReservedWord("End", TokenCode.End);
        AddReservedWord("For", TokenCode.For);
        AddReservedWord("Mod", TokenCode.Mod);
        AddReservedWord("Nil", TokenCode.Nil);
        AddReservedWord("Not", TokenCode.Not);
        AddReservedWord("Set", TokenCode.Set);
        AddReservedWord("Var", TokenCode.Var);
        AddReservedWord("Case", TokenCode.Case);
        AddReservedWord("Else", TokenCode.Else);
        AddReservedWord("File", TokenCode.File);
        AddReservedWord("Goto", TokenCode.Goto);
        AddReservedWord("Then", TokenCode.Then);
        AddReservedWord("Type", TokenCode.Type);
        AddReservedWord("With", TokenCode.With);
        AddReservedWord("Array", TokenCode.Array);
        AddReservedWord("Begin", TokenCode.Begin);
        AddReservedWord("Const", TokenCode.Const);
        AddReservedWord("Label", TokenCode.Label);
        AddReservedWord("Until", TokenCode.Until);
        AddReservedWord("While", TokenCode.While);
        AddReservedWord("Downto", TokenCode.Downto);
        AddReservedWord("Packed", TokenCode.Packed);
        AddReservedWord("Record", TokenCode.Record);
        AddReservedWord("Repeat", TokenCode.Repeat);
        AddReservedWord("Program", TokenCode.Program);
        AddReservedWord("Function", TokenCode.Function);
        AddReservedWord("Procedure", TokenCode.Procedure);
        AddReservedWord("integer", TokenCode.Integer);
    }

    private static void AddReservedWord(string tokenString, TokenCode tokenCode)
    {
        ReservedWord rw = new ReservedWord(tokenString, tokenCode);
        ReservedWords.Add(tokenString.ToLower(), rw);
    }

    private static void InitializeCharacterTypes()
    {
        var i = 0;

        //--Initialize the character code map.
        for (i = 0; i < 127; i++) CharacterTypes[(char)i] = CharacterType.Error;
        for (i = 'a'; i <= 'z'; i++) CharacterTypes[(char)i] = CharacterType.Letter;
        for (i = 'A'; i <= 'Z'; i++) CharacterTypes[(char)i] = CharacterType.Letter;
        for (i = '0'; i <= '9'; i++) CharacterTypes[(char)i] = CharacterType.Digit;

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
        CharacterTypes['\x7f'] = CharacterType.EndOfFile;
    }

    private void SkipWhiteSpace()
    {
        if (_buffer.IsAtEnd()) return;

        do
        {
            if (CharacterTypes[_buffer.CurrentChar] == CharacterType.WhiteSpace)
            {
                _buffer.Advance();
                continue;
            }

            if (_buffer.CurrentChar != '{') continue;

            //--Skip over a comment, then fetch the next character.
            do
            {
                _buffer.Advance();
            } while ((_buffer.CurrentChar != '}') && (_buffer.CurrentChar == '\0'));

            if (_buffer.CurrentChar != '\0')
            {
                _buffer.Advance();
            }
        } while (CharacterTypes[_buffer.CurrentChar] == CharacterType.WhiteSpace || _buffer.CurrentChar == '{');
    }

    public Scanner2(string text)
    {
        _buffer = new StreamBuffer(text);
    }

    public Scanner2(StreamReader reader)
    {
        _buffer = new StreamBuffer(reader);
    }

    public Token GetToken()
    {
        Token token;
        SkipWhiteSpace();

        if (_buffer.IsAtEnd()) return new EOFToken();

        //--Determine the token class, based on the current character.
        switch (CharacterTypes[_buffer.CurrentChar])
        {
            case CharacterType.Letter: 
                token = new WordToken();
                break;

            case CharacterType.Digit:
                token = new NumberToken();
                break;

            case CharacterType.Quote:
                token = new StringToken();
                break;

            case CharacterType.Special:
                token = new SpecialToken();
                break;

            case CharacterType.EndOfFile:
                token = new EOFToken();
                break;

            default:
                token = new ErrorToken();
                break;
        }

        token.ReadToken(_buffer);
        return token;
    }

    public IEnumerable<Token> Scan()
    {
        Token token;
        do
        {
            token = GetToken();
            if (token.Code is TokenCode.EndOfFile or TokenCode.EndOfFile) continue;

            yield return token;

        } while (token.Code != TokenCode.EndOfFile && token.Code != TokenCode.Error);
    }
}