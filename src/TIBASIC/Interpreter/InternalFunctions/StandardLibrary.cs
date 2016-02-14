using System;
using System.Collections.Generic;

namespace TIBASIC.Interpreter
{
    public class StandardLibrary : ILibrary
    {
        private static Random random = new Random();
        public Dictionary<string, InternalFunction> GetFunctions()
        {
            return new Dictionary<string, InternalFunction>()
            {
                { "sin", new InternalFunction(sin) },
                { "cos", new InternalFunction(cos) },
                { "tan", new InternalFunction(tan) },
                { "arcsin", new InternalFunction(arcsin) },
                { "arccos", new InternalFunction(arccos) },
                { "arctan", new InternalFunction(arctan) },
                { "log", new InternalFunction(log) },
                { "Ln", new InternalFunction(Ln) },
                { "sqrt", new InternalFunction(sqrt) },
                { "randInt", new InternalFunction(randInt) }
            };
        }

        private object sin(object[] args)
        {
            return Math.Sin(Convert.ToDouble(args[0]));
        }
        private object cos(object[] args)
        {
            return Math.Cos(Convert.ToDouble(args[0]));
        }
        private object tan(object[] args)
        {
            return Math.Tan(Convert.ToDouble(args[0]));
        }
        private object arcsin(object[] args)
        {
            return Math.Asin(Convert.ToDouble(args[0]));
        }
        private object arccos(object[] args)
        {
            return Math.Acos(Convert.ToDouble(args[0]));
        }
        private object arctan(object[] args)
        {
            return Math.Atan(Convert.ToDouble(args[0]));
        }
        private object log(object[] args)
        {
            return Math.Log(Convert.ToDouble(args[0]));
        }
        private object Ln(object[] args)
        {
            return Math.Log10(Convert.ToDouble(args[0]));
        }
        private object sqrt(object[] args)
        {
            return Math.Sqrt(Convert.ToDouble(args[0]));
        }
        private object randInt(object[] args)
        {
            return random.Next(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]));
        }
    }
}