namespace Surplus
{
    using System.IO;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    static class Compiler {

        static string[] AllowedPrefixes = new string[]{"public", "static"};
        static bool StopFlag;

        static void Main(string[] Args)
        {
            // GET INPUT
            Console.WriteLine("INFO: Load file " + Args[0]);
            if(!Args[0].EndsWith(".sp")){
                PrintError(0, 0);
                return;
            }
            string[] Input = File.ReadAllLines(Args[0]);
            List<string> Output = new List<string>();
            
            // INTERPRETATION LOOP
            StopFlag = false;
            Console.WriteLine("INFO: Interpretation start");
            for(int i = 0; i < Input.Length; i++){
                string Out = Interpret(Input[i], i);
                if(Out != "ERROR" && !StopFlag){
                    Output.Add(Out);
                } else {
                    return;
                }
            }

            // CS OUTPUT
            Console.WriteLine("INFO: Output CS");
            string OutPath = Args[0].Substring(0, Args[0].Length - 2) + "cs";
            File.WriteAllLines(OutPath, Output.ToArray());
        }
        
        // INSTRUCTION INTERPRETER
        static string Interpret(string Line, int Index){
            Line = Line.TrimStart();
            Console.WriteLine("INFO: Interpreting line " + Index);
            List<string> Tokens = Line.Split(' ').ToList();
            string Prefix = "";
            if(AllowedPrefixes.Contains(Tokens[0])){
                while(AllowedPrefixes.Contains(Tokens[0])){
                    Prefix += Tokens[0] + " ";
                    Tokens.RemoveAt(0);
                }
            }
            string ReadReference = "";
            if(Tokens[0] == "readfrom"){
                while(Tokens[0] != "execute"){
                    if(Tokens.Count > 1){
                        Tokens.RemoveAt(0);
                        if(Tokens[0] != "execute"){
                            ReadReference += Tokens[0] + ".";
                        }
                    } else {
                        PrintError(1, Index);
                        return "ERROR";
                    }
                }
                Tokens.RemoveAt(0);
            }
            string WriteReference = "";
            if(Tokens[0] == "writeto"){
                while(Tokens[0] != "execute"){
                    if(Tokens.Count > 1){
                        Tokens.RemoveAt(0);
                        if(Tokens[0] != "execute"){
                            WriteReference += Tokens[0] + ".";
                        }
                    } else {
                        PrintError(1, Index);
                        return "ERROR";
                    }
                }
                Tokens.RemoveAt(0);
            }
            try {
                switch(Tokens[0]){
                    // DECLARATIONS
                    case "namespace":
                        return Line + " {";
                    case "import":
                        return "using " + String.Join(".", TokenRange(Tokens, 1, Tokens.Count - 1).ToArray()) + ";";
                    case "class":
                        return Prefix + "class " + Tokens[1] + " {";
                    case "function":
                        return Prefix + "void " + Tokens[1] + "(" + InterpretDatatypes(TokenRange(Tokens, 2, Tokens.Count - 2), true, Index) + "){";
                    case "declare":
                        return Prefix + InterpretDatatypes(TokenRange(Tokens, 1, Tokens.Count - 1), false, Index) + ";";

                    // VARIABLE MANAGEMENT
                    case "set":
                        return WriteReference + Tokens[1] + " = " + ReadReference + Tokens[2] + ";";
                    case "setvals":
                        return WriteReference + Tokens[1] + " = new byte[]{" + string.Join(", ", TokenRange(Tokens, 2, Tokens.Count - 2).ToArray()) + "};";
                    case "setclone":
                        return "System.Array.Copy(" + ReadReference + Tokens[2] + ", " + WriteReference + Tokens[1] + ", " + ReadReference + Tokens[2] + ".Length);";
                    case "append":
                        return WriteReference + Tokens[1] + ".Add(" + ReadReference + Tokens[2] + ");";
                    case "remove":
                        return WriteReference + Tokens[1] + ".RemoveAt(" + ReadReference + Tokens[2] + ");";
                    case "appendall":
                        return WriteReference + Tokens[1] + ".AddRange(" + ReadReference + Tokens[2] + ");";
                    case "tobytes":
                        return WriteReference + Tokens[2] + " = " + ReadReference + Tokens[1] + ".ToArray();";
                    case "tobytelist":
                        return WriteReference + Tokens[2] + " = " + ReadReference + Tokens[1] + ".ToList();";
                    
                    // GENERAL INSTRUCTIONS
                    case "start":
                        return "{";
                    case "end":
                        return "}";
                    case "if":
                        return "if(" + InterpretCondition(TokenRange(Tokens, 1, Tokens.Count - 1), Index) + "){";
                    case "ifnot":
                        return "if(!(" + InterpretCondition(TokenRange(Tokens, 1, Tokens.Count - 1), Index) + ")){";
                    case "else":
                        return "} else {";
                    case "while":
                        return "while(" + InterpretCondition(TokenRange(Tokens, 1, Tokens.Count - 1), Index) + "){";
                    case "whilenot":
                        return "while(!(" + InterpretCondition(TokenRange(Tokens, 1, Tokens.Count - 1), Index) + ")){";
                    case "switch":
                        return "switch(" + Tokens[1] + "){";
                    case "case":
                        return "case " + Tokens[1] + ":";
                    case "call":
                        return WriteReference + ReadReference + Tokens[1] + "(" + InterpretDatatypes(TokenRange(Tokens, 2, Tokens.Count - 2), true, Index) + ");";

                    // SYSTEM NAMESPACE INTEGRATION
                    case "printtext":
                        return "System.Console.WriteLine(System.Text.Encoding.ASCII.GetString(" + Tokens[1] + "));";
                    case "print":
                        return "System.Console.WriteLine(" + Tokens[1] + ");";
                    
                    // ARITHMETIC INSTRUCTIONS
                    case "add":
                        return Tokens[1] + " += " + Tokens[2] + ";";
                    case "substract":
                        return Tokens[1] + " -= " + Tokens[2] + ";";
                    case "multiply":
                        return Tokens[1] + " *= " + Tokens[2] + ";";
                    case "divide":
                        return Tokens[1] + " = System.Convert.ToByte(" + Tokens[1] + " / " + Tokens[2] + ");";
                    case "modulo":
                        return Tokens[1] + " = System.Convert.ToByte(" + Tokens[1] + " % " + Tokens[2] + ");";

                    // BITWISE INSTRUCTIONS
                    case "invert":
                        return Tokens[1] + " = ~" + Tokens[1];
                    case "lshift":
                        return Tokens[1] + " <<= " + Tokens[2];
                    case "rshift":
                        return Tokens[1] + " >>= " + Tokens[2];
                    case "urshift":
                        return Tokens[1] + " >>>= " + Tokens[2];
                    case "and":
                        return Tokens[1] + " &= " + Tokens[2];
                    case "or":
                        return Tokens[1] + " |= " + Tokens[2];
                    case "xor":
                        return Tokens[1] + " ^= " + Tokens[2];

                    // EMPTY LINE OR SYNTAX ERROR
                    default:
                        if(Line != ""){
                            PrintError(1, Index);
                            return "ERROR";
                        } else {
                            return "";
                        }
                }
            } catch(IndexOutOfRangeException) {
                PrintError(1, Index);
                return "ERROR";
            }
        }

        // CONDITION INTEPRETER
        static string InterpretCondition(List<string> Tokens, int LineIndex){
            if(Tokens.Count > 1){
                switch(Tokens[1]){
                    case "equals":
                        return Tokens[0] + " == " + Tokens[2];
                    case "greater":
                        return Tokens[0] + " > " + Tokens[2];
                    case "less":
                        return Tokens[0] + " < " + Tokens[2];
                    default:
                        PrintError(1, LineIndex);
                        return "ERROR";
                }
            } else {
                PrintError(1, LineIndex);
                return "ERROR";
            }
        }

        // VARIABLE AND ARGUMENT INTERPRETER
        static string InterpretDatatypes(List<string> Tokens, bool IsArg, int LineIndex){
            string Out = "";
            int i = 0;
            if(Tokens.Count == 0){
                return "";
            }
            while(i < Tokens.Count){
                if(i + 1 >= Tokens.Count){
                    PrintError(1, LineIndex);
                    return "ERROR";
                }
                switch(Tokens[i]){
                    // 8-BIT
                    case "int":
                        Out += "byte " + Tokens[i + 1];
                        break;
                    case "array":
                        Out += "byte[] " + Tokens[i + 1];
                        break;
                    case "list":
                        Out += "List<byte> " + Tokens[i + 1] + (IsArg? "" : " = new List<byte>();");
                        break;
                    // 16-BIT
                    case "int16":
                        Out += "ushort " + Tokens[i + 1];
                        break;
                    case "array16":
                        Out += "ushort[] " + Tokens[i + 1];
                        break;
                    case "list16":
                        Out += "List<ushort> " + Tokens[i + 1] + (IsArg? "" : " = new List<ushort>();");
                        break;
                    // 32-BIT
                    case "int32":
                        Out += "uint " + Tokens[i + 1];
                        break;
                    case "array32":
                        Out += "uint[] " + Tokens[i + 1];
                        break;
                    case "list32":
                        Out += "List<uint> " + Tokens[i + 1] + (IsArg? "" : " = new List<uint>();");
                        break;
                    default:
                        PrintError(1, LineIndex);
                        return "ERROR";
                }
                if(i < Tokens.Count - 2 && Tokens.Count > 3){
                    Out += ", ";
                }
                i += 2;
            }
            if(Out == ""){
                PrintError(1, LineIndex);
                return "ERROR";
            } else {
                return Out;
            }
        }

        // GET RANGE OF TOKENS
        static List<string> TokenRange(List<string> Tokens, int Start, int Count){
            List<string> TempList = new List<string>();
            for(int i = Start; i < Count + Start; i++){
                TempList.Add(Tokens[i]);
            }
            return TempList;
        }

        // ERROR LOGGER
        static void PrintError(int Index, int LineIndex){
            switch(Index){
                case 0:
                    Console.WriteLine("ERROR: Unrecognized file format");
                    return;
                case 1:
                    Console.WriteLine("ERROR at line " + (LineIndex + 1).ToString() + ": Syntax error");
                    StopFlag = true;
                    return;
            }
        }
    }
}