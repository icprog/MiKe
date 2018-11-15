namespace UILibrary.Win32
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public static class Helper
    {
        public static int HIWORD(int n)
        {
            return ((n >> 0x10) & 0xffff);
        }

        public static int HIWORD(IntPtr n)
        {
            return HIWORD((int) ((long) n));
        }

        public static bool LeftKeyPressed()
        {
            if (SystemInformation.MouseButtonsSwapped)
            {
                return (Win32.NativeMethods.GetKeyState(2) < 0);
            }
            return (Win32.NativeMethods.GetKeyState(1) < 0);
        }

        public static int LOWORD(int n)
        {
            return (n & 0xffff);
        }

        public static int LOWORD(IntPtr n)
        {
            return LOWORD((int) ((long) n));
        }

        public static int MAKELONG(int low, int high)
        {
            return ((high << 0x10) | (low & 0xffff));
        }

        public static IntPtr MAKELPARAM(int low, int high)
        {
            return (IntPtr) ((high << 0x10) | (low & 0xffff));
        }

        public static void SetRedraw(IntPtr hWnd, bool redraw)
        {
            IntPtr wParam = redraw ? Result.TRUE : Result.FALSE;
            Win32.NativeMethods.SendMessage(hWnd, 11, wParam, 0);
        }

        public static int SignedHIWORD(int n)
        {
            return (short) ((n >> 0x10) & 0xffff);
        }

        public static int SignedHIWORD(IntPtr n)
        {
            return SignedHIWORD((int) ((long) n));
        }

        public static int SignedLOWORD(int n)
        {
            return (short) (n & 0xffff);
        }

        public static int SignedLOWORD(IntPtr n)
        {
            return SignedLOWORD((int) ((long) n));
        }

        public static void Swap(ref int x, ref int y)
        {
            int num = x;
            x = y;
            y = num;
        }

        public static IntPtr ToIntPtr(object structure)
        {
            IntPtr zero = IntPtr.Zero;
            zero = Marshal.AllocCoTaskMem(Marshal.SizeOf(structure));
            Marshal.StructureToPtr(structure, zero, false);
            return zero;
        }
    }
}

