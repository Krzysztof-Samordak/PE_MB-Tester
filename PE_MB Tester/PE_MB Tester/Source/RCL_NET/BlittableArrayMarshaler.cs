/******************************************************
 *       Copyright Keysight Technologies 2018-2021
 ******************************************************/
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Keysight.ApiCoreLibraries
{
    internal class BlittableArrayMarshaler<T> : ICustomMarshaler<T[]> where T : struct
    {
        private Int64Marshaler mInt64Marshaler = new Int64Marshaler();

        //https://docs.microsoft.com/en-us/dotnet/framework/interop/blittable-and-non-blittable-types
        private static List<string> mBlittableTypes = new List<string>{
            "System.Byte",
            "System.SByte",
            "System.Int16",
            "System.UInt16",
            "System.Int32",
            "System.UInt32",
            "System.Int64",
            "System.UInt64",
            "System.IntPtr",
            "System.UIntPtr",
            "System.Single",
            "System.Double" 
            };

        
        public BlittableArrayMarshaler()
        {
            if (!mBlittableTypes.Contains(typeof(T).FullName))
            {
                throw new ArgumentException(string.Format("{0} is not a blittable type so it can not use BlittableArrayMarshaler", typeof(T).FullName));
            }
        }

        public Int32 BufferSize
        {
            get { return 3 * mInt64Marshaler.BufferSize; }
        }

        public void CSharpToBytes(T[] input, MarshalBuffer marshalBuffer)
        {
            Int64 addressOfBuffer = 0;
            Int64 len = 0;

            //Support null input, if input is null, the address and length are set to 0
            if (input != null)
            {
                GCHandle pinnedBufferHandle = GCHandle.Alloc(input, GCHandleType.Pinned);
                marshalBuffer.GCHandles.Add(pinnedBufferHandle);

                addressOfBuffer = pinnedBufferHandle.AddrOfPinnedObject().ToInt64();
                len = input.Length;
            }

            mInt64Marshaler.CSharpToBytes(addressOfBuffer, marshalBuffer);
            mInt64Marshaler.CSharpToBytes(len, marshalBuffer);
            //Convert the allocateMemory delegate -> IntPtr -> Int64
            //var funcPtrInt = marshalBuffer.AllocateMemory.ToInt64();            
            var funcPtrInt = Marshal.GetFunctionPointerForDelegate(AllocationHelper.GetAllocateMemoryDelegate<T>()).ToInt64();
            mInt64Marshaler.CSharpToBytes(funcPtrInt, marshalBuffer);
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref T[] result)
        {
            Int64 memory = 0;
            Int64 len = 0;
            Int64 dummyDelegate = 0;
            mInt64Marshaler.BytesToCSharp(marshalBuffer, ref memory);
            mInt64Marshaler.BytesToCSharp(marshalBuffer, ref len);
            mInt64Marshaler.BytesToCSharp(marshalBuffer, ref dummyDelegate);

            //If the original allocated memory is not big enough, Cmi will allocate a new memory
            //If marshalBuffer.RetrieveMemory returns a null object, it means no new memory is allocated, so the original memory is used.
            var id = (IntPtr)memory;
            Object allocatedMemory = AllocationHelper.RetrieveMemory(id);
            if (allocatedMemory != null)
            {
                result = (T[])allocatedMemory;
            }

        }
    }
}