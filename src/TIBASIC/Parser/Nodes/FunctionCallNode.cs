using System;

namespace TIBASIC.Parser
{
    /// <summary>
    /// Function call node.
    /// </summary>
    public class FunctionCallNode: AstNode
    {
        /// <summary>
        /// Gets the target.
        /// </summary>
        /// <value>The target.</value>
        public AstNode Target { get { return Children[0]; } }
        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        public AstNode Arguments { get { return Children[1]; } }
        /// <summary>
        /// Initializes a new instance of the <see cref="TIBASIC.Parser.FunctionCallNode"/> class.
        /// </summary>
        /// <param name="target">Target.</param>
        /// <param name="arguments">Arguments.</param>
        public FunctionCallNode(AstNode target, ArgListNode arguments)
        {
            Children.Add(target);
            Children.Add(arguments);
        }
    }
}

