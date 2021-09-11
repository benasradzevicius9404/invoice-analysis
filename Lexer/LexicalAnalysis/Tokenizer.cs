using System;
using System.Collections.Generic;
using System.Text;

namespace Lexer.LexicalAnalysis
{
    public class Tokenizer
    {
        public IEnumerable<Token> Tokenize(Grammer grammer, string sourceCode)
        {
            if (string.IsNullOrEmpty(sourceCode))
                throw new ArgumentNullException("sourceCode");

            int i = 0;
            int length = sourceCode.Length;

            var builder = new StringBuilder(sourceCode);

            string str = sourceCode;

            while (i < length)
            {
                foreach (var rule in grammer.Rules)
                {
                    var match = rule.Pattern.Match(str);

                    if (match.Success)
                    {
                        if (match.Length == 0)
                        {
                            throw new Exception(string.Format("Regex matche length is zero. This can lead to infinite loop. Please modify your regex {0} for {1} so that it can't matche character of zero length", rule.Pattern, rule.TokenType));
                        }

                        yield return new Token(rule.TokenType, new Lexeme(i, match.Length, match.Value));
                        i += match.Length;

                        builder.Remove(0, match.Length);
                        break;
                    }
                }

                str = builder.ToString();
            }

            yield return new Token(TokenType.End, null);
        }
    }
}
