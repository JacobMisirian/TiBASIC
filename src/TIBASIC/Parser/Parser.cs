using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using TIBASIC.Lexer;

namespace TIBASIC.Parser
{
    public class Parser
    {
        private List<Token> tokens { get; set; }

        public bool EndOfStream { get { return tokens.Count <= Position; } }
        public int Position = 0;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public AstNode Parse()
        {
            CodeBlockNode tree = new CodeBlockNode();
            while (!EndOfStream)
                tree.Children.Add(StatementNode.Parse(this));

            return tree;
        }

        public bool MatchToken(TokenType clazz)
        {
            return Position < tokens.Count && tokens[Position].TokenType == clazz;
        }

        public bool MatchToken(TokenType clazz, string value)
        {
            return Position < tokens.Count && tokens[Position].TokenType == clazz && ((string)tokens[Position].Value).ToUpper() == value.ToUpper();
        }

        public bool AcceptToken(TokenType clazz)
        {
            if (MatchToken(clazz))
            {
                Position++;
                return true;
            }

            return false;
        }

        public bool AcceptToken(TokenType clazz, string value)
        {
            if (MatchToken(clazz, value.ToUpper()))
            {
                Position++;
                return true;
            }

            return false;
        }

        public Token ExpectToken(TokenType clazz)
        {
            if (!MatchToken(clazz))
                throw new Exception("Tokens did not match. Expected " + clazz);

            return tokens[Position++];
        }

        public Token ExpectToken(TokenType clazz, string value)
        {
            if (!MatchToken(clazz, value.ToUpper()))
                throw new Exception("Tokens did not match. Expected " + clazz + " of value " + value);

            return tokens[Position++];
        }

        public Token ReadToken(int n = 0)
        {
            return tokens[Position + n];
        }

        public Token CurrentToken(int n = 0)
        {
            if (tokens.Count > Position + n)
                return tokens[Position + n];

            return new Token(TokenType.Identifier, "");
        }
    }
}