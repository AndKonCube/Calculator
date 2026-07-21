using System;

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
                if(input == null || input.Trim() == "quit")break;
                Console.WriteLine($"you typed: {input}");
                var parts = input.Split(' ',StringSplitOptions.RemoveEmptyEntries);
                if(parts.Length != 3 || !double.TryParse(parts[0],out double a) 
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
                System.Console.WriteLine(result);
            }

        }
    }
}