using System;

using TIBASIC.Lexer;

namespace TIBASIC.Parser
{
    public class DispNode: AstNode
    {
        public ArgListNode Args { get { return (ArgListNode)Children[0]; } }
        public DispNode(ArgListNode args)
        {
            Children.Add(args);
        }

        public static DispNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "Disp");
            ArgListNode args = ArgListNode.Parse(parser);

            return new DispNode(args);
        }
    }
}

