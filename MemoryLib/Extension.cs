using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryLib
{
    public class Extension
    {
        //Reading Functions
        public static byte[] ReadBytes(int address, int readLen)
        {
            return MemoryLib.ProcReadMemory(MemoryLib.processName, address, readLen);
        }
        public static UInt16 ReadUInt16(int address)
        {
            return BitConverter.ToUInt16(MemoryLib.ProcReadMemory(MemoryLib.processName, address, 2), 0);
        }
        public static UInt32 ReadUInt(int address)
        {
            return BitConverter.ToUInt32(MemoryLib.ProcReadMemory(MemoryLib.processName, address, 4), 0);
        }
        public static UInt64 ReadUInt64(int address)
        {
            return BitConverter.ToUInt64(MemoryLib.ProcReadMemory(MemoryLib.processName, address, 8), 0);
        }
        public static Int16 ReadInt16(int address)
        {
            return BitConverter.ToInt16(MemoryLib.ProcReadMemory(MemoryLib.processName, address, 2), 0);
        }
        public static Int32 ReadInt(int address)
        {
            return BitConverter.ToInt32(MemoryLib.ProcReadMemory(MemoryLib.processName, address, 4), 0);
        }
        public static Int64 ReadInt64(int address)
        {
            return BitConverter.ToInt64(MemoryLib.ProcReadMemory(MemoryLib.processName, address, 8), 0);
        }
        public static float ReadFloat(int address)
        {
            return BitConverter.ToSingle(MemoryLib.ProcReadMemory(MemoryLib.processName, address, 4), 0);
        }
        public static double ReadDouble(int address)
        {
            return BitConverter.ToDouble(MemoryLib.ProcReadMemory(MemoryLib.processName, address, 8), 0);
        }
        public static byte ReadByte(int address)
        {
            return MemoryLib.ProcReadMemory(MemoryLib.processName, address, 1)[0];
        }
        public static SByte ReadSByte(int address)
        {
            return (SByte)MemoryLib.ProcReadMemory(MemoryLib.processName, address, 1)[0];
        }
        public static bool ReadBool(int address)
        {
            return MemoryLib.ProcReadMemory(MemoryLib.processName, address, 1)[0] != 0;
        }
        public static string ReadString(int address)
        {
            int blocksize = 40;
            int scalesize = 0;
            string str = string.Empty;

            while (!str.Contains('\0'))
            {
                byte[] buffer = MemoryLib.ProcReadMemory(MemoryLib.processName, address + scalesize, blocksize);
                str += Encoding.UTF8.GetString(buffer);
                scalesize += blocksize;
            }

            return str.Substring(0, str.IndexOf('\0'));
        }

        public static float[] ReadVec2(int address)
        {
            float[] buff = new float[2];
            buff[0] = ReadFloat(address);
            buff[1] = ReadFloat(address + 4);
            return buff;
        }
        public static float[] ReadVec3(int address)
        {
            float[] buff = new float[3];
            buff[0] = ReadFloat(address);
            buff[1] = ReadFloat(address + 4);
            buff[2] = ReadFloat(address + 8);
            return buff;
        }
        public static float[] ReadVec4(int address)
        {
            float[] buff = new float[4];
            buff[0] = ReadFloat(address);
            buff[1] = ReadFloat(address + 4);
            buff[2] = ReadFloat(address + 8);
            buff[3] = ReadFloat(address + 12);
            return buff;
        }

        //Write
        public static void WriteBytes(int address, byte[] write)
        {
            MemoryLib.ProcWriteMemory(MemoryLib.processName, address, write);
        }
        public static void WriteByte(int address, byte write)
        {
            byte[] tmp = { write };
            WriteBytes(address, tmp);
        }
        public static void WriteUInt16(int address, UInt16 write)
        {
            MemoryLib.ProcWriteMemory(MemoryLib.processName, address, BitConverter.GetBytes(write));
        }
        public static void WriteUInt32(int address, uint write)
        {
            MemoryLib.ProcWriteMemory(MemoryLib.processName, address, BitConverter.GetBytes(write));
        }
        public static void WriteUInt64(int address, UInt64 write)
        {
            MemoryLib.ProcWriteMemory(MemoryLib.processName, address, BitConverter.GetBytes(write));
        }
        public static void WriteInt16(int address, Int16 write)
        {
            MemoryLib.ProcWriteMemory(MemoryLib.processName, address, BitConverter.GetBytes(write));
        }
        public static void WriteInt32(int address, int write)
        {
            MemoryLib.ProcWriteMemory(MemoryLib.processName, address, BitConverter.GetBytes(write));
        }
        public static void WriteInt64(int address, Int64 write)
        {
            MemoryLib.ProcWriteMemory(MemoryLib.processName, address, BitConverter.GetBytes(write));
        }
        public static void WriteFloat(int address, float write)
        {
            MemoryLib.ProcWriteMemory(MemoryLib.processName, address, BitConverter.GetBytes(write));
        }
        public static void WriteDouble(int address, double write)
        {
            MemoryLib.ProcWriteMemory(MemoryLib.processName, address, BitConverter.GetBytes(write));
        }
        public static void WriteString(int address, string write)
        {
            string tmp = write + '\0';
            byte[] bWrite = System.Text.Encoding.ASCII.GetBytes(tmp);
            MemoryLib.ProcWriteMemory(MemoryLib.processName, address, bWrite);
        }

        public static void WriteVec2(int address, float[] write)
        {
            WriteFloat(address, write[0]);
            WriteFloat(address + 4, write[1]);
        }
        public static void WriteVec3(int address, float[] write)
        {
            WriteFloat(address, write[0]);
            WriteFloat(address + 4, write[1]);
            WriteFloat(address + 8, write[2]);
        }
        public static void WriteVec4(int address, float[] write)
        {
            WriteFloat(address, write[0]);
            WriteFloat(address + 4, write[1]);
            WriteFloat(address + 8, write[2]);
            WriteFloat(address + 12, write[3]);
        }
    }
}
