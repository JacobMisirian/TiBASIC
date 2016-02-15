using System;

using TIBASIC.Lexer;

namespace TIBASIC.Parser
{
    public class PrgmNode: AstNode
    {
        public string PrgmPath { get; private set; }
        public PrgmNode(string prgmPath)
        {
            PrgmPath = prgmPath;
        }

        public static PrgmNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "Prgm");
            string prgmPath = parser.ExpectToken(TokenType.String).Value;

            return new PrgmNode(prgmPath);
        }
    }
}

