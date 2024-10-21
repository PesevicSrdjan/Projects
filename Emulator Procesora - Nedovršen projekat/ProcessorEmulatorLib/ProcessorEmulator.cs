using System;
using System.Collections;
using System.Net.NetworkInformation;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using static ProcessorEmulatorLib.Instruction;
using System.Net;
using System.Data.Common;
using System.Reflection;

namespace ProcessorEmulatorLib
{
    public class ProcessorEmulator
    {
        //64 - bitni registar opšte namjene.(U projektnom zadatku je specificirano da imamo 4 registra, pa je zbog toga readonly).
        private long[] registers;
        private readonly int frameCount = 1;
        private readonly int frameSize = 256;
        //Programski brojač.
        long programCounter;
        //Kolekcija koja predstavlja adresni prostor.
        private long [] addressSpace;
        //Memorija
        private IDictionary<byte, Instruction> instructionMapp;
        private IDictionary<byte, Data> dataMapp;
        private int number = 0;
        private Memory memory;
        //Potrebno je dodati memory (gdje će biti smješteni podaci i instrukcije).
        

        private bool equal;
        private bool firstIsGreaterThanSecond;
        private bool secondIsGreaterThanFirst;
        public ProcessorEmulator(int numberOfAddress)
        {
            registers = new long[4];
            programCounter = 0;
            addressSpace = new long[numberOfAddress];
            memory = new Memory(frameCount, frameSize);
            instructionMapp = new Dictionary<byte, Instruction>();
            dataMapp = new Dictionary<byte, Data>();

        }

        //Metoda za dodavanje konkretne vrijednosti u registar ili vrijednosti drugog registra.
        public void ADD(ArithmeticOperations instruction)
        {
            
            if (instruction.getOperationType() == Instruction.OperationType.Value)
            {
                Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                registers[instruction.getIndexOfRegister()] = registers[instruction.getIndexOfRegister()] + instruction.getValue();
            }
            else if (instruction.getOperationType() == Instruction.OperationType.RegisterValue)
            {
                Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                registers[instruction.getIndexOfRegister()] = registers[instruction.getIndexOfRegister()] + registers[(int)instruction.getValue()];

            }
            else if(instruction.getOperationType() == Instruction.OperationType.Data)
            {
                programCounter = registers[instruction.getValue()];
                
                object obj = FetchFromMemory();
                if(obj is Data)
                {
                    Data data = (Data)obj;
                    ArithmeticOperations arm = new ArithmeticOperations(instruction.getNameOfInstruction(), instruction.getIndexOfRegister(), data.getValue(), Instruction.OperationType.Value);
                    ADD(arm);
                }


            }

        }
        //Metoda za oduzimanje konkretne vrijednosti iz registra ili  vrijednosti drugog registra.
        public void SUB(ArithmeticOperations instruction)
        {
            
            if (instruction.getOperationType() == Instruction.OperationType.Value)
            {
                Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                registers[instruction.getIndexOfRegister()] = registers[instruction.getIndexOfRegister()] - instruction.getValue();
            }
            else if (instruction.getOperationType() == Instruction.OperationType.RegisterValue)
            {
                Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                registers[instruction.getIndexOfRegister()] = registers[instruction.getIndexOfRegister()] - registers[(int)instruction.getValue()];

            }
            else if (instruction.getOperationType() == Instruction.OperationType.Data)
            {
                programCounter = registers[instruction.getValue()];
                object obj = FetchFromMemory();
                if (obj is Data)
                {
                    Data data = (Data)obj;
                    ArithmeticOperations arm = new ArithmeticOperations(instruction.getNameOfInstruction(), instruction.getIndexOfRegister(), data.getValue(), Instruction.OperationType.Value);
                    SUB(arm);
                }

            }

        }
        //Metoda za množenje registra sa konkretnom vrijednošću ili sa vrijednošću drugog registra.
        public void MUL(ArithmeticOperations instruction)
        {
            
            if (instruction.getOperationType() == Instruction.OperationType.Value)
            {
                Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                registers[instruction.getIndexOfRegister()] = registers[instruction.getIndexOfRegister()] * instruction.getValue();
            }
            else if (instruction.getOperationType() == Instruction.OperationType.RegisterValue)
            {
                Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                registers[instruction.getIndexOfRegister()] = registers[instruction.getIndexOfRegister()] * registers[(int)instruction.getValue()];

            }
            else if (instruction.getOperationType() == Instruction.OperationType.Data)
            {
                programCounter = registers[instruction.getValue()];
                object obj = FetchFromMemory();
                if (obj is Data)
                {
                    Data data = (Data)obj;
                    ArithmeticOperations arm = new ArithmeticOperations(instruction.getNameOfInstruction(), instruction.getIndexOfRegister(), data.getValue(), Instruction.OperationType.Value);
                    MUL(arm);
                }


            }

        }
        //Metoda za dijeljenje registra konkretnom vrijednošću ili sa vrijednošću drugog registra.
        public void DIV(ArithmeticOperations instruction)
        {
           
            try
            {
                if (instruction.getOperationType() == Instruction.OperationType.Value)
                {
                    Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                    if (instruction.getValue() != 0)
                    {
                        registers[instruction.getIndexOfRegister()] = registers[instruction.getIndexOfRegister()] / instruction.getValue();

                    }
                    else
                    {
                        throw new DivideByZeroException("Dijeljenje sa nulom.");
                    }

                }
                else if (instruction.getOperationType() == Instruction.OperationType.RegisterValue)
                {
                    Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                    if (registers[(int)instruction.getValue()] != 0)
                    {
                        registers[instruction.getIndexOfRegister()] = registers[instruction.getIndexOfRegister()] / registers[(int)instruction.getValue()];

                    }
                    else
                    {
                        throw new DivideByZeroException("Dijeljenje sa nulom.");
                    }


                }
                else if (instruction.getOperationType() == Instruction.OperationType.Data)
                {
                    programCounter = registers[instruction.getValue()];
                    object obj = FetchFromMemory();
                    if (obj is Data)
                    {
                        Data data = (Data)obj;
                        ArithmeticOperations arm = new ArithmeticOperations(instruction.getNameOfInstruction(), instruction.getIndexOfRegister(), data.getValue(), Instruction.OperationType.Value);
                        DIV(arm);
                    }


                }
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Greška: {ex.Message}");
            }

        }
        
        //Logička operacija AND sa konkretnom vrijednošću ili vrijednošću nekog drugog registra.
        public void AND(LogicalOperations instruction)
        {
            
            if (instruction.getOperationType() == Instruction.OperationType.Value)
            {
                Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                registers[instruction.getIndexOfRegister()] = registers[instruction.getIndexOfRegister()] & instruction.getValue();
            }
            else if (instruction.getOperationType() == Instruction.OperationType.RegisterValue)
            {
                Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                registers[instruction.getIndexOfRegister()] = registers[instruction.getIndexOfRegister()] & registers[(int)instruction.getValue()];

            }
            else if (instruction.getOperationType() == Instruction.OperationType.Data)
            {
                programCounter = registers[instruction.getValue()];
                object obj = FetchFromMemory();
                if (obj is Data)
                {
                    Data data = (Data)obj;
                    LogicalOperations arm = new LogicalOperations(instruction.getNameOfInstruction(), instruction.getIndexOfRegister(), data.getValue(), Instruction.OperationType.Value);
                    AND(arm);
                }
            }
        }
        //Logička operacija OR sa konkretnom vrijednošću ili vrijednošću nekog drugog registra.
        public void OR(LogicalOperations instruction)
        {
            
            if (instruction.getOperationType() == Instruction.OperationType.Value)
            {
                Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                registers[instruction.getIndexOfRegister()] = registers[instruction.getIndexOfRegister()] | instruction.getValue();
            }
            else if (instruction.getOperationType() == Instruction.OperationType.RegisterValue)
            {
                Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                registers[instruction.getIndexOfRegister()] = registers[instruction.getIndexOfRegister()] | registers[(int)instruction.getValue()];
            }
            else if (instruction.getOperationType() == Instruction.OperationType.Data)
            {
                programCounter = registers[instruction.getValue()];
                object obj = FetchFromMemory();
                if (obj is Data)
                {
                    Data data = (Data)obj;
                    LogicalOperations arm = new LogicalOperations(instruction.getNameOfInstruction(), instruction.getIndexOfRegister(), data.getValue(), Instruction.OperationType.Value);
                    OR(arm);
                }
            }
        }
        //Logička operacija NOT.
        public void NOT(LogicalOperations instruction)
        {
            Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
            registers[instruction.getIndexOfRegister()] = ~registers[instruction.getIndexOfRegister()];
        }
        
        //Operacija MOV.
        public void MOV(TransferOperations instruction)
        {
            
            if (instruction.getOperationType() == Instruction.OperationType.Value)
            {
                Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                registers[instruction.getIndexOfRegister()] = instruction.getValue();
            }
            else if (instruction.getOperationType() == Instruction.OperationType.RegisterValue)
            {
                Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                registers[instruction.getIndexOfRegister()] = registers[(int)instruction.getValue()];
            }
            else if (instruction.getOperationType() == Instruction.OperationType.Data)
            {
                programCounter = registers[instruction.getValue()];
                object obj = FetchFromMemory();
                if (obj is Data)
                {
                    Data data = (Data)obj;
                    TransferOperations arm = new TransferOperations(instruction.getNameOfInstruction(), instruction.getIndexOfRegister(), data.getValue(), Instruction.OperationType.Value);
                    MOV(arm);
                }
            }


        }
        //Logička operacija XOR sa konkretnom vrijednošću ili vrijednošću nekog drugog registra.
        public void XOR(LogicalOperations instruction)
        {
           
            if (instruction.getOperationType() == Instruction.OperationType.Value)
            {
                Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                registers[instruction.getIndexOfRegister()] = registers[instruction.getIndexOfRegister()] ^ instruction.getValue();
            }
            else if (instruction.getOperationType() == Instruction.OperationType.RegisterValue)
            {
                Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                registers[instruction.getIndexOfRegister()] = registers[instruction.getIndexOfRegister()] ^ registers[(int)instruction.getValue()];
            }
            else if (instruction.getOperationType() == Instruction.OperationType.Data)
            {
                programCounter = registers[instruction.getValue()];
                object obj = FetchFromMemory();
                if (obj is Data)
                {
                    Data data = (Data)obj;
                    LogicalOperations arm = new LogicalOperations(instruction.getNameOfInstruction(), instruction.getIndexOfRegister(), data.getValue(), Instruction.OperationType.Value);
                    XOR(arm);
                }
            }
        }
        
        //Metoda za dodavanje instrukcije u memoriju.
        public void addInstructionToMemory(Instruction instruction, long virtualAddress)
        {
            if (!addressSpace.Contains(virtualAddress))
            {
                addressSpace[number++] = virtualAddress;
                int frameIndex = (int)(virtualAddress >> 8);
                int offset = (int)(virtualAddress & 0xFFF);

                Random random = new Random();
                byte address;
                do
                {
                    address = (byte)random.Next(256);

                } while (instructionMapp.ContainsKey(address));

                if (offset >= frameSize)
                {
                    memory.AddFrame(frameIndex);
                    int newOffset = offset % frameSize;
                    instructionMapp.Add(address, instruction);
                    memory.WriteByte(frameIndex, newOffset, address);

                }
                else
                {
                    instructionMapp.Add(address, instruction);
                    memory.WriteByte(frameIndex, offset, address);

                }

            }
            else
                Console.WriteLine("Zauzeta adresa!");
        }
       
        public void addDataToMemory(Data data, long virtualAddress)
        {
            addressSpace[number++] = virtualAddress;
            int frameIndex = (int)(virtualAddress >> 8);
            int offset = (int)(virtualAddress & 0xFFF);

            Random random = new Random();
            byte address;
            do
            {
                address = (byte)random.Next(256);

            } while (dataMapp.ContainsKey(address));

            if (offset >= frameSize)
            {
                memory.AddFrame(frameIndex);
                int newOffset = offset % frameSize;
                dataMapp.Add(address, data);
                memory.WriteByte(frameIndex, newOffset, address);

            }
            else
            {
                dataMapp.Add(address, data);
                memory.WriteByte(frameIndex, offset, address);

            }

        }
        //Pokretanje procesora.
        public void Run()
        {
            int i = 0;
            while(i < addressSpace.Length) { 
                
                if(addressSpace[i] != 0)
                {
                    programCounter = addressSpace[i];
                    
                    object result = FetchFromMemory();
                    if (result is Instruction)
                    {
                        Instruction ar = (Instruction)result;
                        if (ar.getNameOfInstruction() == "JMP" || (ar.getNameOfInstruction() == "JE" && AreRegistersEqual(ar) == 0) || (ar.getNameOfInstruction() == "JGE" && AreRegistersEqual(ar) == 1) || (ar.getNameOfInstruction() == "JL" && AreRegistersEqual(ar) == -1) || (ar.getNameOfInstruction() == "JNE" && (AreRegistersEqual(ar) == -1 || AreRegistersEqual(ar) == 1)))
                        {
                            DecodeAndExecute(ar);
                            i = Array.IndexOf(addressSpace, programCounter);
                            continue;
                        }
                        else
                        {
                            DecodeAndExecute(ar);
                            
                        }
                     

                    }   
                }
                 i++;
            }
           
        }
        //Prepoznavanje instrukcije i Izvršavanje.
        public void DecodeAndExecute(Instruction instruction)
        {
            if (instruction.getNameOfInstruction().Equals("ADD"))
            {
                ADD((ArithmeticOperations)instruction);
            }
            else if (instruction.getNameOfInstruction().Equals("SUB"))
            {
                SUB((ArithmeticOperations)instruction);
            }
            else if (instruction.getNameOfInstruction().Equals("MUL"))
            {
                MUL((ArithmeticOperations)instruction);
            }
            else if (instruction.getNameOfInstruction().Equals("DIV"))
            {
                DIV((ArithmeticOperations)instruction);
            }
            else if (instruction.getNameOfInstruction().Equals("AND"))
            {
               AND((LogicalOperations)instruction);
            }
            else if (instruction.getNameOfInstruction().Equals("OR"))
            {
               OR((LogicalOperations)instruction);
            }
            else if (instruction.getNameOfInstruction().Equals("NOT"))
            {
               NOT((LogicalOperations)instruction);
            }
            else if (instruction.getNameOfInstruction().Equals("XOR"))
            {
               XOR((LogicalOperations)instruction);
            }
            else if (instruction.getNameOfInstruction().Equals("MOV"))
            {
                MOV((TransferOperations)instruction);
            }
            else if (instruction.getNameOfInstruction().Equals("JMP"))
            {
                JMP((BranchingOperations)instruction);
            }
            else if (instruction.getNameOfInstruction().Equals("JE"))
            {
                JE((BranchingOperations)instruction);
            }
            else if (instruction.getNameOfInstruction().Equals("JNE"))
            {
                JNE((BranchingOperations)instruction);
            }
            else if (instruction.getNameOfInstruction().Equals("JGE"))
            {
                JGE((BranchingOperations)instruction);
            }
            else if (instruction.getNameOfInstruction().Equals("JL"))
            {
                JL((BranchingOperations)instruction);
            }
            else if (instruction.getNameOfInstruction().Equals("INPUT"))
            {
               Input((InputOutput)instruction);
            }
            else if (instruction.getNameOfInstruction().Equals("OUTPUT"))
            {
               Output((InputOutput)instruction);
            }
            else if (instruction.getNameOfInstruction().Equals("HALT"))
            {
                HALT(instruction);
            }
            else if (instruction.getNameOfInstruction().Equals("CMP"))
            {
                CMP((BranchingOperations)instruction);
            }
            else
            {
                Console.WriteLine("Ne postoji navedena operacija!");
            }


        }
        //Iskorišteno za indirektno adresiranje kod JMP.
        public void SetAddressInRegister(int indexOfRegister, long virtualAddress)
        {
            foreach(var reg in registers)
            {
                Console.WriteLine(reg);
            }
            
            registers[indexOfRegister] = virtualAddress;
            Console.WriteLine(registers[indexOfRegister]);
        }

        //Metoda iskorištena, kako bi smo imali direktno i indirektno adresiranje kod JUMP.

        //JMP - Mijenja programski brojač na takav način da ukoliko se izvrši instrukcija skoka, sljedeća adresa na kojoj će se izvršiti
        //instrukcija je ona adresa na koju se "skočilo".
        public void JMP(BranchingOperations instruction)
        {
            if (instruction.getDirectOrIndirect() == Instruction.DirectOrIndirect.Direct)
            {
                Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                programCounter = instruction.getAddress();
 
            }
            else if(instruction.getDirectOrIndirect() == Instruction.DirectOrIndirect.Indirect)
            {
                Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                long addressFromRegister = registers[instruction.getAddress()];
                programCounter = addressFromRegister;
                
            }
        }
        
        //Metoda JE (Jump if Equal).
        public void JE(BranchingOperations instruction)
        {
            int index1 = instruction.getIndex1();
            
            int index2 = instruction.getIndex2();

            BranchingOperations jmpInstruction = new BranchingOperations(instruction.getNameOfInstruction(), instruction.getAddress(), instruction.getDirectOrIndirect());

            if (registers[index1] == registers[index2])
            {
                JMP(jmpInstruction);
            }
            

        }
        //Metoda JNE (Jump if not Equal).
         public void JNE(BranchingOperations instruction)
         {
            int index1 = instruction.getIndex1();
            int index2 = instruction.getIndex2();
            BranchingOperations jmpInstruction = new BranchingOperations(instruction.getNameOfInstruction(), instruction.getAddress(), instruction.getDirectOrIndirect());

            if (registers[index1] != registers[index2])
            {
                JMP(jmpInstruction);
            }


        }
        //Metoda JGE (Jump if Greater).
        public void JGE(BranchingOperations instruction)
        {
            int index1 = instruction.getIndex1();

            int index2 = instruction.getIndex2();

            BranchingOperations jmpInstruction = new BranchingOperations(instruction.getNameOfInstruction(), instruction.getAddress(), instruction.getDirectOrIndirect());

            if (registers[index1] > registers[index2])
            {
                JMP(jmpInstruction);
            }
        }
        public int AreRegistersEqual(Instruction instruction)
        {
            if (instruction is BranchingOperations)
            {
                BranchingOperations instr = (BranchingOperations)instruction;
                int index1 = instr.getIndex1();
                int index2 = instr.getIndex2();

                if (registers[index1] == registers[index2])
                    return 0;
                else if (registers[index1] > registers[index2])
                    return 1;
                else
                    return -1;
            }
            else
            {
                return 0;
            }
        }
        

        //Metoda JL (Jump if Less).
        public void JL(BranchingOperations instruction)
        {
            int index1 = instruction.getIndex1();

            int index2 = instruction.getIndex2();

            BranchingOperations jmpInstruction = new BranchingOperations(instruction.getNameOfInstruction(), instruction.getAddress(), instruction.getDirectOrIndirect());

            if (registers[index1] < registers[index2])
            {
                JMP(jmpInstruction);
            }

        }

        //Metoda CMP.
        public void CMP(BranchingOperations instruction)
        {
            if (instruction.getDirectOrIndirect() == Instruction.DirectOrIndirect.Direct && instruction.getOperationType() == Instruction.OperationType.Value)
            {
                Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                int index = instruction.getIndex1();
                long value = instruction.getAddress();
                if (registers[index] == value)
                {
                    equal = true;
                    firstIsGreaterThanSecond = false;
                    secondIsGreaterThanFirst = false;
                    Console.WriteLine("Registar je jednak sa vrijednošću " + value);

                }
                else if (registers[index] > value)
                {
                    equal = false;
                    firstIsGreaterThanSecond = true;
                    secondIsGreaterThanFirst = false;
                    Console.WriteLine("Registar [" + index + "] je veći od vrijednosti " + value);
                }
                else
                {
                    equal = false;
                    firstIsGreaterThanSecond = false;
                    secondIsGreaterThanFirst = true;
                    Console.WriteLine("Registar [" + index + "] je manji od vrijednosti " + value);
                }
            }
            else if(instruction.getDirectOrIndirect() == Instruction.DirectOrIndirect.Direct && instruction.getOperationType() == Instruction.OperationType.RegisterValue)
            {
                Console.WriteLine($"Executing instruction: {instruction.getNameOfInstruction()}");
                int index1 = instruction.getIndex1();
                int index2 = (int)instruction.getAddress();
                if (registers[index1] == registers[index2])
                {
                    equal = true;
                    firstIsGreaterThanSecond = false;
                    secondIsGreaterThanFirst = false;
                    Console.WriteLine("Registri su jednaki!");

                }
                else if (registers[index1] > registers[index2])
                {
                    equal = false;
                    firstIsGreaterThanSecond = true;
                    secondIsGreaterThanFirst = false;
                    Console.WriteLine("Registar ["+ index1 + "] je veći od Registra [" + index2 + "] !");
                }
                else
                {
                    equal = false;
                    firstIsGreaterThanSecond = false;
                    secondIsGreaterThanFirst = true;
                    Console.WriteLine("Registar [" + index1 + "] je manji od Registra [" + index2 + "] !");
                }

            }
            else if(instruction.getDirectOrIndirect() == Instruction.DirectOrIndirect.Indirect)
            {
                
                long addressFromRegister = registers[(int)instruction.getAddress()];
                programCounter = addressFromRegister;
                object obj = FetchFromMemory();
                if (obj is Data)
                {
                    Data data = (Data)obj;
                    BranchingOperations cmp = new BranchingOperations(instruction.getNameOfInstruction(), instruction.getIndex1(), data.getValue(), Instruction.DirectOrIndirect.Direct, OperationType.Value);
                    CMP(cmp);
                }
                
            }

        }
        
        
        //Pribavljanje instrukcije iz adresnog prostora.
        public object FetchFromMemory()
        {
            
            int frameIndex = (int)(programCounter >> 8);
            int offset = (int)(programCounter & 0xFFF) % frameSize;
            byte readByte = memory.ReadByte(frameIndex, offset);
           
            if (instructionMapp.ContainsKey(readByte))
            {
                return instructionMapp[readByte];
            }
            else if(dataMapp.ContainsKey(readByte))
            {
                return dataMapp[readByte];
            }
           return null;
        }
        public void print()
        {
            for (int i = 0; i < registers.Length; i++)
            {
                Console.WriteLine("Register [" + i + "]:" + registers[i]);
            }
            memory.PrintMemoryContents();
        }

        //I/O rutine.
        public void Input(InputOutput instruction)
        {
            Console.Write("Unesite znak sa tastature: ");
            char c = Console.ReadKey().KeyChar;
            registers[instruction.getIndexOfRegister()] = (long)c;
            Console.WriteLine();
        }
        public void Output(InputOutput instruction)
        {
            char output = (char)registers[instruction.getIndexOfRegister()];
            Console.WriteLine("Znak je:" + output);

        }
        //Metoda za zaustavljanje rada procesora.
        public void HALT(Instruction instruction)
        {
            Console.WriteLine("Procesor je zaustavljen.");
            Environment.Exit(0);

        }



    }
        
        
        
        
       

    
}