# Surplus

Surplus is an experimental programming language that is designed and developed around the constraint of only using alphanumeric characters (A-Z and 0-9).

Traditionally, languages use various special characters to signify groupings, indexing, operators and many more fundamental elements of programming. The aim of Surplus is to implement these functionalities and structures without the use of special characters.

## The Goal

The goal is to have a programming language, which can be used to recreate any existing program with:
1. Indistinguishable functionality
2. Equal processing speed
3. Equal resource requirements

In other words, Surplus should not be inherently more limited, slower or more resource heavy than other programming languages.

Surplus aims to implement object-oriented programming as simply as possible, and it is NOT a language made for the programmer's convenience. If there is a way to implement it while perserving the aforementioned properties by changing your code, the language is adequate.

For example, there is no for-loop in Surplus, since it can be recreated by using a while-loop and an index variable with equal speed and resource cost.

This is also why Surplus only uses bytes and collections of bytes as allowed datatypes.

## Compiler

The Surplus compilation uses the following pipeline:
1. Interpret .sp file
2. Translate into C#
3. Compile C# into an executable

The compiler only outputs a .cs file (steps 2 and 3). The compilation to an executable must be done using an existing C# compiler. 

### Why translate into C#?

The main reason is that I do not know assembly, and C# is what I'm familiar with.

This approach does, however, have its own benefits:

- Surplus code is easier to debug
- The Surplus compiler is easier to debug
- Surplus can be used for any program that compiles C# code

### Error handling

The Surplus compiler does NOT catch errors that a C# compiler would. This is to avoid bloating the compiler code.

This means, that just because your Surplus code can be compiled into C# code, it may not compile into an executable or have desired functionality.

## Examples

### Hello world

 ```
namespace helloworld
    static class program
        static function Main
            declare bytes sample
            setall sample 72 101 108 108 111 32 119 111 114 108 100 33
            printtext sample
        end
    end
end
 ```
