using System;

using TIBASIC.Lexer;

namespace TIBASIC.Parser
{
    public class ConditionalNode: AstNode
    {
        public AstNode Predicate { get { return Children[0]; } }
        public AstNode Body { get { return Children[1]; } }
        public AstNode ElseBody { get { return Children[2]; } }

        public ConditionalNode(AstNode predicate, AstNode body)
        {
            Children.Add(predicate);
            Children.Add(body);
        }
        public ConditionalNode(AstNode predicate, AstNode body, AstNode elseBody)
        {
            Children.Add(predicate);
            Children.Add(body);
            Children.Add(elseBody);
        }

        public static ConditionalNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "If");
            AstNode predicate = ExpressionNode.Parse(parser);
            AstNode body = StatementNode.Parse(parser);

            if (parser.MatchToken(TokenType.Identifier, "Else"))
                return new ConditionalNode(predicate, body, StatementNode.Parse(parser));
            return new ConditionalNode(predicate, body);
        }
    }
}

