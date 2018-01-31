using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LCApp {
    public class ApiCopyFile {
            private const int FO_COPY = 0x0002;
            private const int FOF_ALLOWUNDO = 0x00044;
            //显示进度条  0x00044 // 不显示一个进度对话框 0x0100 显示进度对话框单不显示进度条  0x0002显示进度条和对话框  
            private const int FOF_SILENT = 0x0002;//0x0100;  
            //  
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 0)]
            public struct SHFILEOPSTRUCT {
                public IntPtr hwnd;
                [MarshalAs(UnmanagedType.U4)]
                public int wFunc;
                public string pFrom;
                public string pTo;
                public short fFlags;
                [MarshalAs(UnmanagedType.Bool)]
                public bool fAnyOperationsAborted;
                public IntPtr hNameMappings;
                public string lpszProgressTitle;
            }
            [DllImport("shell32.dll", CharSet = CharSet.Auto)]
            static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);
            public static bool DoCopy(string strSource, string strTarget) {
                SHFILEOPSTRUCT fileop = new SHFILEOPSTRUCT();
                fileop.wFunc = FO_COPY;
                fileop.pFrom = strSource;
                fileop.lpszProgressTitle = "复制大文件";
                fileop.pTo = strTarget;
                //fileop.fFlags = FOF_ALLOWUNDO;  
                fileop.fFlags = FOF_SILENT;
                return SHFileOperation(ref fileop) == 0;
            }
        }
    }

