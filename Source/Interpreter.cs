namespace Surplus
{
    using System.IO;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    static class Interpreter 
    {
        static byte[] Memory = new byte[1000]; // Work memory
        static byte X; // X register
        static byte Y; // Y register
        static Stack<byte> StackMemory = new Stack<byte>(); // Stack
        static ushort Pointer; // The line index of execution
        static ushort Return; // Subroutine return address
        static bool ExitFlag; // Set true to exit

        static void Main(string[] Args)
        {
            // Load file and prepare
            string[] Input = File.ReadAllLines(Args[0]);
            ExitFlag = false;
            Pointer = 0;

            // Execute until exit or end of file
            while(!ExitFlag && Pointer < Input.Length){
                if(Pointer < Input.Length){
                    Interpret(Input[Pointer].Split(' '));
                }
            }
        }

        static void Interpret(string[] Tokens)
        {
            // Get numerics from string
            byte[] Args = new byte[Tokens.Length - 1];
            for(int i = 0; i < Args.Length; i++){
                Args[i] = byte.Parse(Tokens[i+1]);
            }

            // Interpret
            switch(Tokens[0]){
                case "ALC":
                    Memory = new byte[Args[0]];
                    Pointer++;
                    break;
                case "EXT":
                    ExitFlag = true;
                    break;
                case "STX":
                    Memory[Args[0]] = X;
                    Pointer++;
                    break;
                case "STY":
                    Memory[Args[0]] = Y;
                    Pointer++;
                    break;
                case "LDX":
                    X = Memory[Args[0]];
                    Pointer++;
                    break;
                case "LDY":
                    Y = Memory[Args[0]];
                    Pointer++;
                    break;
                case "EQX":
                    X = Args[0];
                    Pointer++;
                    break;
                case "EQY":
                    Y = Args[0];
                    Pointer++;
                    break;
                case "INX":
                    X++;
                    Pointer++;
                    break;
                case "INY":
                    Y++;
                    Pointer++;
                    break;
                case "DEX":
                    X--;
                    Pointer++;
                    break;
                case "DEY":
                    Y--;
                    Pointer++;
                    break;
                case "SUM":
                    X += Y;
                    Pointer++;
                    break;
                case "SUB":
                    X -= Y;
                    Pointer++;
                    break;
                case "TXY":
                    Y = X;
                    Pointer++;
                    break;
                case "TYX":
                    X = Y;
                    Pointer++;
                    break;
                case "BEQ":
                    if(X == 0){
                        Pointer = BitConverter.ToUInt16(Args, 0);
                    }
                    break;
                case "BNE":
                    if(X != 0){
                        Pointer = BitConverter.ToUInt16(Args, 0);
                    }
                    break;
                case "BCY":
                    if(X > Y){
                        Pointer = BitConverter.ToUInt16(Args, 0);
                    }
                    break;
                case "JMP":
                    Pointer = BitConverter.ToUInt16(Args, 0);
                    break;
                case "JSR":
                    Return = Pointer;
                    Pointer = BitConverter.ToUInt16(Args, 0);
                    break;
                case "RTS":
                    Pointer = Return;
                    Pointer++;
                    break;
                case "PSX":
                    StackMemory.Push(X);
                    break;
                case "PSY":
                    StackMemory.Push(Y);
                    break;
                case "PLX":
                    X = StackMemory.Pop();
                    break;
                case "PLY":
                    Y = StackMemory.Pop();
                    break;
                default:
                    ExitFlag = true;
                    break;
            }
        }
    }
}