using System;

namespace TIBASIC.Lexer
{
    public enum TokenType
    {
        Assignment,
        Identifier,
        Number,
        String,
        Operation,
        Comparison,
        Parentheses,
        Comma
    }

    public class Token
    {
        public TokenType TokenType { get; private set; }
        public string Value { get; private set; }
        public Token(TokenType tokenType, string value)
        {
            TokenType = tokenType;
            Value = value;
        }
    }
}

