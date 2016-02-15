using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TIBASIC.Lexer;

namespace TIBASIC.Parser
{
    public class ForNode: AstNode
    {
        public string Variable { get; private set; }
        public AstNode Start { get { return Children[0]; } }
        public AstNode End { get { return Children[1]; } }
        public AstNode Step { get { return Children[2]; } }
        public AstNode Body { get { return Children[3]; } }

        public ForNode(string variable, AstNode start, AstNode end, AstNode step, AstNode body)
        {
            Variable = variable;
            Children.Add(start);
            Children.Add(end);
            Children.Add(step);
            Children.Add(body);
        }

        public static ForNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "For");
            parser.ExpectToken(TokenType.Parentheses, "(");
            string variable = parser.ExpectToken(TokenType.Identifier).Value;
            parser.ExpectToken(TokenType.Comma);
            AstNode start = ExpressionNode.Parse(parser);
            parser.ExpectToken(TokenType.Comma);
            AstNode end = ExpressionNode.Parse(parser);
            AstNode step = new NumberNode(1);
            if (parser.AcceptToken(TokenType.Comma))
                step = ExpressionNode.Parse(parser);
            parser.ExpectToken(TokenType.Parentheses, ")");
            AstNode body = StatementNode.Parse(parser);

            return new ForNode(variable, start, end, step, body);
        }
    }
}
