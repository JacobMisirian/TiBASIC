using System;

using TIBASIC.Lexer;

namespace TIBASIC.Parser
{
    /// <summary>
    /// Repeat node.
    /// </summary>
    public class RepeatNode: AstNode
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
        /// Initializes a new instance of the <see cref="TIBASIC.Parser.RepeatNode"/> class.
        /// </summary>
        /// <param name="predicate">Predicate.</param>
        /// <param name="body">Body.</param>
        public RepeatNode(AstNode predicate, AstNode body)
        {
            Children.Add(predicate);
            Children.Add(body);
        }
        /// <summary>
        /// Parse the specified parser.
        /// </summary>
        /// <param name="parser">Parser.</param>
        public static RepeatNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "Repeat");
            AstNode predicate = ExpressionNode.Parse(parser);
            AstNode body = StatementNode.Parse(parser);

            return new RepeatNode(predicate, body);
        }
    }
}