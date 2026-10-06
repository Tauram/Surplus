# Surplus

Surplus is an experimental live-interpreted programming language that is designed and developed to resemble human-friendly assembly code. With its small set of instructions, it is extremely easy to learn but can still be used to compute any possible calculation or program thanks to being [Turing complete](https://en.wikipedia.org/wiki/Turing_completeness#Formal_definitions).

## Memory structure

Surplus is designed to run on 32-bit systems, so all of its virtual memory types use signed 32-bit integers as their unit of transfer. In this documentation, we will refer to a signed 32-bit integer with the word unit. The Surplus interpreter runs the program in a closed environment and only has access to the following sections of virtual memory:
1. Work memory - An array of units used as work memory. The default size is 250 units (=1 KB), but it can be cleared or resized using the ```ALC``` instruction.
2. Stack - A stack of units seperate from the work memory. The size of the stack is dynamically allocated with elements pushed onto it.
3. X register - The primary unit used for computation
4. Y register - The secondary unit used for computation
5. A register - A unit used as an address for branching and jumping. Addresses are equivalent to the code's line indices, with the first line at address 0.
6. Pointer - The program counter (address of execution) as a unit
7. Exit flag - A boolean that tells the interpreter to stop execution when true. Set with the ```EXT``` instruction.

## Instructions

A full documentation of every available instruction can be found [here](Source/README.md).

## Examples

### Fibonacci

Calculates and prints numbers in the Fibonacci sequence using the registers, stack, and one other memory address, limited to 20 numbers starting from the first 1 in this case.
```
ALC 0
EQX 1
PSX
PLY
TYA
CWX
CWN
SUM
TXY
TAX
PSY
EQY 6765
EQA 16
BCY
EQA 3
JMP
EXT
```
