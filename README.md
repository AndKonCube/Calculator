# Calculator

A small command-line arithmetic calculator written in C# (.NET 10). It reads expressions from
standard input, parses them with a hand-written recursive-descent parser, and prints the result.

## Features

- Four basic operators: `+`, `-`, `*`, `/`
- Parentheses for grouping, nested to any depth
- Correct operator precedence (`*` and `/` bind tighter than `+` and `-`)
- Decimal numbers (`3.5 * 2`)
- Whitespace-insensitive input
- Clear error messages for malformed input

## Requirements

- [.NET SDK 10.0](https://dotnet.microsoft.com/download) or newer

## Getting started

```bash
git clone https://github.com/AndKonCube/Calculator.git
cd Calculator
dotnet run
```

> **Note:** `dotnet run` currently fails at the repo root. `Calculator.csproj` sits in the same
> directory as `Calculator.Tests/`, so the SDK's default source glob pulls the test project's files
> (and its `obj/` output) into the console app and the build errors out with `CS0579` /
> `CS0246: Xunit`. See [Known limitations](#known-limitations) for the fix.

## Usage

The program starts a REPL. Type an expression and press Enter; type `quit` (or send EOF) to exit.

```
> 1 + 2 * 3
7
> (1 + 2) * 3
9
> 10 / 4
2.5
> 2 * (3 + (4 - 1))
12
> 1 +
Error: Unexpected end of expression
> 2 $ 3
Error: Unexpected character: $
> quit
```

## How it works

The evaluator is a two-stage pipeline:

1. **Tokenizer** — [`Program.Tokenize`](Program.cs#L29) scans the input string one character at a
   time and produces a flat list of tokens: numbers, operators, and parentheses. Anything else
   raises a `FormatException`.
2. **Parser / evaluator** — [`Evaluator`](Evaluator.cs) walks that token list with three mutually
   recursive methods that encode the grammar and, with it, operator precedence:

   ```
   Expression := Term   (('+' | '-') Term)*
   Term       := Factor (('*' | '/' |'%') Factor)*
   Unary   := ('+' | '-') unary | factor
   Factor     := Number | '(' Expression ')'
   ```

   Each method evaluates as it parses, so no intermediate syntax tree is built — the result comes
   straight back as a `double`.

Errors are surfaced as `FormatException` and caught in the REPL loop, so a bad expression prints a
message instead of crashing the session.

## Project layout

```
Calculator.csproj          Console application (net10.0)
Program.cs                 REPL entry point + tokenizer
Evaluator.cs               Recursive-descent parser and evaluator
Calculator.Tests/          xUnit test project (net10.0)
```

## Tests

```bash
dotnet test Calculator.Tests
```

The xUnit project is scaffolded but not yet wired up: `Calculator.Tests/UnitTest1.cs` is empty and
the test project has no `ProjectReference` to `Calculator.csproj`. To start writing tests against
`Evaluator`, add the reference first:

```bash
dotnet add Calculator.Tests/Calculator.Tests.csproj reference Calculator.csproj
```

## Known limitations

- **The root project build is broken.** `Calculator.csproj` implicitly compiles every `.cs` file
  beneath its directory, which includes `Calculator.Tests/`. Fix it by excluding the test folder:

  ```xml
  <ItemGroup>
    <Compile Remove="Calculator.Tests/**" />
  </ItemGroup>
  ```

  Moving the console app into its own `src/` folder alongside a solution file would solve it more
  cleanly.
- No unary minus — `-5` and `3 * -2` are rejected as parse errors (`Unexpected token: -`)
- No exponentiation, functions, or constants
- Division by zero follows IEEE 754 double semantics and prints `∞` / `NaN` rather than erroring
- Malformed numbers such as `1.2.3` are collected as a single token and then rejected by the parser
- Results are printed with the current culture's number formatting
- `Tokenize` lives on `Program`, so the evaluator depends on the entry-point class rather than the
  other way round
