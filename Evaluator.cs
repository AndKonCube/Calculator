using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;

namespace MyApp
{
    public class Evaluator
    {
        private static List<string> _tokens = new();
        private int pos;

        public double Evaluate(string input)
        {
            _tokens = Program.Tokenize(input);
            pos = 0;
            double value = ParseTerm();


            return ParseExression();
        }
        private double ParseExression()
        {
            double value =0;
            while (pos<_tokens.Count && (_tokens[pos]== "+"|| _tokens[pos] == "-" ))
            {
                string op = _tokens[pos++];
                double right = ParseTerm();
                value = op =="+" ?value + right: value - right;
            }
            return value;
        }
        
        private double ParseTerm()
        {
            double op = 0;
            return op;
        }
    }
}