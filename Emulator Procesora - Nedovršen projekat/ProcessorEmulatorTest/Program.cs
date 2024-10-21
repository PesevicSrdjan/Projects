using ProcessorEmulatorLib;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Net;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using static ProcessorEmulatorLib.Instruction;

ProcessorEmulator processorEmulator = new ProcessorEmulator(1000);
string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
string pathToParser = Path.Combine(desktopPath, "Arhitektura Projektni", "ProcessorEmulator", "parser.txt");
string[] lines = File.ReadAllLines(pathToParser);
foreach (string line in lines)
{
    string[] parts = line.Split(" ");
    string operationName = parts[0]; //Naziv instrukcije
    if (operationName == "ADD" || operationName == "SUB" || operationName == "MUL" || operationName == "DIV")
    {
        int indexOfRegister = int.Parse(parts[1]); //Indeks registra.
        long value = long.Parse(parts[2]); //Vrijednost ili vrijednost nekog registra.
        long address = Convert.ToInt64(parts[4], 16); //Adresa na koju se dodaje instrukcija
        Instruction.OperationType operationType; //Tip operacije.
        if (parts[3] == "VALUE")
        {
            operationType = Instruction.OperationType.Value;
        }
        else if (parts[3] == "REGISTER_VALUE")
        {
            operationType = Instruction.OperationType.RegisterValue;
        }
        else if (parts[3] == "DATA")
        {
            operationType = Instruction.OperationType.Data;
        }
        else
        {
            throw new ArgumentException("Invalid operation type specified.");
        }
        ArithmeticOperations instruction = new ArithmeticOperations(operationName, indexOfRegister, value, operationType);
        processorEmulator.addInstructionToMemory(instruction, address);

    }
    else if (operationName == "DATA")
    {
        /*
         * U parser datoteci se može kreirati podatak upotrebom:
           DATA [vrijednost_podatka] [indeks_registra_na_koji_je_smještena_virtuelna_adresa_podatka]  .
           Iskorišteno, kako bi instrukcije mogle da obavljaju operacije sa podacima koji se već nalaze u memoriji.
        */
        long value = long.Parse(parts[1]); //Vrijednost podatka.
        long address = Convert.ToInt64(parts[2], 16);//Adresa podatka.
        Data data = new Data(value);
        processorEmulator.addDataToMemory(data, address);


    }
    else if (operationName == "AND" || operationName == "OR" || operationName == "XOR" || operationName == "NOT")
    {

        if (operationName == "NOT")
        {
            int indexOfRegisterNot = int.Parse(parts[1]); //Indeks registra.
            long addressNot = Convert.ToInt64(parts[2], 16); //Adresa podatka.
            LogicalOperations logInstruction = new LogicalOperations(operationName, indexOfRegisterNot);
            processorEmulator.addInstructionToMemory(logInstruction, addressNot);
            continue;
        }
        int indexOfRegister = int.Parse(parts[1]);
        long value = long.Parse(parts[2]);
        long address = Convert.ToInt64(parts[4], 16);
        Instruction.OperationType operationType;

        if (parts[3] == "VALUE")
        {
            operationType = Instruction.OperationType.Value;
        }
        else if (parts[3] == "REGISTER_VALUE")
        {
            operationType = Instruction.OperationType.RegisterValue;
        }
        else if (parts[3] == "DATA")
        {
            operationType = Instruction.OperationType.Data;
        }
        else
        {
            throw new ArgumentException("Invalid operation type specified.");
        }
        LogicalOperations instruction = new LogicalOperations(operationName, indexOfRegister, value, operationType);
        processorEmulator.addInstructionToMemory(instruction, address);


    }
    else if (operationName == "MOV")
    {
        if (parts[3] == "ADDRESS")
        {
            Instruction.OperationType operType = Instruction.OperationType.Value;
            int indexOfReg = int.Parse(parts[1]);
            long addressToReg = Convert.ToInt64(parts[2], 16);
            long addressOfInst = Convert.ToInt64(parts[4], 16);
            TransferOperations instr = new TransferOperations(operationName, indexOfReg, addressToReg, operType);
            processorEmulator.addInstructionToMemory(instr, addressOfInst);
            continue;
        }
        int indexOfRegister = int.Parse(parts[1]);
        long value = long.Parse(parts[2]);
        long address = Convert.ToInt64(parts[4], 16);
        Instruction.OperationType operationType;

        if (parts[3] == "VALUE")
        {
            operationType = Instruction.OperationType.Value;
        }
        else if (parts[3] == "REGISTER_VALUE")
        {
            operationType = Instruction.OperationType.RegisterValue;
        }
        else if (parts[3] == "DATA")
        {
            operationType = Instruction.OperationType.Data;
        }
        else
        {
            throw new ArgumentException("Invalid operation type specified.");
        }
        TransferOperations instruction = new TransferOperations(operationName, indexOfRegister, value, operationType);
        processorEmulator.addInstructionToMemory(instruction, address);

    }
    else if (operationName == "JMP")
    {
        Instruction.DirectOrIndirect directOrIndirect; //Tip adresiranja.
        long addressOfInstruction = Convert.ToInt64(parts[3], 16); //Adresa instrukcije JMP.
        if (parts[2] == "DIRECT")
        {
            /*  Ako je u pitanju direktno adresiranje, naveli smo konkretnu adresu na koju želimo da se skoči.
             * 
             * 
             */
            directOrIndirect = Instruction.DirectOrIndirect.Direct;
            long address = Convert.ToInt64(parts[1], 16);
            BranchingOperations jmpInstr = new BranchingOperations(operationName, address, directOrIndirect);
            processorEmulator.addInstructionToMemory(jmpInstr, addressOfInstruction);
        }
        else if (parts[2] == "INDIRECT")
        {
            int indexOfRegister = int.Parse(parts[1]);
            /*  Ako je u pitanju indirektno adresiranje, naveli smo registar koji sadrži adresu na koju se treba skočiti.
             * NAPOMENA:Prethodno je tu adresu potrebno unijeti u registar korištenjem SET [indeks_registra][adresa_instrukcije] u parser datoteci.
             * 
             */
            directOrIndirect = Instruction.DirectOrIndirect.Indirect;
            BranchingOperations jmpInstr = new BranchingOperations(operationName, indexOfRegister, directOrIndirect);
            processorEmulator.addInstructionToMemory(jmpInstr, addressOfInstruction);
        }
        else
        {
            throw new ArgumentException("Invalid operation type specified.");
        }

    }
    else if (operationName == "JE" || operationName == "JNE" || operationName == "JGE" || operationName == "JL")
    {
        Instruction.DirectOrIndirect directOrIndirect;
        int indexOfRegister1 = int.Parse(parts[1]);
        int indexOfRegister2 = int.Parse(parts[2]);
        long addressOfInstruction = Convert.ToInt64(parts[5], 16);
        if (parts[4] == "DIRECT")
        {
            directOrIndirect = Instruction.DirectOrIndirect.Direct;
            long address = Convert.ToInt64(parts[3], 16);
            BranchingOperations jmpInstr = new BranchingOperations(operationName, indexOfRegister1, indexOfRegister2, address, directOrIndirect);
            processorEmulator.addInstructionToMemory(jmpInstr, addressOfInstruction);
        }
        else if (parts[4] == "INDIRECT")
        {
            directOrIndirect = Instruction.DirectOrIndirect.Indirect;
            int indexOfRegister = int.Parse(parts[3]);
            BranchingOperations jmpInstr = new BranchingOperations(operationName, indexOfRegister1, indexOfRegister2, indexOfRegister, directOrIndirect);
            processorEmulator.addInstructionToMemory(jmpInstr, addressOfInstruction);
        }
        else
        {
            throw new ArgumentException("Invalid operation type specified.");
        }


    }
    else if (operationName == "CMP")
    {
        int indexOfRegister = int.Parse(parts[1]);
        long valueOrRegister = long.Parse(parts[2]);

        Instruction.OperationType operationType;
        Instruction.DirectOrIndirect directOrIndirect;
        if (parts[3] == "DIRECT")
        {
            long addressOfInstruction = Convert.ToInt64(parts[5], 16);
            directOrIndirect = Instruction.DirectOrIndirect.Direct;

            if (parts[4] == "VALUE")
            {
                operationType = Instruction.OperationType.Value;
            }
            else if (parts[4] == "REGISTER_VALUE")
            {
                operationType = Instruction.OperationType.RegisterValue;
            }
            else
            {
                throw new ArgumentException("Invalid operation type specified.");
            }
            BranchingOperations cmp = new BranchingOperations(operationName, indexOfRegister, valueOrRegister, directOrIndirect, operationType);
            processorEmulator.addInstructionToMemory(cmp, addressOfInstruction);

        }
        else if (parts[3] == "INDIRECT")
        {
            long addressOfInstruction = Convert.ToInt64(parts[4], 16);
            directOrIndirect = Instruction.DirectOrIndirect.Indirect;
            BranchingOperations cmp = new BranchingOperations(operationName, indexOfRegister, valueOrRegister, directOrIndirect);
            processorEmulator.addInstructionToMemory(cmp, addressOfInstruction);

        }
        else
        {
            throw new ArgumentException("Invalid operation type specified.");
        }
    }
    else if (operationName == "INPUT" || operationName == "OUTPUT")
    {
        int indexOfRegister = int.Parse(parts[1]);
        long address = Convert.ToInt64(parts[2], 16);
        InputOutput inputOutput = new InputOutput(operationName, indexOfRegister);
        processorEmulator.addInstructionToMemory(inputOutput, address);
    }
    else if (operationName == "HALT")
    {
        long address = Convert.ToInt64(parts[1], 16);
        Instruction instruction = new Instruction(operationName);
        processorEmulator.addInstructionToMemory(instruction, address);
    }

}
processorEmulator.Run();
processorEmulator.print();

