using System;

namespace TIBASIC.Parser
{
    public class GotoNode: AstNode
    {
        public string Label { get; private set; }
        public GotoNode(string label)
        {
            Label = label;
        }
    }
}

