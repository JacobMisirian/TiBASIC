using System;

using TIBASIC.Lexer;

namespace TIBASIC.Parser
{
    public class ExpressionNode: AstNode
    {
        public static AstNode Parse(Parser parser)
        {
            return parseAssignment(parser);
        }

        private static AstNode parseAssignment(Parser parser)
        {
            AstNode left = parseAdditive(parser);

            if (parser.AcceptToken(TokenType.Assignment))
                return new BinaryOpNode(BinaryOperation.Assignment, left, parseAssignment(parser));
            else
                return left;
        }

        private static AstNode parseAdditive(Parser parser)
        {
            AstNode left = parseMultiplicitive(parser);
            while (parser.MatchToken(TokenType.Operation) || parser.MatchToken(TokenType.Identifier, "and") || parser.MatchToken(TokenType.Identifier, "or"))
            {
                switch ((string)parser.CurrentToken().Value)
                {
                    case "+":
                        parser.AcceptToken(TokenType.Operation);
                        left = new BinaryOpNode(BinaryOperation.Addition, left, parseMultiplicitive(parser));
                        continue;
                    case "-":
                        parser.AcceptToken(TokenType.Operation);
                        left = new BinaryOpNode(BinaryOperation.Subtraction, left, parseMultiplicitive(parser));
                        continue;
                    case "and":
                        parser.AcceptToken(TokenType.Identifier);
                        left = new BinaryOpNode(BinaryOperation.And, left, parseMultiplicitive(parser));
                        continue;
                    case "or":
                        parser.AcceptToken(TokenType.Identifier);
                        left = new BinaryOpNode(BinaryOperation.Or, left, parseMultiplicitive(parser));
                        continue;
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private static AstNode parseMultiplicitive(Parser parser)
        {
            AstNode left = parseComparison(parser);
            while (parser.MatchToken(TokenType.Operation))
            {
                switch ((string)parser.CurrentToken().Value)
                {
                    case "*":
                        parser.AcceptToken(TokenType.Operation);
                        left = new BinaryOpNode(BinaryOperation.Multiplication, left, parseComparison(parser));
                        continue;
                    case "/":
                        parser.AcceptToken(TokenType.Operation);
                        left = new BinaryOpNode(BinaryOperation.Division, left, parseComparison(parser));
                        continue;
                    case "%":
                        parser.AcceptToken(TokenType.Operation);
                        left = new BinaryOpNode(BinaryOperation.Modulus, left, parseComparison(parser));
                        continue;
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private static AstNode parseComparison(Parser parser)
        {
            AstNode left = parseFunctionCall(parser);
            while (parser.MatchToken(TokenType.Comparison))
            {
                switch ((string)parser.CurrentToken().Value)
                {
                    case "=":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOpNode(BinaryOperation.Equal, left, parseFunctionCall(parser));
                        continue;
                    case "!=":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOpNode(BinaryOperation.NotEqual, left, parseFunctionCall(parser));
                        continue;
                    case ">":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOpNode(BinaryOperation.GreaterThan, left, parseFunctionCall(parser));
                        continue;
                    case "<":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOpNode(BinaryOperation.LessThan, left, parseFunctionCall(parser));
                        continue;
                    case ">=":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOpNode(BinaryOperation.GreaterThanOrEqual, left, parseFunctionCall(parser));
                        continue;
                    case "<=":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOpNode(BinaryOperation.LessThanOrEqual, left, parseFunctionCall(parser));
                        continue;
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private static AstNode parseFunctionCall(Parser parser)
        {
            return parseFunctionCall(parser, parseTerm(parser));
        }
        private static AstNode parseFunctionCall(Parser parser, AstNode left)
        {
            if (parser.MatchToken(TokenType.Parentheses, "("))
                return parseFunctionCall(parser, new FunctionCallNode(left, ArgListNode.Parse(parser)));
            else
                return left;
        }

        private static AstNode parseTerm(Parser parser)
        {
            if (parser.MatchToken(TokenType.Number))
                return new NumberNode(Convert.ToDouble(parser.ExpectToken(TokenType.Number).Value));
            else if (parser.AcceptToken(TokenType.Parentheses, "("))
            {
                AstNode statement = ExpressionNode.Parse(parser);
                parser.ExpectToken(TokenType.Parentheses, ")");
                return statement;
            }
            else if (parser.MatchToken(TokenType.Identifier, "Then"))
            {
                CodeBlockNode block = new CodeBlockNode();
                parser.ExpectToken(TokenType.Identifier, "Then");

                while (!parser.EndOfStream && !parser.MatchToken(TokenType.Identifier, "EndIf") && !parser.MatchToken(TokenType.Identifier, "Else"))
                    block.Children.Add(StatementNode.Parse(parser));

                if (parser.MatchToken(TokenType.Identifier, "Else"))
                    return block;

                parser.ExpectToken(TokenType.Identifier, "EndIf");

                return block;
            }
            else if (parser.MatchToken(TokenType.Identifier, "Else"))
            {
                parser.ExpectToken(TokenType.Identifier, "Else");
                return StatementNode.Parse(parser);
            }
            else if (parser.MatchToken(TokenType.Identifier, "Do"))
            {
                CodeBlockNode block = new CodeBlockNode();
                parser.ExpectToken(TokenType.Identifier, "Do");

                while (!parser.EndOfStream && !parser.MatchToken(TokenType.Identifier, "End"))
                    block.Children.Add(StatementNode.Parse(parser));

                parser.ExpectToken(TokenType.Identifier, "End");

                return block;
            }
            else if (parser.MatchToken(TokenType.String))
                return new StringNode((string)parser.ExpectToken(TokenType.String).Value);
            else if (parser.MatchToken(TokenType.Identifier))
                return new IdentifierNode((string)parser.ExpectToken(TokenType.Identifier).Value);
            else
                throw new Exception("Unexpected " + parser.CurrentToken().TokenType + " in Parser: " + parser.CurrentToken().Value + ".");

        }
    }
}

