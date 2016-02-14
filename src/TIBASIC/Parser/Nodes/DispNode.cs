using System;

using TIBASIC.Lexer;

namespace TIBASIC.Parser
{
    /// <summary>
    /// Disp node.
    /// </summary>
    public class DispNode: AstNode
    {
        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        public ArgListNode Args { get { return (ArgListNode)Children[0]; } }
        /// <summary>
        /// Initializes a new instance of the <see cref="TIBASIC.Parser.DispNode"/> class.
        /// </summary>
        /// <param name="args">Arguments.</param>
        public DispNode(ArgListNode args)
        {
            Children.Add(args);
        }
        /// <summary>
        /// Parse the specified parser.
        /// </summary>
        /// <param name="parser">Parser.</param>
        public static DispNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "Disp");
            ArgListNode args = ArgListNode.Parse(parser);

            return new DispNode(args);
        }
    }
}

