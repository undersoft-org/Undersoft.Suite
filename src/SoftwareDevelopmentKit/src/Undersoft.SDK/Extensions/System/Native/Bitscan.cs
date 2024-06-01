using System.Runtime.InteropServices;

namespace System
{
    public static class Bitscan
    {
        [DllImport("Undersoft.SDK.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ReverseIndex32(uint x);

        [DllImport("Undersoft.SDK.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ForwardIndex32(uint x);

        [DllImport("Undersoft.SDK.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint LengthBefore32(uint x);

        [DllImport("Undersoft.SDK.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint LengthAfter32(uint x);

        [DllImport("Undersoft.SDK.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ReverseIndex64(ulong x);

        [DllImport("Undersoft.SDK.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ForwardIndex64(ulong x);

        [DllImport("Undersoft.SDK.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint LengthBefore64(ulong x);

        [DllImport("Undersoft.SDK.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint LengthAfter64(ulong x);
    }
}
