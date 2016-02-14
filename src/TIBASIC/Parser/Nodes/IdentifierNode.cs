using System;

namespace TIBASIC.Parser
{
    /// <summary>
    /// Identifier node.
    /// </summary>
    public class IdentifierNode: AstNode
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Identifier { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TIBASIC.Parser.IdentifierNode"/> class.
        /// </summary>
        /// <param name="value">Value.</param>
        public IdentifierNode(string value)
        {
            Identifier = value;
        }
    }
}

