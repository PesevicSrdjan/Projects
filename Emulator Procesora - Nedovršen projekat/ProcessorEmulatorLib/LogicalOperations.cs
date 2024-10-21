using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulatorLib
{
    public class LogicalOperations:Instruction
    {
        private int indexOfRegister;
        private long value;

        public LogicalOperations(string nameOfInstruction, int indexOfRegister, long value, OperationType type) : base(nameOfInstruction, type)
        {
            this.indexOfRegister = indexOfRegister;
            this.value = value;
        }
        public LogicalOperations(string nameOfInstruction, int indexOfRegister):base(nameOfInstruction)
        {
            this.nameOfInstruction = nameOfInstruction;
            this.indexOfRegister = indexOfRegister;
        }

        public int getIndexOfRegister()
        {
            return this.indexOfRegister;
        }
        public long getValue()
        {
            return this.value;
        }
    }
}
