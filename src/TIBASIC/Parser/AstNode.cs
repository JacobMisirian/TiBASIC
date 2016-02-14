using System;
using System.Collections.Generic;

namespace TIBASIC.Parser
{
    /// <summary>
    /// Ast node.
    /// </summary>
    public abstract class AstNode
    {
        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <value>The children.</value>
        public List<AstNode> Children { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TIBASIC.Parser.AstNode"/> class.
        /// </summary>
        public AstNode()
        {
            Children = new List<AstNode>();
        }
    }
}

