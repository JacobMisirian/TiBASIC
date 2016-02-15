using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

using TIBASIC.Lexer;
using TIBASIC.Parser;

namespace TIBASIC.Interpreter
{
    /// <summary>
    /// Ti-BASIC Interpreter.
    /// </summary>
    public class Interpreter
    {
        public string Input { get; set; }
        private int position = 0;
        public Interpreter()
        {
            Input = "";
            foreach (Dictionary<string, InternalFunction> entries in loadFunctions())
                foreach (KeyValuePair<string, InternalFunction> entry in entries)
                    variables.Add(entry.Key, entry.Value);
        }
        /// <summary>
        /// Interpret the specified ast.
        /// </summary>
        /// <param name="ast">Ast.</param>
        public void Interpret(AstNode ast)
        {
            labels = new Dictionary<string, int>();
            scanLabels(ast);

            for (position = 0; position < ast.Children.Count; position++)
                executeStatement(ast.Children[position]);
        }

        private void scanLabels(AstNode ast)
        {
            for (int i = 0; i < ast.Children.Count; i++)
            {
                if (ast.Children[i] is LblNode)
                {
                    string label = ((LblNode)ast.Children[i]).Label;
                    if (labels.ContainsKey(label))
                        throw new Exception("Label: " + label + " is already declared!");
                    labels.Add(label, position);
                }
            }
        }

        private void executeStatement(AstNode node)
        {
            if (node is CodeBlockNode)
                foreach (AstNode subNode in node.Children)
                    executeStatement(subNode);
            else if (node is ConditionalNode)
            {
                var ifNode = (ConditionalNode)node;
                bool eval = (bool)evaluateNode(ifNode.Predicate);
                if (eval)
                    executeStatement(ifNode.Body);
                else if (!eval && ifNode.Children.Count > 2)
                    executeStatement(ifNode.ElseBody);
            }
            else if (node is WhileNode)
            {
                var whileNode = (WhileNode)node;
                while ((bool)evaluateNode(whileNode.Predicate))
                    executeStatement(whileNode.Body);
            }
            else if (node is ForNode)
            {
                var forNode = (ForNode)node;
                if (!variables.ContainsKey(forNode.Variable))
                    variables.Add(forNode.Variable, 0);
                for (variables[forNode.Variable] = evaluateNode(forNode.Start); Convert.ToDouble(variables[forNode.Variable]) < Convert.ToDouble(evaluateNode(forNode.End)); variables[forNode.Variable] = Convert.ToDouble(variables[forNode.Variable]) + Convert.ToDouble(evaluateNode(forNode.Step)))
                    executeStatement(forNode.Body);
            }
            else if (node is DispNode)
            {
                var dispNode = (DispNode)node;
                foreach (AstNode subNode in dispNode.Args.Children)
                    OnConsoleOutput(new ConsoleOutputEventArgs { Output = evaluateNode(subNode).ToString() });
                OnConsoleOutput(new ConsoleOutputEventArgs { Output = "\n" });
            }
            else if (node is InputNode)
            {
                foreach (AstNode subNode in ((InputNode)node).Variables.Children)
                {
                    string variable = ((IdentifierNode)subNode).Identifier;
                    if (variables.ContainsKey(variable))
                        variables.Remove(variable);
                    variables.Add(variable, Console.ReadLine());
                }
            }
            else if (node is PromptNode)
            {
                foreach (AstNode subNode in ((PromptNode)node).Variables.Children)
                {
                    string variable = ((IdentifierNode)subNode).Identifier;
                    if (variables.ContainsKey(variable))
                        variables.Remove(variable);
                    Console.Write(variable + "? ");
                    variables.Add(variable, Console.ReadLine());
                }
            }
            else if (node is LblNode)
                ;
            else if (node is GotoNode)
            {
                string label = ((GotoNode)node).Label;
                if (!labels.ContainsKey(label))
                    throw new Exception("Label " + label + " does not exist!");
                position = labels[label];
            }
            else
                evaluateNode(node);
        }

        private object evaluateNode(AstNode node)
        {
            if (node is NumberNode)
                return ((NumberNode)node).Number;
            else if (node is StringNode)
                return ((StringNode)node).String;
            else if (node is FunctionCallNode)
            {
                FunctionCallNode functionNode = (FunctionCallNode)node;
                IFunction target = evaluateNode(functionNode.Target) as IFunction;
                if (target == null)
                    throw new Exception("Attempt to run a non-valid function!");
                object[] arguments = new object[functionNode.Arguments.Children.Count];
                for (int i = 0; i < functionNode.Arguments.Children.Count; i++)
                    arguments[i] = evaluateNode(functionNode.Arguments.Children[i]);
                return target.Invoke(arguments);
            }
            else if (node is IdentifierNode)
            {
                string identifier = ((IdentifierNode)node).Identifier;
                if (variables.ContainsKey(identifier))
                    return variables[identifier];
                else
                    throw new Exception("Identifier: " + identifier + " is not a variable!");
            }
            else if (node is BinaryOpNode)
                return interpretBinaryOperation((BinaryOpNode)node);
            else
                throw new Exception("Unexpected node in interpreter: " + node);
        }

        private object interpretBinaryOperation(BinaryOpNode binaryNode)
        {
            switch (binaryNode.BinaryOperation)
            {
                case BinaryOperation.Addition:
                    return getLeft(binaryNode) + getRight(binaryNode);
                case BinaryOperation.Subtraction:
                    return getLeft(binaryNode) - getRight(binaryNode);
                case BinaryOperation.Multiplication:
                    return getLeft(binaryNode) * getRight(binaryNode);
                case BinaryOperation.Division:
                    return getLeft(binaryNode) / getRight(binaryNode);
                case BinaryOperation.Modulus:
                    return getLeft(binaryNode) % getRight(binaryNode);
                case BinaryOperation.Equal:
                    return evaluateNode(binaryNode.Left).GetHashCode() == evaluateNode(binaryNode.Right).GetHashCode();
                case BinaryOperation.NotEqual:
                    return evaluateNode(binaryNode.Left).GetHashCode() != evaluateNode(binaryNode.Right).GetHashCode();
                case BinaryOperation.GreaterThan:
                    return getLeft(binaryNode) > getRight(binaryNode);
                case BinaryOperation.LessThan:
                    return getLeft(binaryNode) < getRight(binaryNode);
                case BinaryOperation.GreaterThanOrEqual:
                    return getLeft(binaryNode) >= getRight(binaryNode);
                case BinaryOperation.LessThanOrEqual:
                    return getLeft(binaryNode) <= getRight(binaryNode);
                case BinaryOperation.Assignment:
                    object value = evaluateNode(binaryNode.Left);
                    string variable = ((IdentifierNode)binaryNode.Right).Identifier;
                    if (variables.ContainsKey(variable))
                        variables.Remove(variable);
                    variables.Add(variable, value);
                    return value;
                case BinaryOperation.And:
                    return (bool)evaluateNode(binaryNode.Left) && (bool)evaluateNode(binaryNode.Right);
                case BinaryOperation.Or:
                    return (bool)evaluateNode(binaryNode.Left) || (bool)evaluateNode(binaryNode.Right);
                default:
                    throw new Exception("Unexpected binary operation in interpreter: " + binaryNode.BinaryOperation);
            }
        }

        private string readLine()
        {
            OnConsoleInput(new ConsoleInputEventArgs());
            while (Input == "")
                Thread.Sleep(15);
            string temp = Input;
            Input = "";

            return temp;
        }

        private double getLeft(BinaryOpNode binaryNode)
        {
            return Convert.ToDouble(evaluateNode(binaryNode.Left));
        }

        private double getRight(BinaryOpNode binaryNode)
        {
            return Convert.ToDouble(evaluateNode(binaryNode.Right));
        }

        private Dictionary<string, object> variables = new Dictionary<string, object>()
        {
            { "True", true },
            { "False", false },
            { "Null", null }
        };
        private Dictionary<string, int> labels = new Dictionary<string, int>();

        private List<Dictionary<string, InternalFunction>> loadFunctions(string path = "")
        {
            List<Dictionary<string, InternalFunction>> result = new List<Dictionary<string, InternalFunction>>();
            Assembly testAss;

            if (path != "")
                testAss = Assembly.LoadFrom(path);
            else
                testAss = Assembly.GetExecutingAssembly();

            foreach (Type type in testAss.GetTypes())
                if (type.GetInterface(typeof(ILibrary).FullName) != null)
            {
                ILibrary ilib = (ILibrary)Activator.CreateInstance(type);
                result.Add(ilib.GetFunctions());
            }

            return result;
        }

        public event EventHandler<ConsoleOutputEventArgs> ConsoleOutput;
        protected virtual void OnConsoleOutput(ConsoleOutputEventArgs e)
        {
            EventHandler<ConsoleOutputEventArgs> handler = ConsoleOutput;
            if (handler != null)
                handler(this, e);
        }
        public event EventHandler<ConsoleInputEventArgs> ConsoleInput;
        protected virtual void OnConsoleInput(ConsoleInputEventArgs e)
        {
            EventHandler<ConsoleInputEventArgs> handler = ConsoleInput;
            if (handler != null)
                handler(this, e);
        }
    }
}