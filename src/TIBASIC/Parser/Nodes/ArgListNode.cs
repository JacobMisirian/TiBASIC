using System;

using TIBASIC.Lexer;

namespace TIBASIC.Parser
{
    /// <summary>
    /// Argument list node.
    /// </summary>
    public class ArgListNode: AstNode
    {
        /// <summary>
        /// Parse the specified parser.
        /// </summary>
        /// <param name="parser">Parser.</param>
        public static ArgListNode Parse(Parser parser)
        {
            ArgListNode ret = new ArgListNode();
            ret.Children.Add(ExpressionNode.Parse(parser));
            while (parser.AcceptToken(TokenType.Comma))
                ret.Children.Add(ExpressionNode.Parse(parser));
            return ret;
        }
    }
}

