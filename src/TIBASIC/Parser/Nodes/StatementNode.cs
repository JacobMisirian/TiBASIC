using System;

using TIBASIC.Lexer;

namespace TIBASIC.Parser
{
    /// <summary>
    /// Statement node.
    /// </summary>
    public class StatementNode: AstNode
    {
        /// <summary>
        /// Parse the specified parser.
        /// </summary>
        /// <param name="parser">Parser.</param>
        public static AstNode Parse(Parser parser)
        {
            if (parser.MatchToken(TokenType.Identifier, "If"))
                return ConditionalNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "While"))
                return WhileNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "Repeat"))
                return RepeatNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "For"))
                return ForNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "Disp"))
                return DispNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "Input"))
                return InputNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "Prompt"))
                return PromptNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "Prgm"))
                return PrgmNode.Parse(parser);
            else if (parser.AcceptToken(TokenType.Identifier, "Lbl"))
                return new LblNode(parser.ExpectToken(TokenType.Identifier).Value);
            else if (parser.AcceptToken(TokenType.Identifier, "Goto"))
                return new GotoNode(parser.ExpectToken(TokenType.Identifier).Value);
            else
                return ExpressionNode.Parse(parser);
        }
    }
}

