using System;

using TIBASIC.Lexer;

namespace TIBASIC.Parser
{
    public class ArgListNode: AstNode
    {
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

