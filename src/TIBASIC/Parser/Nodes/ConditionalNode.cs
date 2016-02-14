using System;

using TIBASIC.Lexer;

namespace TIBASIC.Parser
{
    /// <summary>
    /// Conditional node.
    /// </summary>
    public class ConditionalNode: AstNode
    {
        /// <summary>
        /// Gets the predicate.
        /// </summary>
        /// <value>The predicate.</value>
        public AstNode Predicate { get { return Children[0]; } }
        /// <summary>
        /// Gets the body.
        /// </summary>
        /// <value>The body.</value>
        public AstNode Body { get { return Children[1]; } }
        /// <summary>
        /// Gets the else body.
        /// </summary>
        /// <value>The else body.</value>
        public AstNode ElseBody { get { return Children[2]; } }
        /// <summary>
        /// Initializes a new instance of the <see cref="TIBASIC.Parser.ConditionalNode"/> class.
        /// </summary>
        /// <param name="predicate">Predicate.</param>
        /// <param name="body">Body.</param>
        public ConditionalNode(AstNode predicate, AstNode body)
        {
            Children.Add(predicate);
            Children.Add(body);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TIBASIC.Parser.ConditionalNode"/> class.
        /// </summary>
        /// <param name="predicate">Predicate.</param>
        /// <param name="body">Body.</param>
        /// <param name="elseBody">Else body.</param>
        public ConditionalNode(AstNode predicate, AstNode body, AstNode elseBody)
        {
            Children.Add(predicate);
            Children.Add(body);
            Children.Add(elseBody);
        }
        /// <summary>
        /// Parse the specified parser.
        /// </summary>
        /// <param name="parser">Parser.</param>
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

