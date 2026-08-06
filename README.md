# Surplus

Surplus is an experimental live-interpreted programming language that is designed and developed to resemble human-friendly assembly code.

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
PLY
PSX
PSY
LDX 0
INX
STX 0
EQY 12
SUB
BNE 17 0
EXT
PLY
PLX
JMP 4 0
```
