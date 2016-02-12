using System;

using TIBASIC.Lexer;

namespace TIBASIC.Parser
{
    public class StatementNode: AstNode
    {
        public static AstNode Parse(Parser parser)
        {
            if (parser.MatchToken(TokenType.Identifier, "If"))
                return ConditionalNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "While"))
                return WhileNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "Disp"))
                return DispNode.Parse(parser);
            else if (parser.AcceptToken(TokenType.Identifier, "Lbl"))
                return new LblNode(parser.ExpectToken(TokenType.Identifier).Value);
            else if (parser.AcceptToken(TokenType.Identifier, "Goto"))
                return new GotoNode(parser.ExpectToken(TokenType.Identifier).Value);
            else
                return ExpressionNode.Parse(parser);
        }
    }
}

