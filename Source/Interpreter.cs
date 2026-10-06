namespace Surplus
{
    using System.IO;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    static class Interpreter 
    {
        static int[] Memory = new int[250]; // Work memory
        static Stack<int> StackMemory = new Stack<int>(); // Stack
        static int X; // X register
        static int Y; // Y register
        static int A; // Address register
        static int Pointer; // The program counter (line index of execution)
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
                    Interpret(Pointer, Input[Pointer].Split(' '));
                }
            }
        }

        static void Interpret(int DebugIndex, string[] Tokens)
        {
            // Get numerics from string
            int[] Args = new int[Tokens.Length - 1];
            for(int i = 0; i < Args.Length; i++){
                try {
                    Args[i] = int.Parse(Tokens[i+1]);
                } catch {
                    ExitFlag = true;
                    Console.WriteLine("Error at " + DebugIndex.ToString() + ": Non-integer argument");
                    return;
                }
            }

            // Interpret
            try {
                switch(Tokens[0]){
                    case "ALC":
                        Memory = new int[Args[0]];
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
                    case "STA":
                        Memory[Args[0]] = A;
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
                    case "LDA":
                        A = Memory[Args[0]];
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
                    case "EQA":
                        A = Args[0];
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
                    case "TXA":
                        A = X;
                        Pointer++;
                        break;
                    case "TYX":
                        X = Y;
                        Pointer++;
                        break;
                    case "TAX":
                        X = A;
                        Pointer++;
                        break;
                    case "BEQ":
                        if(X == 0){
                            Pointer = A;
                        } else {
                            Pointer++;
                        }
                        break;
                    case "BNE":
                        if(X != 0){
                            Pointer = A;
                        } else {
                            Pointer++;
                        }
                        break;
                    case "BCY":
                        if(X > Y){
                            Pointer = A;
                        } else {
                            Pointer++;
                        }
                        break;
                    case "JMP":
                        Pointer = A;
                        break;
                    case "PSX":
                        StackMemory.Push(X);
                        Pointer++;
                        break;
                    case "PSY":
                        StackMemory.Push(Y);
                        Pointer++;
                        break;
                    case "PSA":
                        StackMemory.Push(A);
                        Pointer++;
                        break;
                    case "PLX":
                        X = StackMemory.Pop();
                        Pointer++;
                        break;
                    case "PLY":
                        Y = StackMemory.Pop();
                        Pointer++;
                        break;
                    case "PLA":
                        A = StackMemory.Pop();
                        Pointer++;
                        break;
                    case "NOT":
                        X = ~X;
                        Pointer++;
                        break;
                    case "AND":
                        X = X & Y;
                        Pointer++;
                        break;
                    case "ORA":
                        X = X | Y;
                        Pointer++;
                        break;
                    case "XOR":
                        X = X ^ Y;
                        Pointer++;
                        break;
                    case "ASL":
                        X = X << 1;
                        Pointer++;
                        break;
                    case "ASR":
                        X = X >> 1;
                        Pointer++;
                        break;
                    case "CWX":
                        Console.Write(X.ToString());
                        Pointer++;
                        break;
                    case "CAX":
                        Console.Write(System.Text.Encoding.ASCII.GetString(new byte[1]{(byte)X}));
                        Pointer++;
                        break;
                    case "CWN":
                        Console.Write('\n');
                        Pointer++;
                        break;
                    case "CCL":
                        Console.Clear();
                        Pointer++;
                        break;
                    default:
                        ExitFlag = true;
                        Console.WriteLine("Error at " + DebugIndex.ToString() + ": Unknown operation");
                        break;
                }
            } catch {
                ExitFlag = true;
                Console.WriteLine("Error at " + DebugIndex.ToString() + ": Illegal operation");
            }
        }
    }
}