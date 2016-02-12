using System;

namespace TIBASIC.Parser
{
    public class NumberNode: AstNode
    {
        public double Number { get; private set; }
        public NumberNode(double value)
        {
            Number = value;
        }
    }
}

