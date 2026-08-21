# Instructions

## ALC
Resizes the work memory to a specified size in bytes. This clears the memory to all 0s.

Arguments:
1. int - the new size of the work memory as number of bytes

## EXT
Sets the exit flag of the program to true and stops execution.

## STX
Stores the value of the X register into a specified address in work memory.

Arguments:
1. int - the address to store the value into

## STY
Stores the value of the Y register into a specified address in work memory.

Arguments:
1. int - the address to store the value into

## LDX
Loads the value of a specified address in work memory into the X register.

Arguments:
1. int - the address to load the value from
 
## LDY
Loads the value of a specified address in work memory into the Y register.

Arguments:
1. int - the address to load the value from

## EQX
Sets the value of the X register.

Arguments:
1. byte - the new value

## EQY
Sets the value of the Y register.

Arguments:
1. byte - the new value

## INX
Increases the value of the X register by one.

## INY
Increases the value of the Y register by one.

## DEX
Decreases the value of the X register by one.

## DEY
Decreases the value of the Y register by one.

## SUM
Adds the value of the Y register to the X register.

## SUB
Substracts the value of the Y register from the X register.

## TXY
Transfers the value of the X register into the Y register.

## TYX
Transfers the value of the Y register into the X register.

## BEQ
Jumps execution to a specified address if the X register is equal to 0.

Arguments:
1. int - address to jump to as a zero-based line index

## BNE
Jumps execution to a specified address if the X register is not equal to 0.

Arguments:
1. int - address to jump to as a zero-based line index

## BCY
Jumps execution to a specified address if the value of the X register is greater than the value of the Y register.

Arguments:
1. int - address to jump to as a zero-based line index

## JMP
Jumps execution to a specified address.

Arguments:
1. int - address to jump to as a zero-based line index

## JSR
Jumps execution to a specified address as a subroutine and sets the return address to the next instruction.

Arguments:
1. int - address to jump to as a zero-based line index

## RTS
Returns execution to the return address.

## PSX
Pushes the value of the X register onto the stack.

## PSY
Pushes the value of the Y register onto the stack.

## PLX
Pulls the topmost element from the stack into the X register. The element is then removed from the stack.

## PLY
Pulls the topmost element from the stack into the Y register. The element is then removed from the stack.

## NOT
Inverts the bits of the X register.

## AND
Performs a bitwise AND using the X and Y registers. The resulting value is set to the X register.

## ORA
Performs a bitwise OR using the X and Y registers. The resulting value is set to the X register.

## XOR
Performs a bitwise XOR using the X and Y registers. The resulting value is set to the X register.

## ASL
Performs an arithmetic left shift to the X register.

## LSR
Performs a logical right shift to the X register.

## CWX
Writes the value of the X register to the console as a number.

## CAX
Writes the value of the X register to the console as an ASCII character.

## CWN
Writes a newline to the console.

## CCL
Clears the console.
