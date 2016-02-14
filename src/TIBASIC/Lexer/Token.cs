using System;

namespace TIBASIC.Lexer
{
    /// <summary>
    /// Token type.
    /// </summary>
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
    /// <summary>
    /// Token.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        /// <value>The type of the token.</value>
        public TokenType TokenType { get; private set; }
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TIBASIC.Lexer.Token"/> class.
        /// </summary>
        /// <param name="tokenType">Token type.</param>
        /// <param name="value">Value.</param>
        public Token(TokenType tokenType, string value)
        {
            TokenType = tokenType;
            Value = value;
        }
    }
}

