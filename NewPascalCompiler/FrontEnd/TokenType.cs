namespace NewPascalCompiler.FrontEnd;

public enum TokenType
{
    And, Array, Begin, Case, Const, Div, Do, Downto, Else, End,
    File, For, Function, Goto, If, In, Label, Mod, Nil, Not,
    Of, Or, Packed, Procedure, Program, Record, Repeat, Set,
    Then, To, Type, Until, Var, While, With,

    Plus, Minus, Star, Slash, ColonEquals,
    Dot, Comma, SemiColon, Colon, Quote,
    Equals, NotEquals, LessThan, LessEquals,
    GreaterEquals, GreaterThan, LeftParen, RightParen,
    LeftBracket, RightBracket, LeftBrace, RightBrace,
    UpArrow, DotDot,

    Identifier,
    Integer, Real, String,
    Error, EndOfFile
}