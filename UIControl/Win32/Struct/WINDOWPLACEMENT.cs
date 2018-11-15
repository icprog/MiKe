namespace UILibrary.Win32.Struct
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWPLACEMENT 
    {
        public int length;
        public int flags;
        public int showCmd;
        public Point ptMinPosition;
        public Point ptMaxPosition;
        public UILibrary.Win32.Struct.RECT rcNormalPosition;
        public static WINDOWPLACEMENT Default
        {
            get
            {
                WINDOWPLACEMENT windowplacement =new WINDOWPLACEMENT();
                return new WINDOWPLACEMENT { length = Marshal.SizeOf(windowplacement) };
            }
        }
    }
}

