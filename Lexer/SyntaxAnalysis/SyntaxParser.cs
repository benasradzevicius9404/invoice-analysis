using Lexer.LexicalAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Lexer.SyntaxAnalysis
{
    public class SyntaxParser
    {
        private Dictionary<SyntaxNodeType, INodeParser> nodeParsers = new Dictionary<SyntaxNodeType, INodeParser>();

        public SyntaxParser()
        {
            nodeParsers.Add(SyntaxNodeType.Statement, new StatementParser());
            nodeParsers.Add(SyntaxNodeType.VariableDeclaration, new VariableDeclarationParser());
            nodeParsers.Add(SyntaxNodeType.ParameterList, new ParameterListParser());
            nodeParsers.Add(SyntaxNodeType.Analyzer, new AnalyzerParser());
            nodeParsers.Add(SyntaxNodeType.CompilationUnit, new CompilationUnitParser());
        }

        public ParseResult Parse(IEnumerable<Token> tokens)
        {
            var enumerator = tokens.Where(x => x.Type != TokenType.WhiteSpace).GetEnumerator();
            enumerator.MoveNext();

            return ParseNode(SyntaxNodeType.CompilationUnit, enumerator);
        }

        public ParseResult ParseNode(SyntaxNodeType nodeType, IEnumerator<Token> tokens)
        {
            return nodeParsers[nodeType].Parse(tokens, this);
        }
    }
}
