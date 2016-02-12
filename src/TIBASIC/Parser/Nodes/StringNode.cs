using System;

namespace TIBASIC.Parser
{
    public class StringNode: AstNode
    {
        public string String { get; private set; }
        public StringNode(string value)
        {
            String = value;
        }
    }
}

