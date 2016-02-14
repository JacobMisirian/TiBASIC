using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using TIBASIC.Lexer;

namespace TIBASIC.Parser
{
    /// <summary>
    /// Parser.
    /// </summary>
    public class Parser
    {
        private List<Token> tokens { get; set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="TIBASIC.Parser.Parser"/> end of stream.
        /// </summary>
        /// <value><c>true</c> if end of stream; otherwise, <c>false</c>.</value>
        public bool EndOfStream { get { return tokens.Count <= Position; } }
        /// <summary>
        /// The position.
        /// </summary>
        public int Position = 0;
        /// <summary>
        /// Initializes a new instance of the <see cref="TIBASIC.Parser.Parser"/> class.
        /// </summary>
        /// <param name="tokens">Tokens.</param>
        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }
        /// <summary>
        /// Parse this instance.
        /// </summary>
        public AstNode Parse()
        {
            CodeBlockNode tree = new CodeBlockNode();
            while (!EndOfStream)
                tree.Children.Add(StatementNode.Parse(this));

            return tree;
        }
        /// <summary>
        /// Matchs the token.
        /// </summary>
        /// <returns><c>true</c>, if token was matched, <c>false</c> otherwise.</returns>
        /// <param name="clazz">Clazz.</param>
        public bool MatchToken(TokenType clazz)
        {
            return Position < tokens.Count && tokens[Position].TokenType == clazz;
        }
        /// <summary>
        /// Matchs the token.
        /// </summary>
        /// <returns><c>true</c>, if token was matched, <c>false</c> otherwise.</returns>
        /// <param name="clazz">Clazz.</param>
        /// <param name="value">Value.</param>
        public bool MatchToken(TokenType clazz, string value)
        {
            return Position < tokens.Count && tokens[Position].TokenType == clazz && ((string)tokens[Position].Value).ToUpper() == value.ToUpper();
        }
        /// <summary>
        /// Accepts the token.
        /// </summary>
        /// <returns><c>true</c>, if token was accepted, <c>false</c> otherwise.</returns>
        /// <param name="clazz">Clazz.</param>
        public bool AcceptToken(TokenType clazz)
        {
            if (MatchToken(clazz))
            {
                Position++;
                return true;
            }

            return false;
        }
        /// <summary>
        /// Accepts the token.
        /// </summary>
        /// <returns><c>true</c>, if token was accepted, <c>false</c> otherwise.</returns>
        /// <param name="clazz">Clazz.</param>
        /// <param name="value">Value.</param>
        public bool AcceptToken(TokenType clazz, string value)
        {
            if (MatchToken(clazz, value.ToUpper()))
            {
                Position++;
                return true;
            }

            return false;
        }
        /// <summary>
        /// Expects the token.
        /// </summary>
        /// <returns>The token.</returns>
        /// <param name="clazz">Clazz.</param>
        public Token ExpectToken(TokenType clazz)
        {
            if (!MatchToken(clazz))
                throw new Exception("Tokens did not match. Expected " + clazz);

            return tokens[Position++];
        }
        /// <summary>
        /// Expects the token.
        /// </summary>
        /// <returns>The token.</returns>
        /// <param name="clazz">Clazz.</param>
        /// <param name="value">Value.</param>
        public Token ExpectToken(TokenType clazz, string value)
        {
            if (!MatchToken(clazz, value.ToUpper()))
                throw new Exception("Tokens did not match. Expected " + clazz + " of value " + value);

            return tokens[Position++];
        }
        /// <summary>
        /// Reads the token.
        /// </summary>
        /// <returns>The token.</returns>
        /// <param name="n">N.</param>
        public Token ReadToken(int n = 0)
        {
            return tokens[Position + n];
        }
        /// <summary>
        /// Currents the token.
        /// </summary>
        /// <returns>The token.</returns>
        /// <param name="n">N.</param>
        public Token CurrentToken(int n = 0)
        {
            if (tokens.Count > Position + n)
                return tokens[Position + n];

            return new Token(TokenType.Identifier, "");
        }
    }
}