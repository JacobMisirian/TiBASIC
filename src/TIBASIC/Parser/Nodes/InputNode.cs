using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TIBASIC.Lexer;

namespace TIBASIC.Parser
{
    public class InputNode: AstNode
    {
        public ArgListNode Variables { get { return (ArgListNode)Children[0]; } }

        public InputNode(ArgListNode variables)
        {
            Children.Add(variables);
        }

        public static InputNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "Input");
            ArgListNode variables = ArgListNode.Parse(parser);

            return new InputNode(variables);
        }
    }
}
