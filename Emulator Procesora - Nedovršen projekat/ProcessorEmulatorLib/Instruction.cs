using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulatorLib
{
    public class Instruction
    {
        //Korišteno kako bi se znalo da li je u pitanju direktno ili indirektno adresiranje, kod (Jump,JE,JNE,JGE,JL,CMP)
        public enum DirectOrIndirect
        {
            Direct,
            Indirect
        }
        //Korišteno kako bi se napravila razlika između instrukcija (Jump,JE,JNE,JGE,JL,CMP)(Moguće da je bespotrebno,jer provjeravam instrukcije putem imena)
        public enum OperationType
        {
            Value, // Koristi se kako bi se naznačilo da se radi sa konkretnom vrijednošću.
            RegisterValue, // Koristi se kako bi se naznačilo da se koristi vrijednost registra.
            Data, //Podatak na nekoj adresi koji je u memoriji.
            Address //Samo kod MOV, kako bi se dodala virtuelna adresa u registar za indirektno adresiranje.
        }
        protected string nameOfInstruction;
        protected OperationType type;
        protected DirectOrIndirect directOrIndirect;
        
        public Instruction(string nameOfInstruction, OperationType type)
        {
            this.nameOfInstruction = nameOfInstruction;
            this.type = type;
        }
        public Instruction(string nameOfInstruction, DirectOrIndirect directOrIndirect)
        {
            this.nameOfInstruction = nameOfInstruction;
            this.directOrIndirect = directOrIndirect;
        }
        public Instruction(string nameOfInstruction, DirectOrIndirect directOrIndirect,OperationType type)
        {
            this.nameOfInstruction = nameOfInstruction;
            this.directOrIndirect = directOrIndirect;
            this.type = type;
        }
        public Instruction(string nameOfInstruction)
        {
            this.nameOfInstruction = nameOfInstruction;
        }
        public string getNameOfInstruction()
        {
            return this.nameOfInstruction;
        }
        public OperationType getOperationType()
        {
            return this.type;
        }
        public DirectOrIndirect getDirectOrIndirect()
        {
            return this.directOrIndirect;
        }

    }
}
