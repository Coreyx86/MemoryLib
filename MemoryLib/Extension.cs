using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MemoryLib
{
    public class Extension
    {
        #region IMPORTS
        const int PROCESS_VM_READ = 0x0010;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_OPERATION = 0x0008;

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);
        #endregion

        private string processName;

        public Extension(string _processName)
        {
            processName = _processName;
        }

        private byte[] ProcReadMemory(int address, int readLength)
        {
            Process process = Process.GetProcessesByName(processName)[0];
            IntPtr processHandle = OpenProcess(PROCESS_VM_READ, false, process.Id);

            int bytesRead = 0;
            byte[] buffer = new byte[readLength];

            ReadProcessMemory((int)processHandle, address, buffer, readLength, ref bytesRead);

            return buffer;
        }

        private void ProcWriteMemory(int address, byte[] memory)
        {
            Process process = Process.GetProcessesByName(processName)[0];
            IntPtr processHandle = OpenProcess(0x1F0FFF, false, process.Id);

            int bytesWritten = 0;

            WriteProcessMemory((int)processHandle, address, memory, memory.Length, ref bytesWritten);
        }

        //Reading Functions
        #region READING
        public byte[] ReadBytes(int address, int readLen)
        {
            return ProcReadMemory(address, readLen);
        }
        public UInt16 ReadUInt16(int address)
        {
            return BitConverter.ToUInt16(ProcReadMemory(address, 2), 0);
        }
        public UInt32 ReadUInt(int address)
        {
            return BitConverter.ToUInt32(ProcReadMemory(address, 4), 0);
        }
        public UInt64 ReadUInt64(int address)
        {
            return BitConverter.ToUInt64(ProcReadMemory(address, 8), 0);
        }
        public Int16 ReadInt16(int address)
        {
            return BitConverter.ToInt16(ProcReadMemory(address, 2), 0);
        }
        public Int32 ReadInt(int address)
        {
            return BitConverter.ToInt32(ProcReadMemory(address, 4), 0);
        }
        public Int64 ReadInt64(int address)
        {
            return BitConverter.ToInt64(ProcReadMemory(address, 8), 0);
        }
        public float ReadFloat(int address)
        {
            return BitConverter.ToSingle(ProcReadMemory(address, 4), 0);
        }
        public double ReadDouble(int address)
        {
            return BitConverter.ToDouble(ProcReadMemory(address, 8), 0);
        }
        public byte ReadByte(int address)
        {
            return ProcReadMemory(address, 1)[0];
        }
        public SByte ReadSByte(int address)
        {
            return (SByte)ProcReadMemory(address, 1)[0];
        }
        public bool ReadBool(int address)
        {
            return ProcReadMemory(address, 1)[0] != 0;
        }
        public string ReadString(int address)
        {
            int blocksize = 40;
            int scalesize = 0;
            string str = string.Empty;

            while (!str.Contains('\0'))
            {
                byte[] buffer = ProcReadMemory(address + scalesize, blocksize);
                str += Encoding.UTF8.GetString(buffer);
                scalesize += blocksize;
            }

            return str.Substring(0, str.IndexOf('\0'));
        }

        public float[] ReadVec2(int address)
        {
            float[] buff = new float[2];
            buff[0] = ReadFloat(address);
            buff[1] = ReadFloat(address + 4);
            return buff;
        }
        public float[] ReadVec3(int address)
        {
            float[] buff = new float[3];
            buff[0] = ReadFloat(address);
            buff[1] = ReadFloat(address + 4);
            buff[2] = ReadFloat(address + 8);
            return buff;
        }
        public float[] ReadVec4(int address)
        {
            float[] buff = new float[4];
            buff[0] = ReadFloat(address);
            buff[1] = ReadFloat(address + 4);
            buff[2] = ReadFloat(address + 8);
            buff[3] = ReadFloat(address + 12);
            return buff;
        }
        #endregion

        //Write
        #region WRITING
        public void WriteBytes(int address, byte[] write)
        {
            ProcWriteMemory(address, write);
        }
        public void WriteUInt16(int address, UInt16 write)
        {
            ProcWriteMemory(address, BitConverter.GetBytes(write));
        }
        public void WriteUInt32(int address, uint write)
        {
            ProcWriteMemory(address, BitConverter.GetBytes(write));
        }
        public void WriteUInt64(int address, UInt64 write)
        {
            ProcWriteMemory(address, BitConverter.GetBytes(write));
        }
        public void WriteInt16(int address, Int16 write)
        {
            ProcWriteMemory(address, BitConverter.GetBytes(write));
        }
        public void WriteInt32(int address, int write)
        {
            ProcWriteMemory(address, BitConverter.GetBytes(write));
        }
        public void WriteInt64(int address, Int64 write)
        {
            ProcWriteMemory(address, BitConverter.GetBytes(write));
        }
        public void WriteFloat(int address, float write)
        {
            ProcWriteMemory(address, BitConverter.GetBytes(write));
        }
        public void WriteDouble(int address, double write)
        {
            ProcWriteMemory(address, BitConverter.GetBytes(write));
        }
        public void WriteString(int address, string write)
        {
            string tmp = write + '\0';
            byte[] bWrite = System.Text.Encoding.ASCII.GetBytes(tmp);
            ProcWriteMemory(address, bWrite);
        }

        public void WriteVec2(int address, float[] write)
        {
            WriteFloat(address, write[0]);
            WriteFloat(address + 4, write[1]);
        }
        public void WriteVec3(int address, float[] write)
        {
            WriteFloat(address, write[0]);
            WriteFloat(address + 4, write[1]);
            WriteFloat(address + 8, write[2]);
        }
        public void WriteVec4(int address, float[] write)
        {
            WriteFloat(address, write[0]);
            WriteFloat(address + 4, write[1]);
            WriteFloat(address + 8, write[2]);
            WriteFloat(address + 12, write[3]);
        }
        #endregion
    }
}
