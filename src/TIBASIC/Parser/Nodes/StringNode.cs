using System;

namespace TIBASIC.Parser
{
    /// <summary>
    /// String node.
    /// </summary>
    public class StringNode: AstNode
    {
        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <value>The string.</value>
        public string String { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TIBASIC.Parser.StringNode"/> class.
        /// </summary>
        /// <param name="value">Value.</param>
        public StringNode(string value)
        {
            String = value;
        }
    }
}

