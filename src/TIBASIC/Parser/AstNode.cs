using System;
using System.Collections.Generic;

namespace TIBASIC.Parser
{
    public abstract class AstNode
    {
        public List<AstNode> Children { get; private set; }
        public AstNode()
        {
            Children = new List<AstNode>();
        }
    }
}

