using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProcessorEmulatorLib.Instruction;

namespace ProcessorEmulatorLib
{
    public class BranchingOperations:Instruction
    {

        private long address;
        private int indexOfRegister1, indexOfRegister2;
        public BranchingOperations(string nameOfInstruction, long address,DirectOrIndirect directOrIndirect):base(nameOfInstruction,directOrIndirect)
        {
            this.address = address;
        }
        
        public BranchingOperations(string nameOfInstruction, int indexOfRegister1, int indexOfRegister2,long address, DirectOrIndirect directOrIndirect) : base(nameOfInstruction, directOrIndirect)
        {
            this.indexOfRegister1 = indexOfRegister1;
            this.indexOfRegister2 = indexOfRegister2;
            this.address = address;
        }
        
        public BranchingOperations(string nameOfInstruction, int indexOfRegister1, long address, DirectOrIndirect directOrIndirect,OperationType type) : base(nameOfInstruction, directOrIndirect,type)
        {
            this.indexOfRegister1 = indexOfRegister1;
            this.address = address;
        }
        public BranchingOperations(string nameOfInstruction, int indexOfRegister1, long address, DirectOrIndirect directOrIndirect) : base(nameOfInstruction, directOrIndirect)
        {
            this.indexOfRegister1 = indexOfRegister1;
            this.address = address;
        }
        public long getAddress()
        {
            return this.address;
        }
        
        public int getIndex1()
        {
            return this.indexOfRegister1;
        }
        public int getIndex2()
        {
            return this.indexOfRegister2;
        }
    }
}
