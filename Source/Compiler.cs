namespace Surplus
{
    using System.IO;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    static class Compiler {
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
            try {
                switch(Tokens[0]){
                    // DECLARATIONS
                    case "namespace":
                        return Line + " {";
                    case "import":
                        return "using " + String.Join(".", TokenRange(Tokens, Index, 1, Tokens.Count - 1).ToArray()) + ";";
                    case "class":
                        return TokenRangeString(Tokens, Index, " ", -1, -1, "as", "", true) + "class " + Tokens[1] + " {";
                    case "function":
                        return TokenRangeString(Tokens, Index, " ", -1, -1, "as", "", true) + "void " + Tokens[1] + "(" + InterpretDatatypes(TokenRange(Tokens, Index, 2, -1, "", "as"), true, Index) + "){";
                    case "declare":
                        return TokenRangeString(Tokens, Index, " ", -1, -1, "as", "", true) + InterpretDatatypes(TokenRange(Tokens, Index, 1, -1, "", "as"), false, Index) + ";";;

                    // VARIABLE MANAGEMENT
                    case "set":
                        return TokenRangeString(Tokens, Index, ".", 1, -1, "", "to") + " = " + ParseForStrings(TokenRangeString(Tokens, Index, ".", -1, -1, "to")) + ";";
                    case "setindex":
                        return TokenRangeString(Tokens, Index, ".", -1, -1, "in", "to") + "[" + TokenRangeString(Tokens, Index, ".", 1, -1, "", "in") + "] = " + ParseForStrings(TokenRangeString(Tokens, Index, ".", -1, -1, "to")) + ";";
                    case "getindex":
                        return TokenRangeString(Tokens, Index, ".", -1, -1, "to") + " = " + ParseForStrings(TokenRangeString(Tokens, Index, ".", -1, -1, "in", "to")) + "[" + TokenRangeString(Tokens, Index, ".", 1, -1, "", "in") + "];";
                    case "setclone":
                        return "System.Array.Copy(" + TokenRangeString(Tokens, Index, ".", 1, -1, "", "to") + ", " + TokenRangeString(Tokens, Index, ".", -1, -1, "to") + ", " + TokenRangeString(Tokens, Index, ".", 1, -1, "", "to") + ".Length);";
                    case "append":
                        return TokenRangeString(Tokens, Index, ".", -1, -1, "to") + ".Add(" + ParseForStrings(TokenRangeString(Tokens, Index, ".", 1, -1, "", "to")) + ");";
                    case "remove":
                        return TokenRangeString(Tokens, Index, ".", -1, -1, "from") + ".RemoveAt(" + TokenRangeString(Tokens, Index, ".", 1, -1, "", "from") + ");";
                    case "appendall":
                        return TokenRangeString(Tokens, Index, ".", -1, -1, "to") + ".AddRange(" + TokenRangeString(Tokens, Index, ".", 1, -1, "", "to") + ");";
                    case "setlength":
                        return TokenRangeString(Tokens, Index, ".", 1, -1, "", "to") + " = new " + TokenRangeString(Tokens, Index, ".", -1, -1, "with") + "[" + TokenRangeString(Tokens, Index, ".", -1, -1, "to", "with") + "];";
                    
                    // GENERAL INSTRUCTIONS
                    case "start":
                        return "{";
                    case "end":
                        return "}";
                    case "if":
                        return "if(" + InterpretCondition(TokenRange(Tokens, Index, 1, Tokens.Count - 1), Index) + "){";
                    case "ifnot":
                        return "if(!(" + InterpretCondition(TokenRange(Tokens, Index, 1, Tokens.Count - 1), Index) + ")){";
                    case "else":
                        return "} else {";
                    case "while":
                        return "while(" + InterpretCondition(TokenRange(Tokens, Index, 1, Tokens.Count - 1), Index) + "){";
                    case "whilenot":
                        return "while(!(" + InterpretCondition(TokenRange(Tokens, Index, 1, Tokens.Count - 1), Index) + ")){";
                    case "switch":
                        return "switch(" + TokenRangeString(Tokens, Index, ".", 1) + "){";
                    case "case":
                        return "case " + ParseForStrings(TokenRangeString(Tokens, Index, ".", 1)) + ":";
                    case "call":
                        return TokenRangeString(Tokens, Index, ".", 1, -1, "", "with") + "(" + ParseForStrings(TokenRangeString(Tokens, Index, ".", -1, -1, "with").Replace(".and.", ", ")) + ");";

                    // SYSTEM NAMESPACE INTEGRATION
                    case "printtext":
                        return "System.Console.WriteLine(System.Text.Encoding.ASCII.GetString(" + TokenRangeString(Tokens, Index, ".", 1) + "));";
                    case "print":
                        return "System.Console.WriteLine(" + ParseForStrings(TokenRangeString(Tokens, Index, ".", 1)) + ");";
                    
                    // ARITHMETIC INSTRUCTIONS
                    case "increase":
                        return TokenRangeString(Tokens, Index, ".", 1, -1, "", "by") + " += " + TokenRangeString(Tokens, Index, ".", -1, -1, "by") + ";";
                    case "decrease":
                        return TokenRangeString(Tokens, Index, ".", 1, -1, "", "by") + " -= " + TokenRangeString(Tokens, Index, ".", -1, -1, "by") + ";";
                    case "multiply":
                        return TokenRangeString(Tokens, Index, ".", 1, -1, "", "by") + " *= " + TokenRangeString(Tokens, Index, ".", -1, -1, "by") + ";";
                    case "divide":
                        return TokenRangeString(Tokens, Index, ".", 1, -1, "", "by") + " = " + TokenRangeString(Tokens, Index, ".", 1, -1, "", "by") + " / " + TokenRangeString(Tokens, Index, ".", -1, -1, "by") + ";";
                    case "modulo":
                        return TokenRangeString(Tokens, Index, ".", 1, -1, "", "by") + " = " + TokenRangeString(Tokens, Index, ".", 1, -1, "", "by") + " % " + TokenRangeString(Tokens, Index, ".", -1, -1, "by") + ";";

                    // BITWISE INSTRUCTIONS
                    case "invert":
                        return TokenRangeString(Tokens, Index, ".", -1, -1, "to") + " = ~" + TokenRangeString(Tokens, Index, ".", 1, -1, "", "to");
                    case "lshift":
                        return TokenRangeString(Tokens, Index, ".", -1, -1, "to") + " <<= " + TokenRangeString(Tokens, Index, ".", 1, -1, "", "to");
                    case "rshift":
                        return TokenRangeString(Tokens, Index, ".", -1, -1, "to") + " >>= " + TokenRangeString(Tokens, Index, ".", 1, -1, "", "to");
                    case "urshift":
                        return TokenRangeString(Tokens, Index, ".", -1, -1, "to") + " >>>= " + TokenRangeString(Tokens, Index, ".", 1, -1, "", "to");
                    case "and":
                        return TokenRangeString(Tokens, Index, ".", -1, -1, "to") + " &= " + TokenRangeString(Tokens, Index, ".", 1, -1, "", "to");
                    case "or":
                        return TokenRangeString(Tokens, Index, ".", -1, -1, "to") + " |= " + TokenRangeString(Tokens, Index, ".", 1, -1, "", "to");
                    case "xor":
                        return TokenRangeString(Tokens, Index, ".", -1, -1, "to") + " ^= " + TokenRangeString(Tokens, Index, ".", 1, -1, "", "to");

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
                        return TokenRangeString(Tokens, LineIndex, ".", 0, -1, "", "equals") + " == " + ParseForStrings(TokenRangeString(Tokens, LineIndex, ".", -1, -1, "equals"));
                    case "greater":
                        return TokenRangeString(Tokens, LineIndex, ".", 0, -1, "", "greater") + " > " + TokenRangeString(Tokens, LineIndex, ".", -1, -1, "greater");
                    case "less":
                        return TokenRangeString(Tokens, LineIndex, ".", 0, -1, "", "less") + " < " + TokenRangeString(Tokens, LineIndex, ".", -1, -1, "less");
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
                    case "array":
                        if(i + 2 >= Tokens.Count){
                            PrintError(1, LineIndex);
                            return "ERROR";
                        }
                        Out += Tokens[i + 1] + "[] " + Tokens[i + 2];
                        i++;
                        break;
                    case "list":
                        if(i + 2 >= Tokens.Count){
                            PrintError(1, LineIndex);
                            return "ERROR";
                        }
                        Out += "List<" + Tokens[i + 1] + "> " + Tokens[i + 2];
                        i++;
                        break;
                    default:
                        Out += Tokens[i] + " " + Tokens[i + 1];
                        break;
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
        static List<string> TokenRange(List<string> Tokens, int LineIndex, int Start = 0, int Count = -1, string StartKeyword = "", string EndKeyword = ""){
            List<string> TempList = new List<string>();
            if(StartKeyword != ""){
                int FoundIndex = Tokens.IndexOf(StartKeyword);
                if(FoundIndex != -1 && FoundIndex < Tokens.Count - 1){
                    Start = FoundIndex + 1;
                }
                if(FoundIndex == -1){
                    return TempList;
                }
            }
            if(Count == -1){
                Count = Tokens.Count - Start;
            }
            for(int i = Start; i < Count + Start; i++){
                if(EndKeyword != "" && Tokens[i] == EndKeyword){
                    break;
                }
                TempList.Add(Tokens[i]);
            }
            return TempList;
        }

        // GET TOKENRANGE AS STRING
        static string TokenRangeString(List<string> Tokens, int LineIndex, string Seperator, int Start = 0, int Count = -1, string StartKeyword = "", string EndKeyword = "", bool TrailingChar = false){
            List<string> TempList = TokenRange(Tokens, LineIndex, Start, Count, StartKeyword, EndKeyword);
            return string.Join(Seperator, TempList.ToArray()) + ((TrailingChar && TempList.Count > 0)? Seperator : "");
        }

        // HANDLE STRING VALUES
        static string ParseForStrings(string Input){
            bool Quoting = false;
            string Output = "";
            int i = 0;
            while(i < Input.Length){
                if(Input.Length >= i + 6 && Input.Substring(i, 6) == "quote."){
                    Quoting = true;
                    Output += "\"";
                    i += 5;
                } else if(Input.Length >= i + 6 && Input.Substring(i, 6) == ".quote"){
                    Quoting = false;
                    Output += "\"";
                    i += 5;
                } else if(Quoting && Input[i] == '.'){
                    Output += " ";
                } else {
                    Output += Input[i];
                }
                i++;
            }
            return Output;
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