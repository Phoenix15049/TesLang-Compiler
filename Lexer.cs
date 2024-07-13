namespace TesLang_Compiler;

public class Lexer
{
    private string code;
    private int line;
    private int column;
    private int index;

    private static readonly Dictionary<string, TokenType> Keywords = new()
    {
        { "fn", TokenType.FN },
        { "as", TokenType.AS },
        { "vector", TokenType.VECTOR },
        { "int", TokenType.INT },
        { "for", TokenType.FOR },
        { "to", TokenType.TO },
        { "length", TokenType.LEN },
        { "begin", TokenType.BEGIN },
        { "end", TokenType.END },
        { "return", TokenType.RETURN }
    };

    public Lexer(string code)
    {
        this.code = code;
        this.line = 1;
        this.column = 1;
        this.index = 0;
    }

    public List<Token> Tokenize()
    {
        List<Token> tokens = new();

        while (index < code.Length)
        {
            char c = code[index];

            if (char.IsWhiteSpace(c))
            {
                if (c == '\n')
                {
                    line++;
                    column = 0;
                }
                index++;
                column++;
            }
            else if (char.IsLetter(c))
            {
                tokens.Add(ReadIdentifierOrKeyword());
            }
            else if (char.IsDigit(c))
            {
                tokens.Add(ReadNumber());
            }
            else
            {
                switch (c)
                {
                    case '(':
                        tokens.Add(new Token(TokenType.LPAREN, c.ToString(), line, column));
                        break;
                    case ')':
                        tokens.Add(new Token(TokenType.RPAREN, c.ToString(), line, column));
                        break;
                    case '{':
                        tokens.Add(new Token(TokenType.LCURLYBR, c.ToString(), line, column));
                        break;
                    case '}':
                        tokens.Add(new Token(TokenType.RCURLYBR, c.ToString(), line, column));
                        break;
                    case '<':
                        tokens.Add(new Token(TokenType.LESS_THAN, c.ToString(), line, column));
                        break;
                    case '>':
                        tokens.Add(new Token(TokenType.GREATER_THAN, c.ToString(), line, column));
                        break;
                    case '=':
                        tokens.Add(new Token(TokenType.EQ, c.ToString(), line, column));
                        break;
                    case ';':
                        tokens.Add(new Token(TokenType.SEMI_COLON, c.ToString(), line, column));
                        break;
                    case '+':
                        tokens.Add(new Token(TokenType.PLUS, c.ToString(), line, column));
                        break;
                    case '[':
                        tokens.Add(new Token(TokenType.LSQUAREBR, c.ToString(), line, column));
                        break;
                    case ']':
                        tokens.Add(new Token(TokenType.RSQUAREBR, c.ToString(), line, column));
                        break;
                    case ',':
                        tokens.Add(new Token(TokenType.COMMA, c.ToString(), line, column));
                        break;
                    default:
                        tokens.Add(new Token(TokenType.UNKNOWN, c.ToString(), line, column));
                        break;
                }
                index++;
                column++;
            }
        }

        return tokens;
    }

    private Token ReadIdentifierOrKeyword()
    {
        int startColumn = column;
        int startLine = line;
        int startIndex = index;

        while (index < code.Length && char.IsLetterOrDigit(code[index]))
        {
            index++;
            column++;
        }

        string text = code[startIndex..index];
        TokenType type = Keywords.ContainsKey(text) ? Keywords[text] : TokenType.ID;

        return new Token(type, text, startLine, startColumn);
    }

    private Token ReadNumber()
    {
        int startColumn = column;
        int startLine = line;
        int startIndex = index;

        while (index < code.Length && char.IsDigit(code[index]))
        {
            index++;
            column++;
        }

        string text = code[startIndex..index];
        return new Token(TokenType.NUMBER, text, startLine, startColumn);
    }
}