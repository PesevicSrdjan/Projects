using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulatorLib
{
    public class Data
    {
        private long value;

        public Data(long value)
        {
            this.value = value;

        }
        public long getValue()
        {
            return this.value;
        }
        public void setValue(long value)
        {
            this.value = value;
        }


    }
}
