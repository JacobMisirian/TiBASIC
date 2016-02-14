using System;

namespace TIBASIC.Parser
{
    /// <summary>
    /// Code block node.
    /// </summary>
    public class CodeBlockNode: AstNode
    {
        /// <summary>
        /// Parse the specified parser.
        /// </summary>
        /// <param name="parser">Parser.</param>
        public static AstNode Parse(Parser parser)
        {
            CodeBlockNode block = new CodeBlockNode();

            while (!parser.EndOfStream)
                block.Children.Add(StatementNode.Parse(parser));

            return block;
        }
    }
}

