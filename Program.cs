using System;
using System.Collections.Generic;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("> ");
                string? input = Console.ReadLine();
                if (input == null || input.Trim() == "quit") break;
                if (input.Trim().Length == 0) continue;

                try
                {
                    double result = new Evaluator().Evaluate(input);
                    Console.WriteLine(result);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (DivideByZeroException dvex)
                {
                    Console.WriteLine($"Error: {dvex.Message}");
                }
            }
        }

        public static List<string> Tokenize(string s)
        {
            var tokens = new List<string>();
            if (string.IsNullOrEmpty(s))
            {
                return tokens;
            }

            int i = 0;
            while (i < s.Length)
            {
                char c = s[i];

                if (char.IsWhiteSpace(c))
                {
                    i++;
                }
                else if (char.IsDigit(c) || c == '.')
                {
                    int start = i;
                    while (i < s.Length && (char.IsDigit(s[i]) || s[i] == '.'))
                    {
                        i++;
                    }
                    tokens.Add(s.Substring(start, i - start));
                }
                else if (c == '+' || c == '-' || c == '*' || c == '/' || c == '%' || c == '(' || c == ')')
                {
                    tokens.Add(c.ToString());
                    i++;
                }
                else if (c == --c)
                {
                    tokens.Add(c.ToString());
                    i++;
                }
                else
                {
                    throw new FormatException($"Unexpected character: {c}");
                }
            }
            return tokens;
        }
    }
}
