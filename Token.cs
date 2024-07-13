namespace TesLang_Compiler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

public enum TokenType
{
    FN, ID, LPAREN, RPAREN, AS, VECTOR, INT, LESS_THAN, GREATER_THAN, LCURLYBR, RCURLYBR,
    DBL_COLON, EQ, NUMBER, SEMI_COLON, FOR, TO, LEN, BEGIN, END, RETURN, PLUS, LSQUAREBR,
    RSQUAREBR, COMMENT, UNKNOWN, COMMA
}

public class Token
{
    public TokenType Type { get; }
    public string Value { get; }
    public int Line { get; }
    public int Column { get; }

    public Token(TokenType type, string value, int line, int column)
    {
        Type = type;
        Value = value;
        Line = line;
        Column = column;
    }

    public override string ToString()
    {
        return $"{Line} | {Column} | {Type} | {Value}";
    }
}