# Compiler

## Declarations

### namespace

Declares a namespace

Requires an ```end``` statement to close the body.

Example of syntax:
```
namespace example
```

### import

Declares an assembly reference

Example of syntax:
```
import System Collections Generic
```

### class

Declares a class

Supports modifiers

Requires an ```end``` statement to close the body.

Example of syntax:
```
class example
```

### function

Declares a function with/without arguments

Supports modifiers

Requires an ```end``` statement to close the body.

Example of syntax 1:
```
function sample
```
Example of syntax 2:
```
function sample array string foo int bar
```
(declares function ```sample``` that takes a string array named ```foo``` and an int named ```bar``` as arguments)

Example of syntax 3:
```
function sample list bool foo as public static
```
(declares a public and static function ```sample``` that takes a boolean list named ```foo``` as an argument)

### declare

Declares a variable or a collection

Supports modifiers

Example of syntax 1:
```
declare int example
```
Example of syntax 2:
```
declare list string example as static
```
(declares a static string list ```example```)

## Variable Management

### set

Sets the value of a variable

Uses ```to``` keyword for seperation

Example of syntax 1:
```
set example to 12
```
(Sets the value of ```example``` to 12)

Example of syntax 2:
```
set sample to foo
```
(Sets the value of ```sample``` to the value of ```foo```)

### setindex

Sets the value of an element at a specified index of a collection

Uses ```in``` and ```to``` keywords for seperation

Example of syntax 1:
```
setindex 0 in example to 4
```
(Sets the value of ```example``` at index 0 to 4)

Example of syntax 2:
```
setindex foo in example to quote Test string quote
```
(Sets the value of ```example``` at index ```foo``` to "test string")

### getindex

Gets the value of an element at a specified index of a collection

Uses ```in``` and ```to``` keywords for seperation

Example of syntax:
```
getindex 0 in example to foo
```
(Gets the value of ```example``` at index 0 and outputs into ```foo```)

### copyarray

Copies an array to another array

Uses ```to``` keyword for seperation

Example of syntax:
```
copyarray example to foo
```
(Copies ```example``` to ```foo```)

### append

Appends an element to the end of a list

Uses ```to``` keyword for seperation

Example of syntax 1:
```
append 12 to example
```
Example of syntax 2:
```
append quote test string quote to foo
```
(Appends "test string" to ```foo```)

### remove

Removes an element at a specified index of a collection

Uses ```from``` keyword for seperation

Example of syntax:
```
remove 0 from example
```
(Removes element at index 0 from  ```example```)

### appendall

Appends all the elements of an array or list to the end of a list

Uses ```to``` keyword for seperation

Example of syntax:
```
appendall foo to example
```
(Appends the values of ```foo``` to  ```example```)

### setlength

Sets the length of an array using a specified datatype

Uses ```to``` and ```with``` keywords for seperation

Example of syntax:
```
setlength example to 10 with string
```
(Sets ```example``` to an array with 10 empty strings)

### initlist

Initializes a list using a specified datatype

Uses ```with``` keyword for seperation

Example of syntax:
```
initlist example with bool
```
(Initializes ```example``` as a boolean list)

## General Instructions

### start

Opens a body (used for defining scope)

Not required for other declarations

Example of syntax:
```
start
    set out to 1
end
```

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

Executes the body if the specified condition is true

Requires a comparison operator for the condition

Requires an ```end``` statement to close the body.

Example of syntax:
```
if example equals quote yes quote
```
(executes if the value of ```example``` is "yes")

### ifnot

Executes the body if the specified condition is false

Requires a comparison operator for the condition

Requires an ```end``` statement to close the body.

Example of syntax:
```
ifnot example greater 12
```

### else

Executes if the if statement did not execute

Requires an ```end``` statement to close the body.

Example of syntax:
```
if example equal 12
    set out to 1
else
    set out to 0
end
```

### while

Executes once, and loops until specified condition is false

Requires a comparison operator for the condition

Requires an ```end``` statement to close the body.

Example of syntax:
```
while example less 12
```

### whilenot

Executes once, and loops until specified condition is true

Requires a comparison operator for the condition

Requires an ```end``` statement to close the body.

Example of syntax:
```
whilenot example equals 12
```

### switch, case

Executes specific code for specific values of a variable

Requires an ```end``` statement to close the body.

Example of syntax:
```
switch example
    case 0
        set out to quote zero quote
    case 1
        set out to quote one quote
    case 2
        set out to quote two quote
end
```

### call

Calls a function with/without specified arguments

Uses ```with``` and ```and``` keywords for seperation

Example of syntax 1:
```
call example
```
Example of syntax 2:
```
call example with quote test string quote and foo
```
(Calls ```example``` with arguments "test string" and ```foo```)

## Arithmetic instructions

### increase

Increases the value of a variable by a specified value

Uses ```by``` keyword for seperation

Example of syntax:
```
increase example by 12
```

### decrease

Decreases the value of a variable by a specified value

Uses ```by``` keyword for seperation

Example of syntax:
```
decrease example by 12
```

### multiply

Multiplies the value of a variable by a specified value

Uses ```by``` keyword for seperation

Example of syntax:
```
multiply example by 2
```

### divide

Divides the value of a variable by a specified value

Uses ```by``` keyword for seperation

Example of syntax:
```
divide example by 2
```

### modulo

Sets the value of a variable to its remainder with a specified modulo

Uses ```by``` keyword for seperation

Example of syntax:
```
modulo example by 2
```

## Bitwise instructions

### invert

Inverts the bits of a value and outputs to a variable

Uses ```to``` keyword for seperation

Example of syntax:
```
invert example to foo
```

### lshift

Shifts the bits of a value to the left and outputs to a variable

Uses ```to``` keyword for seperation

Example of syntax:
```
lshift example to foo
```

### rshift

Shifts the bits of a value to the right while preserving the sign and outputs to a variable

Uses ```to``` keyword for seperation

Example of syntax:
```
rshift example to foo
```

### urshift

Shifts the bits of a value to the right filling with zeros from the left (unsigned shift) and outputs to a variable

Uses ```to``` keyword for seperation

Example of syntax:
```
urshift example to foo
```

### and

Applies a bitwise AND-operator with a specified value to a variable

Uses ```to``` keyword for seperation

Example of syntax:
```
and example to foo
```
(Applies ```example``` with the AND-operator onto ```foo```)

### or

Applies a bitwise OR-operator with a specified value to a variable

Uses ```to``` keyword for seperation

Example of syntax:
```
or example to foo
```
(Applies ```example``` with the OR-operator onto ```foo```)

### xor

Applies a bitwise XOR-operator with a specified value to a variable

Uses ```to``` keyword for seperation

Example of syntax:
```
xor example to foo
```
(Applies ```example``` with the XOR-operator onto ```foo```)

## Collection datatypes

### array

Array of a specified datatype

Example of syntax:
```
declare array string example
```

### list

List of a specified datatype

Example of syntax:
```
declare list int example
```

## Modifiers

Used at the end of declarations after ```as``` keyword

Example of syntax:
```
class example as public static
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

## Other keywords

### quote

Used to specify a literal string value

Example of syntax:
```
set example to quote Hello World! quote
```
(sets the value of ```example``` to the string "Hello World!" (without quotes))