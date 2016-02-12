using System;

namespace TIBASIC.Parser
{
    public class LblNode: AstNode
    {
        public string Label { get; private set; }
        public LblNode(string label)
        {
            Label = label;
        }
    }
}

