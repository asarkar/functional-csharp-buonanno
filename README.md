My solutions to the exercises from the book [Functional Programming in C#](https://www.amazon.com/Functional-Programming-Second-Enrico-Buonanno-dp-1617299820/dp/1617299820/ref=dp_ob_title_bk), 2nd ed.

[![](https://github.com/asarkar/functional-csharp-buonanno/workflows/CI/badge.svg)](https://github.com/asarkar/functional-csharp-buonanno/actions)

Official GitHub repo: https://github.com/la-yumba/functional-csharp-code-2/tree/master

## Contents

### Part 1. Foundations
2. [Thinking in functions](src/Ch02)
3. [Why function purity matters](src/Ch03)

### Part 2. Core techniques
4. Designing function signatures and types
5. [Modeling the possible absence of data](src/Ch05)
6. [Patterns in functional programming](src/Ch06)
7. [Designing programs with function composition](src/Ch07)
8. [Functional error handling](src/Ch08)
9. [Structuring an application with functions](src/Ch09)

## Development

Deleting `bin` and `obj` directories:
```
find . -type d \( -name bin -o -name obj \) -exec rm -rf {} +
```

Running tests:
```
./.github/run.sh <directory>
```

## License

Released under [Apache License v2.0](LICENSE).