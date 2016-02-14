using System;

namespace TIBASIC.Parser
{
    /// <summary>
    /// Binary operation.
    /// </summary>
    public enum BinaryOperation
    {
        Assignment,
        Addition,
        Subtraction,
        Multiplication,
        Division,
        Modulus,
        GreaterThan,
        LessThan,
        Not,
        NotEqual,
        Equal,
        GreaterThanOrEqual,
        LessThanOrEqual,
        And,
        Or
    }
    /// <summary>
    /// Binary op node.
    /// </summary>
    public class BinaryOpNode: AstNode
    {
        /// <summary>
        /// Gets the binary operation.
        /// </summary>
        /// <value>The binary operation.</value>
        public BinaryOperation BinaryOperation { get; private set; }
        /// <summary>
        /// Gets the left.
        /// </summary>
        /// <value>The left.</value>
        public AstNode Left { get { return Children[0]; } }
        /// <summary>
        /// Gets the right.
        /// </summary>
        /// <value>The right.</value>
        public AstNode Right { get { return Children[1]; } }
        /// <summary>
        /// Initializes a new instance of the <see cref="TIBASIC.Parser.BinaryOpNode"/> class.
        /// </summary>
        /// <param name="binaryOperation">Binary operation.</param>
        /// <param name="left">Left.</param>
        /// <param name="right">Right.</param>
        public BinaryOpNode(BinaryOperation binaryOperation, AstNode left, AstNode right)
        {
            BinaryOperation = binaryOperation;
            Children.Add(left);
            Children.Add(right);
        }
    }
}

