using Lexer.LexicalAnalysis;
using System.Collections.Generic;

namespace Lexer.SyntaxAnalysis
{
    public interface INodeParser
    {
        ParseResult Parse(IEnumerator<Token> tokens, SyntaxParser parser);
    }
}
