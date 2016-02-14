using System;

namespace TIBASIC.Parser
{
    /// <summary>
    /// Lbl node.
    /// </summary>
    public class LblNode: AstNode
    {
        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <value>The label.</value>
        public string Label { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TIBASIC.Parser.LblNode"/> class.
        /// </summary>
        /// <param name="label">Label.</param>
        public LblNode(string label)
        {
            Label = label;
        }
    }
}

