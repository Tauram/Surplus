# Surplus

Surplus is an experimental live-interpreted programming language that is designed and developed to resemble human-friendly assembly code. With its small set of instructions, it is extremely easy to learn but can still be used to compute any possible calculation or program thanks to being [Turing complete](https://en.wikipedia.org/wiki/Turing_completeness#Formal_definitions).

## Memory structure

The Surplus interpreter runs the program in a closed environment and only has access to the following sections of virtual memory:
1. Work memory - An array of bytes used as work memory. The default size is 1000 bytes, but it can be cleared or resized using the ```ALC``` instruction.
2. Stack - A stack of bytes seperate from the work memory. The size of the stack is dynamically allocated with elements pushed onto it.
3. X register - The primary byte used for computation
4. Y register - The secondary byte used for computation
5. Pointer - The address (line index) of execution as a signed 32-bit integer. The first line of a program is at index 0.
6. Return address - The line index to return to from a subroutine as a signed 32-bit integer, automatically set and cannot be modified.
7. Exit flag - A boolean that tells the interpreter to stop execution when true. Set with the ```EXT``` instruction.

## Instructions

A full documentation of every available instruction can be found [here](Source/README.md).

## Examples

### Fibonacci

Calculates and prints numbers in the Fibonacci sequence using the registers, stack, and one other memory address, limited to 12 numbers starting from the second 1 in this case.
```
ALC 1
EQX 1
EQY 0
STY 0
PSX
SUM
CWX
CWN
PLY
PSX
PSY
LDX 0
INX
STX 0
EQY 12
SUB
BNE 18
EXT
PLY
PLX
JMP 4
```
