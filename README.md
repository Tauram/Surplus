# Surplus

Surplus is an experimental high-level programming language that is designed and developed around the constraint of only using alphanumeric characters (A-Z, 0-9, and whitespace characters).

Traditionally, programming languages use various special characters to signify groupings, indexing, operators and many more fundamental elements of programming. The aim of Surplus is to implement these functionalities and structures without the use of special characters.

## The Goal

The goal is to have a programming language, which can be used to recreate any existing program with:
1. Indistinguishable functionality
2. Equal processing speed
3. Equal resource requirements

In other words, Surplus should not be inherently more limited, slower or more resource heavy than other programming languages.*

Surplus aims to implement object-oriented programming with a minimalistic approach, and it is NOT a language made for the programmer's convenience. If there is a way to implement it while perserving the aforementioned properties by changing your code, the language is adequate.**

For example, there is no for-loop in Surplus, since it can be recreated by using a while-loop and an index variable with equal speed and resource cost.

<sub>*This only applies to the fully compiled program. The source code can/will take more space written in Surplus than other standard programming languages</sub>

<sub>**This rule is somewhat flexible, as Surplus is still built to be a high-level language. The same program should not be "unreasonably" more tedious to write in Surplus than in other standard programming languages.</sub>

## Compiler

The Surplus compilation uses the following pipeline:
1. Open .sp file
2. Interpret
3. Translate into C#
4. Output .cs file

The compilation to an executable must be done using an existing C# compiler. 

### Why translate into C#?

The main reason is that I do not know any low-level languages and C# is what I'm familiar with.

This approach does, however, have its own benefits:

- Surplus code is easier to debug
- The Surplus compiler is easier to debug
- Surplus can be used for any program that compiles C# code

### Error handling

The Surplus compiler does NOT catch errors that a C# compiler would. This is to avoid bloating the compiler code.

This means, that just because your Surplus code can be compiled into C# code, it may not compile into an executable or have desired functionality.

## Syntax reference

[Compiler README](/Source/README.md)

## Examples

### Hello world

```
namespace helloworld
    class program as static
        function Main as static
            call System Console WriteLine with quote Hello World! quote
        end
    end
end
```