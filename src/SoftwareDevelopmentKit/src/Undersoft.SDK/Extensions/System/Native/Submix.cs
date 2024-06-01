﻿using System.Runtime.InteropServices;

/*************************************************************************************
    Copyright (c) 2020 Undersoft

    System.Submix
              
    @author Darius Hanc                                                  
    @project NETStandard.Undersoft.SDK                      
    @version 0.7.1.r.d (Feb 7, 2021)                                            
    @licence MIT                                       
 *********************************************************************************/
namespace System
{
    public static class Submix
    {
        [DllImport("Undersoft.SDK.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint SubmixMap32(uint src, uint dest);

        [DllImport("Undersoft.SDK.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint SubmixMapToMask32(uint src, uint dest, uint bitmask, int msbid);

        [DllImport("Undersoft.SDK.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint SubmixMask32(uint dest);

        [DllImport("Undersoft.SDK.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int SubmixMsbId32(uint dest);

        [DllImport("Undersoft.SDK.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ulong SubmixMap64(ulong src, ulong dest);

        [DllImport("Undersoft.SDK.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ulong SubmixMapToMask64(ulong src, ulong dest, ulong bitmask, int msbid);

        [DllImport("Undersoft.SDK.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ulong SubmixMask64(ulong dest);

        [DllImport("Undersoft.SDK.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int SubmixMsbId64(ulong dest);

        public static uint Map(uint src, uint dest)
        {
            return SubmixMap32(src, dest);

            //   uint submix = 0,
            //   _src = src;

            //   int msbid = (int)ReverseIndex32(dest);
            //   uint bitmask = 0xFFFFFFFF >> (31 - msbid);

            //   do
            //   {

            //     submix += (_src & bitmask);
            //       if (submix > dest)
            //           submix -= dest;
            //       _src >>= msbid;

            //   } while (_src > 0)

            //   return submix;
        }
        public static uint Map(int src, int dest)
        {
            return SubmixMap32((uint)src, (uint)dest);
        }
        public static uint Map(uint src, uint dest, uint bitmask, int msbid)
        {
            return SubmixMapToMask32(src, dest, bitmask, msbid);
            //   uint submix = 0,
            //   _src = src;           

            //   do
            //   {

            //     submix += (_src & bitmask);
            //       if (submix > dest)
            //           submix -= dest;
            //       _src >>= msbid;

            //   } while (_src > 0)

            //   return submix;
        }
        public static uint Map(int src, int dest, uint bitmask, int msbid)
        {
            return SubmixMapToMask32((uint)src, (uint)dest, bitmask, msbid);
        }
        public static uint Mask(int dest)
        {
            return SubmixMask32((uint)dest);
            //int msbid = (int)Bitscan.ReverseIndex32((uint)dest);
            //return 0xFFFFFFFF >> (31 - msbid);
        }
        public static uint Mask(uint dest)
        {
            return SubmixMask32(dest);
            //int msbid = (int)Bitscan.ReverseIndex32(dest);
            //return  0xFFFFFFFF >> (31 - msbid);
        }
        public static int MsbId(int dest)
        {
            return SubmixMsbId32((uint)dest);
        }
        public static int MsbId(uint dest)
        {
            return SubmixMsbId32(dest);
        }

        public static ulong Map(ulong src, ulong dest)
        {
            return SubmixMap64(src, dest);

            //   ulong submix = 0,
            //   _src = src;

            //   long msbid = (int)ReverseIndex32(dest);
            //   ulong bitmask = 0xFFFFFFFF >> (31 - msbid);

            //   do
            //   {

            //     submix += (_src & bitmask);
            //       if (submix > dest)
            //           submix -= dest;
            //       _src >>= msbid;

            //   } while (_src > 0)

            //   return submix;
        }
        public static ulong Map(long src, long dest)
        {
            return SubmixMap64((ulong)src, (ulong)dest);
        }
        public static ulong Map(ulong src, ulong dest, ulong bitmask, int msbid)
        {
            return SubmixMapToMask64(src, dest, bitmask, msbid);

            //   ulong submix = 0,
            //   _src = src;

            //   do
            //   {

            //     submix += (_src & bitmask);
            //       if (submix > dest)
            //           submix -= dest;
            //       _src >>= msbid;

            //   } while (_src > 0)

            //   return submix;
        }
        public static ulong Map(long src, long dest, ulong bitmask, int msbid)
        {
            return SubmixMapToMask64((ulong)src, (ulong)dest, bitmask, msbid);
        }
        public static ulong Mask(long dest)
        {
            return SubmixMask64((ulong)dest);
            //int msbId = (int)Bitscan.ReverseIndex64((ulong)dest);
            //return 0xFFFFFFFFFFFFFFFF >> (63 - msbId);
        }
        public static ulong Mask(ulong dest)
        {
            return SubmixMask64(dest);
            //int msbId = (int)Bitscan.ReverseIndex64(dest);
            //return 0xFFFFFFFFFFFFFFFF >> (63 - msbId);
        }
        public static int MsbId(long dest)
        {
            return SubmixMsbId64((ulong)dest);
        }
        public static int MsbId(ulong dest)
        {
            return SubmixMsbId64(dest);
        }
    }
}
