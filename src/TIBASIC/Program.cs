using System;
using System.IO;

using TIBASIC.Lexer;
using TIBASIC.Parser;
using TIBASIC.Interpreter;

namespace TIBASIC
{
    class MainClass
    {
        public static Interpreter.Interpreter interpreter = new TIBASIC.Interpreter.Interpreter();
        public static void Main(string[] args)
        {
            if (args.Length <= 0)
                repl();
            interpreter.Interpret(new Parser.Parser(new Scanner().Scan(File.ReadAllText(args[0]))).Parse());
        }

        private static void repl()
        {
            while (true)
            {
                string source = Console.ReadLine();
                //  foreach (Token token in new Scanner().Scan(source))
                //    Console.WriteLine(token.TokenType + "\t" + token.Value);
                interpreter.Interpret(new Parser.Parser(new Scanner().Scan(source)).Parse());
            }
        }
    }
}
