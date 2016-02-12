using System;

namespace TIBASIC.Parser
{
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

    public class BinaryOpNode: AstNode
    {
        public BinaryOperation BinaryOperation { get; private set; }
        public AstNode Left { get { return Children[0]; } }
        public AstNode Right { get { return Children[1]; } }

        public BinaryOpNode(BinaryOperation binaryOperation, AstNode left, AstNode right)
        {
            BinaryOperation = binaryOperation;
            Children.Add(left);
            Children.Add(right);
        }
    }
}

