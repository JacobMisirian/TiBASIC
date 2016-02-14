using System;
using System.Collections.Generic;

namespace TIBASIC.Lexer
{
    /// <summary>
    /// Scanner.
    /// </summary>
    public class Scanner
    {
        private List<Token> result = new List<Token>();
        private string code;
        private int position = 0;

        private string peekLetter { get { return ((char)code[position]).ToString(); } }
        private string readLetter { get { return ((char)code[position++]).ToString(); } }
        /// <summary>
        /// Scan the specified code for Tokens.
        /// </summary>
        /// <param name="code">Code.</param>
        public List<Token> Scan(string code)
        {
            this.code = code;
            whiteSpace();
            while (peekChar() != -1)
            {
                if (char.IsLetterOrDigit((char)peekChar()))
                    result.Add(scanData());
                else
                {
                    switch (peekLetter)
                    {
                        case "\"":
                            result.Add(scanString());
                            break;
                        case "+":
                        case "*":
                        case "/":
                        case "%":
                            result.Add(new Token(TokenType.Operation, readLetter));
                            break;
                        case "-":
                            string op = readLetter;
                            if (peekLetter == ">")
                                result.Add(new Token(TokenType.Assignment, op + readLetter));
                            else
                                result.Add(new Token(TokenType.Operation, op));
                            break;
                        case ">":
                            position++;
                            if (peekLetter == "=")
                                result.Add(new Token(TokenType.Comparison, ">" + readLetter));
                            else
                                result.Add(new Token(TokenType.Comparison, ">"));
                            break;
                        case "<":
                            position++;
                            if (peekLetter == "=")
                                result.Add(new Token(TokenType.Comparison, "<" + readLetter));
                            else
                                result.Add(new Token(TokenType.Comparison, "<"));
                            break;
                        case "=":
                            result.Add(new Token(TokenType.Comparison, readLetter));
                            break;
                        case "!":
                            result.Add(new Token(TokenType.Comparison, readLetter));
                            break;
                        case "(":
                        case ")":
                            result.Add(new Token(TokenType.Parentheses, readLetter));
                            break;
                        case ",":
                            result.Add(new Token(TokenType.Comma, readLetter));
                            break;
                        default:
                            Console.WriteLine("Unknown char: " + readLetter);
                            break;
                    }
                }
                whiteSpace();
            }

            return result;
        }
        
        private Token scanData()
        {
            string result = "";
            double temp = 0;
            while ((char.IsLetterOrDigit((char)peekChar()) && peekChar() != -1) || ((char)(peekChar()) == '.'))
                result += ((char)readChar()).ToString();
            if (double.TryParse(result, out temp))
                return new Token(TokenType.Number, result);

            return new Token(TokenType.Identifier, result);
        }

        private Token scanString()
        {
            position++;
            string res = "";
            while (peekLetter != "\"" && peekChar() != -1)
                res += readLetter;
            position++;

            return new Token(TokenType.String, res);
        }

        private void whiteSpace()
        {
            while (char.IsWhiteSpace((char)peekChar()))
                position++;
        }

        private int peekChar(int n = 0)
        {
            return code.Length > position + n ? code[position + n] : -1;
        }

        private int readChar()
        {
            return code.Length > position ? code[position++] : -1;
        }
    }
}

