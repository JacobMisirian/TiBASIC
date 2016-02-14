using System;

namespace TIBASIC.Parser
{
    /// <summary>
    /// Number node.
    /// </summary>
    public class NumberNode: AstNode
    {
        /// <summary>
        /// Gets the number.
        /// </summary>
        /// <value>The number.</value>
        public double Number { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TIBASIC.Parser.NumberNode"/> class.
        /// </summary>
        /// <param name="value">Value.</param>
        public NumberNode(double value)
        {
            Number = value;
        }
    }
}

