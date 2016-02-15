using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TIBASIC.Lexer;

namespace TIBASIC.Parser
{
    public class PromptNode: AstNode
    {
        public ArgListNode Variables { get { return (ArgListNode)Children[0]; } }

        public PromptNode(ArgListNode variables)
        {
            Children.Add(variables);
        }

        public static PromptNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "Prompt");
            ArgListNode variables = ArgListNode.Parse(parser);

            return new PromptNode(variables);
        }
    }
}
