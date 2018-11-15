namespace UILibrary.Win32.Struct
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [StructLayout(LayoutKind.Sequential)]
    public struct API_MSG
    {
        public IntPtr Hwnd;
        public int Msg;
        public IntPtr WParam;
        public IntPtr LParam;
        public int Time;
        public UILibrary.Win32.Struct.POINT Pt;
        public Message ToMessage()
        {
            return new Message { HWnd = this.Hwnd, Msg = this.Msg, WParam = this.WParam, LParam = this.LParam };
        }

        public void FromMessage(ref Message msg)
        {
            this.Hwnd = msg.HWnd;
            this.Msg = msg.Msg;
            this.WParam = msg.WParam;
            this.LParam = msg.LParam;
        }
    }
}

