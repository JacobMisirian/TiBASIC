using System;

namespace TIBASIC.Parser
{
    public class IdentifierNode: AstNode
    {
        public string Identifier { get; private set; }
        public IdentifierNode(string value)
        {
            Identifier = value;
        }
    }
}

