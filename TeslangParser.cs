using TesLang_Compiler;
using System;
using System.Collections.Generic;

namespace TesLang_Compiler
{
    public class TeslangParser
    {
        private List<Token> tokens;
        private int current;

        public TeslangParser(List<Token> tokens)
        {
            this.tokens = tokens;
            this.current = 0;
        }

        public void Parse()
        {
            while (!IsAtEnd())
            {
                ParseProg();
            }
        }

        private void ParseProg()
        {
            while (!IsAtEnd())
            {
                ParseFunc();
            }
        }

        private void ParseFunc()
        {
            Consume(TokenType.FN, "Expect 'fn' keyword.");
            Token funcName = Consume(TokenType.ID, "Expect function name.");
            Consume(TokenType.LPAREN, "Expect '(' after function name.");
            ParseFlist();
            Consume(TokenType.RPAREN, "Expect ')' after parameters.");
            Consume(TokenType.LESS_THAN, "Expect '<' before return type.");
            Token returnType = Consume(TokenType.ID, "Expect return type.");
            Consume(TokenType.GREATER_THAN, "Expect '>' after return type.");

            if (Match(TokenType.LCURLYBR))
            {
                ParseBody();
                Consume(TokenType.RCURLYBR, "Expect '}' after function body.");
            }
            else if (Match(TokenType.EQ))
            {
                ParseExpr();
                Consume(TokenType.SEMI_COLON, "Expect ';' after expression.");
            }
            else
            {
                throw new Exception("Expect '{' or '=>' after function type.");
            }
        }

        private void ParseFlist()
        {
            if (!Check(TokenType.RPAREN))
            {
                do
                {
                    Token paramName = Consume(TokenType.ID, "Expect parameter name.");
                    Consume(TokenType.AS, "Expect 'as' after parameter name.");
                    Token paramType = Consume(TokenType.ID, "Expect parameter type after 'as'.");
                    // Store the parameter in the symbol table (implementation not shown)
                } while (Match(TokenType.COMMA));
            }
        }

        private void ParseBody()
        {
            while (!Check(TokenType.RCURLYBR) && !IsAtEnd())
            {
                ParseStmt();
            }
        }

        private void ParseStmt()
        {
            if (Match(TokenType.RETURN))
            {
                ParseExpr();
                Consume(TokenType.SEMI_COLON, "Expect ';' after return statement.");
            }
            else if (Match(TokenType.FOR))
            {
                Consume(TokenType.LPAREN, "Expect '(' after 'for'.");
                Token loopVar = Consume(TokenType.ID, "Expect loop variable.");
                Consume(TokenType.EQ, "Expect '=' after loop variable.");
                ParseExpr();
                Consume(TokenType.TO, "Expect 'to' in loop.");
                ParseExpr();
                Consume(TokenType.RPAREN, "Expect ')' after loop range.");
                Consume(TokenType.BEGIN, "Expect 'begin' before loop body.");
                ParseBody();
                Consume(TokenType.END, "Expect 'end' after loop body.");
            }
            else
            {
                ParseExpr();
                Consume(TokenType.SEMI_COLON, "Expect ';' after expression.");
            }
        }

        private void ParseExpr()
        {
            ParsePrimary();

            while (Match(TokenType.PLUS))
            {
                Token operatorToken = Previous();
                ParsePrimary();
            }
        }

        private void ParsePrimary()
        {
            if (Match(TokenType.NUMBER))
            {
                // Handle number literal
                Token numberToken = Previous();
                Console.WriteLine($"Parsed number literal: {numberToken.Value} at line {numberToken.Line}, column {numberToken.Column}");
            }
            else if (Match(TokenType.ID))
            {
                if (Match(TokenType.LPAREN))
                {
                    ParseCall();
                }
                else if (Match(TokenType.EQ))
                {
                    ParseAssignment();
                }
            }
            else if (Match(TokenType.LSQUAREBR))
            {
                // Parse vectors
                ParseVector();
            }
            else
            {
                throw new Exception("Expect expression.");
            }
        }

        private void ParseCall()
        {
            if (!Check(TokenType.RPAREN))
            {
                do
                {
                    ParseExpr();
                } while (Match(TokenType.COMMA));
            }
            Consume(TokenType.RPAREN, "Expect ')' after arguments.");
        }

        private void ParseAssignment()
        {
            ParseExpr();
            Consume(TokenType.SEMI_COLON, "Expect ';' after assignment.");
        }

        private void ParseVector()
        {
            do
            {
                ParseExpr();
            } while (Match(TokenType.COMMA));

            Consume(TokenType.RSQUAREBR, "Expect ']' after vector elements.");
        }

        private Token Consume(TokenType type, string message)
        {
            if (Check(type)) return Advance();
            throw new Exception(message);
        }

        private bool Match(TokenType type)
        {
            if (Check(type))
            {
                Advance();
                return true;
            }
            return false;
        }

        private bool Check(TokenType type)
        {
            if (IsAtEnd()) return false;
            return Peek().Type == type;
        }

        private Token Advance()
        {
            if (!IsAtEnd()) current++;
            return Previous();
        }

        private bool IsAtEnd()
        {
            return Peek().Type == TokenType.UNKNOWN;
        }

        private Token Peek()
        {
            return tokens[current];
        }

        private Token Previous()
        {
            return tokens[current - 1];
        }
    }
}

