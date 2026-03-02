# Compiler

## Declarations

### namespace

Declares a namespace

Example of syntax:
```
namespace example
```
Requires an ```end``` statement to close the body.

### class

Declares a class

Supports modifiers

Example of syntax:
```
class example
```
Requires an ```end``` statement to close the body.

### function

Declares a function

Supports modifiers

Example of syntax:
```
class example
```
Requires an ```end``` statement to close the body.

### declare

Declares a variable or a collection

Supports modifiers

Example of syntax 1:
```
declare byte example
```
Example of syntax 2:
```
public static declare bytelist example
```

## Variable Management

### set

Sets the value of a variable

Example of syntax 1:
```
set example 12
```
Example of syntax 2:
```
set example foo
```
(Sets the value of ```example``` to the value of ```foo```)

### setall

Sets every element in an array

Example of syntax:
```
setall example 12 5 26
```

### append

Appends an element to the end of a list

Example of syntax:
```
append example 12
```

### appendall

Appends all the elements of an array or list to the end of a list

Example of syntax:
```
appendall example foo
```
(Appends the values of ```foo``` to  ```example```)

### remove

Removes an element from a specified index in a list

Example of syntax:
```
remove example 0
```

### tobytes

Converts a byte list into a byte array

Example of syntax:
```
tobytes example foo
```
(Converts ```example``` into a byte array and outputs in  ```foo```, ```foo``` must be declared beforehand)

### tobytelist

Converts a byte array into a byte list

Example of syntax:
```
tobytelist example foo
```
(Converts ```example``` into a byte list and outputs in  ```foo```, ```foo``` must be declared beforehand)

## General Instructions

### end

Closes a body

Example of syntax:
```
namespace example
    class foo
    end
end
```

### if

Executes if the specified condition is true

Example of syntax:
```
if example equals 12
```
Requires an ```end``` statement to close the body.

Requires a comparison operator for the condition

### ifnot

Executes if the specified condition is false

Example of syntax:
```
ifnot example greater 12
```
Requires an ```end``` statement to close the body.

Requires a comparison operator for the condition

### if, else

Executes if the if statement did not execute

Example of syntax:
```
if example equal 12
    print 1
else
    print 0
end
```
Requires an ```end``` statement to close the body.

### while

Executes once, and loops until specified condition is false

Example of syntax:
```
while example less 12
```
Requires an ```end``` statement to close the body.

Requires a comparison operator for the condition

### whilenot

Executes once, and loops until specified condition is true

Example of syntax:
```
whilenot example equals 12
```
Requires an ```end``` statement to close the body.

Requires a comparison operator for the condition

### switch, case

Executes specific code for specific values of a variable

Example of syntax:
```
switch example
    case 0
        print 10
    case 1
        print 4
    case 2
        print 17
end
```
Requires an ```end``` statement to close the body.

### call

Calls a function with specified arguments

Example of syntax 1:
```
call example
```
Example of syntax 2:
```
call example 2 foo
```

## Command-line Instructions

### printtext

Writes a byte array into the console as ASCII code text

Example of syntax:
```
setall example 72 101 108 108 111 32 119 111 114 108 100 33
printtext example
```
(Outputs "Hello world!")

### print

Writes a value into the console
Example of syntax:
```
set example 72
print example
```
(Outputs "72")

## Arithmetic instructions

### add

Adds a value into a variable

Example of syntax:
```
add example 12
```

### substract

Substracts a value from a variable

Example of syntax:
```
substract example 12
```

### multiply

Multiplies a variable with a value

Example of syntax:
```
multiply example 2
```

### divide

Divides a variable by a value

Example of syntax:
```
divide example 2
```

### modulo

Sets a variable to its remainder with a specified modulo

Example of syntax:
```
modulo example 2
```

## Datatypes

### byte

8-bit unsigned integer, which ranges from 0 to 255 (inclusive)

### bytes

Array of bytes

### bytelist

List of bytes

## Modifiers

### public

Defines the scope of a declared object as public

Example of syntax:
```
public static class example
```

### static

Defines a declared object as static i.e. non-instantiable

Example of syntax:
```
public static function example
```

## Comparison Operators

### equals

Is true if the values are equal

Example of syntax:
```
example equals 12
```

### greater

Is true if the former value is greater than the latter value

Example of syntax:
```
example greater 12
```

### less

Is true if the former value is less than the latter value

Example of syntax:
```
example less 12
```