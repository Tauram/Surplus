# Compiler

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

## Variable management

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

## General instructions

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