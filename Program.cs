using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("> ");
                string? input = Console.ReadLine();
                Tokenize(input);
                if (input == null || input.Trim() == "quit") break;
                Console.WriteLine($"you typed: {input}");
                //if()
                /*var parts = input.Split(' ',StringSplitOptions.RemoveEmptyEntries);
                //if(parts.Length != 3 || !double.TryParse(parts[0],out double a) 
                                     || !double.TryParse(parts[2], out double b))
                {
                    System.Console.WriteLine("Format: nu number op number");continue;
                }
                double result = parts[1] switch
                {
                    "+" => a + b,
                    "-" => a - b,
                    "*" => a * b,
                    "/" => b == 0 ? throw new DivideByZeroException() : a / b,
                    _ => throw new ArgumentException($"Uknown operator : {parts[1]}")
                };
                System.Console.WriteLine(result);*/
            }
        }

        public static List<string> Tokenize(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return new List<string>();
            }
            foreach (char token in s)
            {
                System.Console.WriteLine(token);
            }
            return s.Split(' ').ToList();
        }
    }
}