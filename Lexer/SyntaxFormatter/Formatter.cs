using Lexer.SyntaxAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lexer.SyntaxFormatter
{
    public class Formatter
    {
        public Dictionary<SyntaxNodeType, ISyntaxNodeFormatter> formatters = new Dictionary<SyntaxNodeType, SyntaxFormatter.ISyntaxNodeFormatter>();

        public Formatter()
        {
            formatters.Add(SyntaxNodeType.Analyzer, new AnalyzerFormatter());
            formatters.Add(SyntaxNodeType.CompilationUnit, new CompilationUnitFormatter());
            formatters.Add(SyntaxNodeType.ParameterList, new ParameterListFormatter());
            formatters.Add(SyntaxNodeType.Statement, new StatementFormatter());
            formatters.Add(SyntaxNodeType.VariableDeclaration, new VariableDeclarationFormatter());
        }

        public void Format(StringBuilder builder, SyntaxNode node, int level = 0)
        {
            Console.WriteLine($"Formatting {node.Type}");
            formatters[node.Type].Format(builder, node, this, level + 1);
        }
    }

    public interface ISyntaxNodeFormatter
    {
        void Format(StringBuilder builder, SyntaxNode syntax, Formatter formatter, int level);
    }

    public class VariableDeclarationFormatter : ISyntaxNodeFormatter
    {
        public void Format(StringBuilder builder, SyntaxNode syntax, Formatter formatter, int level)
        {
            ;
            builder.Append($"{syntax.GetToken(LexicalAnalysis.TokenType.VariableName).Token.Lexeme.Value} = ");
            formatter.Format(builder, syntax.GetNode(SyntaxNodeType.Analyzer), level);
        }
    }

    public class CompilationUnitFormatter : ISyntaxNodeFormatter
    {
        public void Format(StringBuilder builder, SyntaxNode syntax, Formatter formatter, int level)
        {
            var statements = syntax.Nodes;

            foreach (var statement in statements)
            {
                formatter.Format(builder, statement, level);
            }
        }
    }

    public class StatementFormatter : ISyntaxNodeFormatter
    {
        public void Format(StringBuilder builder, SyntaxNode syntax, Formatter formatter, int level)
        {
            if (syntax.GetNode(SyntaxNodeType.VariableDeclaration) != null)
            {
                formatter.Format(builder, syntax.GetNode(SyntaxNodeType.VariableDeclaration), level);
                builder.AppendLine(";");
            }
            else
            {
                formatter.Format(builder, syntax.GetNode(SyntaxNodeType.Analyzer), level);
                builder.AppendLine(";");
            }
        }
    }

    public class AnalyzerFormatter : ISyntaxNodeFormatter
    {
        public void Format(StringBuilder builder, SyntaxNode syntax, Formatter formatter, int level)
        {
            builder.Append(syntax.Tokens.First(x => x.Token.Type == LexicalAnalysis.TokenType.AnalyzerReference).Token.Lexeme.Value);
            builder.Append("(");
            var parameters = syntax.Nodes.First(x => x.Type == SyntaxNodeType.ParameterList);
            formatter.Format(builder, parameters, level + 1);
            builder.Append(")");
        }
    }

    public class ParameterListFormatter : ISyntaxNodeFormatter
    {
        public void Format(StringBuilder builder, SyntaxNode syntax, Formatter formatter, int level)
        {
            if (syntax.Children.Length > 2)
            {
                builder.AppendLine();
            }

            foreach (var child in syntax.Children)
            {
                if (child is SyntaxToken t)
                {
                    if (t.Token.Type == LexicalAnalysis.TokenType.VariableName)
                    {
                        if (syntax.Children.Length <= 2)
                        {
                            builder.Append($"{t.Token.Lexeme.Value},");
                        }
                        else
                        {
                            builder.AppendLine(new string(' ', level * 2) + $"{t.Token.Lexeme.Value},");
                        }
                    }
                    if (t.Token.Type == LexicalAnalysis.TokenType.StringLiteral)
                    {
                        if (syntax.Children.Length <= 2)
                        {
                            builder.Append($"{t.Token.Lexeme.Value},");
                        }
                        else
                        {
                            builder.AppendLine(new string(' ', level * 2) + $"{t.Token.Lexeme.Value},");
                        }
                    }
                }

                if (child is SyntaxNode n && n.Type == SyntaxNodeType.Analyzer)
                {
                    builder.Append(new string(' ', level * 2));
                    formatter.Format(builder, n, level + 1);
                    builder.AppendLine(",");
                }
            }
        }
    }
}
