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
            try {
                switch(Tokens[0]){
                    // DECLARATIONS
                    case "namespace":
                        return Line + " {" + '\n' + "using System;" + '\n' + "using System.Collections.Generic;";
                    case "class":
                        return Prefix + "class " + Tokens[1] + " {";
                    case "function":
                        return Prefix + "void " + Tokens[1] + "(" + InterpretDatatypes(TokenRange(Tokens, 2, Tokens.Count - 2), true, Index) + "){";
                    case "declare":
                        return Prefix + InterpretDatatypes(TokenRange(Tokens, 1, Tokens.Count - 1), false, Index) + ";";

                    // VARIABLE MANAGEMENT
                    case "set":
                        return Tokens[1] + " = " + Tokens[2] + ";";
                    case "setall":
                        return Tokens[1] + " = new byte[]{" + string.Join(", ", TokenRange(Tokens, 2, Tokens.Count - 2).ToArray()) + "};";
                    case "append":
                        return Tokens[1] + ".Add(" + Tokens[2] + ");";
                    case "remove":
                        return Tokens[1] + ".RemoveAt(" + Tokens[2] + ");";
                    case "appendall":
                        return Tokens[1] + ".AddRange(" + Tokens[2] + ");";
                    case "tobytes":
                        return Tokens[2] + " = " + Tokens[1] + ".ToArray();";
                    case "tobytelist":
                        return Tokens[2] + " = " + Tokens[1] + ".ToList();";
                    
                    // GENERAL INSTRUCTIONS
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
                        return Tokens[1] + "(" + InterpretDatatypes(TokenRange(Tokens, 2, Tokens.Count - 2), true, Index) + ");";

                    // COMMAND-LINE INSTRUCTIONS
                    case "printtext":
                        return "Console.WriteLine(System.Text.Encoding.ASCII.GetString(" + Tokens[1] + "));";
                    case "print":
                        return "Console.WriteLine(" + Tokens[1] + ");";
                    
                    // ARITHMETIC INSTRUCTIONS
                    case "add":
                        return Tokens[1] + " += " + Tokens[2] + ";";
                    case "substract":
                        return Tokens[1] + " -= " + Tokens[2] + ";";
                    case "multiply":
                        return Tokens[1] + " *= " + Tokens[2] + ";";
                    case "divide":
                        return Tokens[1] + " = Convert.ToByte(" + Tokens[1] + " / " + Tokens[2] + ");";
                    case "modulo":
                        return Tokens[1] + " = Convert.ToByte(" + Tokens[1] + " % " + Tokens[2] + ");";

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
                    case "byte":
                        Out += "byte " + Tokens[i + 1];
                        break;
                    case "bytes":
                        Out += "byte[] " + Tokens[i + 1];
                        break;
                    case "bytelist":
                        Out += "List<byte> " + Tokens[i + 1] + (IsArg? "" : " = new List<byte>();");
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