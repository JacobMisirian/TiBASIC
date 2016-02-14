using System;

namespace TIBASIC.Parser
{
    /// <summary>
    /// Goto node.
    /// </summary>
    public class GotoNode: AstNode
    {
        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <value>The label.</value>
        public string Label { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TIBASIC.Parser.GotoNode"/> class.
        /// </summary>
        /// <param name="label">Label.</param>
        public GotoNode(string label)
        {
            Label = label;
        }
    }
}

