using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulatorLib
{
    public class Memory
    {
        private int frameCount, frameSize;
        private Dictionary<int, byte[]> frames;

        public Memory(int frameCount, int frameSize)
        {
            this.frameCount = frameCount;
            this.frameSize = frameSize;
            frames = new Dictionary<int, byte[]>(frameCount);

            for(int i = 0; i < frameCount; i++)
            {
                frames[i] = new byte[frameSize];
            }

        }
        public void AddFrame(int frameIndex)
        {
            if (!frames.ContainsKey(frameIndex))
            {
                frames.Add(frameIndex, new byte[frameSize]);
                
            }
            
        }
        public byte ReadByte(int frameNumber, int offset)
        {
            return frames[frameNumber][offset];
        }
        public void WriteByte(int frameNumber, int offset, byte value)
        {
            frames[frameNumber][offset] = value;

        }

        public void PrintMemoryContents()
        { 
            foreach(var frame in frames) 
            {
                int i = frame.Key;
                Console.WriteLine($"Frame {frame.Key}:");
                for (int j = 0; j < frameSize; j++)
                {
                   Console.Write(frames[i][j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
