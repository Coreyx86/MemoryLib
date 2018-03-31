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
    public static class MemoryLib
    {
        const int PROCESS_VM_READ = 0x0010;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_OPERATION = 0x0008;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        public static byte[] ProcReadMemory(string processName, int address, int readLength)
        {
            Process process = Process.GetProcessesByName(processName)[0];
            IntPtr processHandle = OpenProcess(PROCESS_VM_READ, false, process.Id);

            int bytesRead = 0;
            byte[] buffer = new byte[readLength];

            ReadProcessMemory((int)processHandle, address, buffer, readLength, ref bytesRead);

            return buffer;
        }

        public static void ProcWriteMemory(string processName, int address, byte[] memory)
        {
            Process process = Process.GetProcessesByName(processName)[0];
            IntPtr processHandle = OpenProcess(0x1F0FFF, false, process.Id);

            int bytesWritten = 0;

            WriteProcessMemory((int)processHandle, address, memory, memory.Length, ref bytesWritten);
        }

        public static string processName;

        static MemoryLib()
        {

        }



    }
}
