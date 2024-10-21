using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulatorLib
{
    public class InputOutput:Instruction
    {
        private int indexOfRegister;
        public InputOutput(string nameOfInstruction,int indexOfRegister):base(nameOfInstruction)
        {
            this.indexOfRegister = indexOfRegister;
        }

        public int getIndexOfRegister()
        {
            return indexOfRegister;
        }
    }
}
