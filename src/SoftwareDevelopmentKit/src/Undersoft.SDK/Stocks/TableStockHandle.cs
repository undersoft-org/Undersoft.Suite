using Undersoft.SDK.Extracting;

namespace Undersoft.SDK.Stocks
{
    using System;
    using System.Runtime.InteropServices;

    public class TableStockHandle
    {
        #region Fields

        public int sizeStruct = 0;
        public Type typeStruct = null;

        #endregion

        #region Constructors

        public TableStockHandle(Type t)
        {
            typeStruct = t;
            sizeStruct = Marshal.SizeOf(t);
        }

        #endregion

        #region Methods

        public unsafe void* GetPtr(object[] structure)
        {
            Type gg = structure.GetType();
            GCHandle pinn = GCHandle.Alloc(structure, GCHandleType.Pinned);
            nint address = Marshal.UnsafeAddrOfPinnedArrayElement(structure, 0);
            return address.ToPointer();
        }


        public object PtrToStructure(nint pointer)
        {
            return Extract.PointerToStructure(pointer, typeStruct, 0);
        }

        public void PtrToStructure(nint pointer, object structure)
        {
            if (structure != null)
                Extract.PointerToStructure(pointer, structure);
            else
                structure = Extract.PointerToStructure(pointer, typeStruct, 0);
        }

        public unsafe void ReadArray(ref object[] buffer, byte* source, int index, int count)
        {
            if (index < 0) index = 0;
            int bufferIndex = index;
            if (buffer == null) { buffer = new object[count]; bufferIndex = 0; }
            else if (buffer.Length - index < count) { count = buffer.Length - index; }
            if (count < 0) throw new ArgumentOutOfRangeException("count");

            int offset = index * sizeStruct;
            for (int i = 0; i < count; i++)
            {
                int arrayoffset = i + bufferIndex;
                if (buffer[arrayoffset] != null)
                    Extract.PointerToStructure(source + (i * sizeStruct + offset), buffer[arrayoffset]);
                else
                    buffer[arrayoffset] = Extract.PointerToStructure(source, typeStruct, i * sizeStruct + offset);
            }
        }

        public unsafe void ReadArray(ref object[] buffer, int destIndex, byte* source, int index, int count)
        {
            if (index < 0) index = 0;
            if (buffer == null) { buffer = new object[count]; destIndex = 0; }
            else if (buffer.Length - destIndex - index < count)
                count = buffer.Length - destIndex - index;
            if (count < 0) throw new ArgumentOutOfRangeException("count");

            int offset = index * sizeStruct;

            for (int i = 0; i < count; i++)
            {
                int arrayoffset = i + destIndex;
                if (buffer[arrayoffset] != null)
                    Extract.PointerToStructure(source + (i * sizeStruct + offset), buffer[arrayoffset]);
                else
                    buffer[arrayoffset] = Extract.PointerToStructure(source, typeStruct, i * sizeStruct + offset);
            }
        }

        public unsafe void ReadArray(ref object[] buffer, nint source, int index, int count)
        {
            if (index < 0) index = 0;
            int bufferIndex = index;
            if (buffer == null) { buffer = new object[count]; bufferIndex = 0; }
            else if (buffer.Length - index < count) { count = buffer.Length - index; }
            if (count < 0) throw new ArgumentOutOfRangeException("count");

            int offset = index * sizeStruct;

            for (int i = 0; i < count; i++)
            {
                int arrayoffset = i + bufferIndex;
                if (buffer[arrayoffset] != null)
                    Extract.PointerToStructure(source + (i * sizeStruct + offset), buffer[arrayoffset]);
                else
                    buffer[arrayoffset] = Extract.PointerToStructure(source, typeStruct, i * sizeStruct + offset);
            }
        }

        public int SizeOf(object t)
        {
            return sizeStruct;
        }

        public unsafe void StructureToPtr(object s, nint pointer)
        {
            s.StructureTo((byte*)pointer);
        }

        public unsafe void WriteArray(byte* destination, int srcIndex, ref object[] buffer, int index, int count)
        {
            if (index < 0) index = 0;
            if (buffer == null) throw new ArgumentNullException("buffer");
            if (count < 0) throw new ArgumentOutOfRangeException("count");
            if (buffer.Length - index < count) count = buffer.Length - index;

            int offset = index * sizeStruct;

            for (int i = 0; i < count; i++)
            {
                int arrayoffset = i + srcIndex;
                Extract.StructureToPointer(buffer[arrayoffset], destination + (i * sizeStruct + offset));
            }
        }

        public unsafe void WriteArray(byte* destination, ref object[] buffer, int index, int count)
        {

            if (index < 0) index = 0;
            int bufferIndex = index;
            if (buffer == null) throw new ArgumentNullException("buffer");
            if (count < 0) throw new ArgumentOutOfRangeException("count");
            if (buffer.Length - index < count) count = buffer.Length - index;

            int offset = index * sizeStruct;

            for (int i = 0; i < count; i++)
            {
                int arrayoffset = i + bufferIndex;
                Extract.StructureToPointer(buffer[arrayoffset], destination + (i * sizeStruct + offset));
            }
        }

        public unsafe void WriteArray(nint destination, ref object[] buffer, int index, int count)
        {
            if (index < 0) index = 0;
            int bufferIndex = index;
            if (buffer == null) throw new ArgumentNullException("buffer");
            if (count < 0) throw new ArgumentOutOfRangeException("count");
            if (buffer.Length - index < count) count = buffer.Length - index;

            int offset = index * sizeStruct;

            for (int i = 0; i < count; i++)
            {
                int arrayoffset = i + bufferIndex;
                Extract.StructureToPointer(buffer[arrayoffset], destination + (i * sizeStruct + offset));
            }
        }

        #endregion
    }
}
