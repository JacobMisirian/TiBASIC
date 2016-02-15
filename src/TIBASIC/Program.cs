using System;
using System.IO;

using TIBASIC.Lexer;
using TIBASIC.Parser;
using TIBASIC.Interpreter;

namespace TIBASIC
{
    class MainClass
    {
        /// <summary>
        /// The interpreter.
        /// </summary>
        public static Interpreter.Interpreter interpreter = new TIBASIC.Interpreter.Interpreter();
        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            interpreter.ConsoleInput += interpreter_OnConsoleInput;
            interpreter.ConsoleRead += interpreter_OnConsoleRead;
            interpreter.ConsoleOutput += interpreter_OnConsoleOutput;
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

        private static void interpreter_OnConsoleOutput(object sender, ConsoleOutputEventArgs e)
        {
            Console.Write(e.Output);
        }

        private static void interpreter_OnConsoleInput(object sender, ConsoleInputEventArgs e)
        {
            interpreter.Input = Console.ReadLine();
        }

        private static void interpreter_OnConsoleRead(object sender, ConsoleReadEventArgs e)
        {
            string c = Console.Read().ToString();
            Console.Write("\b");
            interpreter.Input = c;
        }
    }
}
