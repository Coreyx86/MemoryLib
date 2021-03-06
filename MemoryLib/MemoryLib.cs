﻿using System;
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
     public class Memory
    {


        private Extension extension;
        public Extension Extension { get { return extension; } }

        private string processName = string.Empty;
        public string ProcessName
        {
            get
            {
                return processName;
            }
            set
            {
                processName = value;

                if(extension != null)
                {
                    extension.ProcessName = value;
                }
            }
        }

        public Memory(string _processName)
        {
            processName = _processName;
            extension = new Extension(_processName);
        }
    }
}
