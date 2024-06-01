namespace System
{
    using System.Runtime.CompilerServices;

    public static class UniqueTetrahexExtensions
    {
        private static readonly char[] _base64 = new[]{
            '0','1','2','3','4','5','6','7','8','9','A',
            'B','C','D','E','F','G','H','I','J','K','a',
            'b','c','d','e','f','g','h','i','j','k','L',
            'M','N','O','P','Q','R','S','T','U','V','W',
            'X','Y','Z','l','m','n','o','p','q','r','s',
            't','u','v','w','x','y','z','-','.'};


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte ToTetrahexByte(this char phchar)
        {
            if (phchar <= '.')
                return (byte)(phchar + 17);
            else if (phchar <= '9')
                return (byte)(phchar - 48);
            else if (phchar <= 'Z')
                return (byte)(phchar - 55);
            return (byte)(phchar - 61);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char ToTetrahexChar(this byte phbyte)
        {
            if (phbyte <= 9)
                return (char)(phbyte + 48);
            else if (phbyte <= 35)
                return (char)(phbyte + 55);
            else if (phbyte <= 61)
                return (char)(phbyte + 61);
            return (char)(phbyte - 17);
        }

    }
}
