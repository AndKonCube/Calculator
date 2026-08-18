using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace MyApp
{
    public class Evaluator
    {
        private List<string> _tokens = new();
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
            while (pos < _tokens.Count && (_tokens[pos] == "+" || _tokens[pos] == "-" || _tokens[pos] == "--"))
            {
                string op = _tokens[pos++];
                double right = ParseTerm();
                value = op == "+" ? value + right : value - right;
            }
            return value;
        }

        private double ParseTerm()
        {
            double value = ParseUnary();
            while (pos < _tokens.Count && (_tokens[pos] == "*" || _tokens[pos] == "/" || _tokens[pos] == "%"))
            {
                string op = _tokens[pos++];
                double right = ParseUnary();

                if (right == 0 && (op == "/" || op == "%")) throw new DivideByZeroException();
                value = op switch
                {
                    "*" => value * right,
                    "/" => value / right,
                    "%" => value % right,
                };
            }
            return value;
        }

        private double ParseFactor()
        {
            if (pos >= _tokens.Count)
                throw new FormatException("Unexpected end of expression");

            string token = _tokens[pos++];

            if (token == "(")
            {
                double value = ParseExpression();
                if (pos >= _tokens.Count || _tokens[pos] != ")")
                    throw new FormatException("Missing closing parenthesis");
                pos++;
                return value;
            }

            if (double.TryParse(token, out double number))
                return number;

            throw new FormatException($"Unexpected token: {token}");
        }
        private double ParseUnary()
        {
            if (pos < _tokens.Count && (_tokens[pos] == "-" || _tokens[pos] == "+"))
            {
                string op = _tokens[pos++];
                double value = ParseUnary();
                return op == "-" ? -value : value;
            }
            return ParseFactor();
        }

        private double ParseExponant()
        {
            return ParseExponant();
        }
    }
}

