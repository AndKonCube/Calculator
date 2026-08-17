using System;
using System.Collections.Generic;

namespace MyApp
{
    public class Evaluator
    {
        private  List<string> _tokens = new();
        private int pos;

        public double Evaluate(string input)
        {
            _tokens = Program.Tokenize(input);
            pos = 0;

            double result = ParseExpression();
            if (pos < _tokens.Count)
                throw new FormatException($"Unexpected token: {_tokens[pos]}");
            return result;
        }
        private double ParseExpression()
        {
            double value = ParseTerm();
            while (pos < _tokens.Count && (_tokens[pos] == "+" || _tokens[pos] == "-"))
            {
                string op = _tokens[pos++];
                double right = ParseTerm();
                value = op == "+" ? value + right : value - right;
            }
            return value;
        }

        private double ParseTerm()
        {
            double value = ParseFactor();
            while (pos < _tokens.Count && (_tokens[pos] == "*" || _tokens[pos] == "/"|| _tokens[pos] == "%"))
            {
                string op = _tokens[pos++];
                double right = ParseFactor();
                value = op switch
                {
                    "*" => value * right,
                    "/" => value / right,
                    _ => value % right,
                };
            }
            return value;
        }

        private double ParseFactor()
        {
            if (pos >= _tokens.Count)
                throw new FormatException("Unexpected end of expression");

            string token = _tokens[pos++];        // advance FIRST

            if (token == "(")
            {
                double value = ParseExpression();  // whatever is inside is a full expression
                if (pos >= _tokens.Count || _tokens[pos] != ")")
                    throw new FormatException("Missing closing parenthesis");
                pos++;                            // consume the ")"
                return value;
            }

            if (double.TryParse(token, out double number))
                return number;

            throw new FormatException($"Unexpected token: {token}");
        }
    }
}