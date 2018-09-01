using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MemoryLib
{
    /*
        MemoryLib V1 By Corey Nelson 2018.

        This library was made to simplify the use of Reading/Writing process memory in real time. 
     */
     public class MemoryLib
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
        public string ProcessName { get { return processName; } set { if (value != null || value != string.Empty) { processName = value; } } }

        private Extension extension;
        public Extension Extension { get { return extension; } }

        public MemoryLib(string _processName)
        {
            processName = _processName;
            extension = new Extension(_processName);
        }

        public byte[] ProcReadMemory(int address, int readLength)
        {
            Process process = Process.GetProcessesByName(processName)[0];
            IntPtr processHandle = OpenProcess(PROCESS_VM_READ, false, process.Id);

            int bytesRead = 0;
            byte[] buffer = new byte[readLength];

            ReadProcessMemory((int)processHandle, address, buffer, readLength, ref bytesRead);

            return buffer;
        }

        public void ProcWriteMemory(int address, byte[] memory)
        {
            Process process = Process.GetProcessesByName(processName)[0];
            IntPtr processHandle = OpenProcess(0x1F0FFF, false, process.Id);

            int bytesWritten = 0;

            WriteProcessMemory((int)processHandle, address, memory, memory.Length, ref bytesWritten);
        }
    }
}
