/******************************************************
 *       Copyright Keysight Technologies 2018-2021
 ******************************************************/
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Keysight.ApiCoreLibraries;

namespace Keysight.ApiCoreLibraries
{
    /// <summary>
    ///  Marshaler 2 dimensional jagged array of blittable types   
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Jagged2DBlittableArrayMarshaler<T> : ICustomMarshaler<T[][]> where T : struct
    {
        private Int64Marshaler mInt64Marshaler = new Int64Marshaler();
        private BlittableArrayMarshaler<T> mArrayMarshaler = new BlittableArrayMarshaler<T>();
        private BlittableArrayMarshaler<Byte> mByteArrayMarshaler = new BlittableArrayMarshaler<Byte>();

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

        
        public Jagged2DBlittableArrayMarshaler()
        {
            if (!mBlittableTypes.Contains(typeof(T).FullName))
            {
                throw new ArgumentException(string.Format("{0} is not a blittable type so it can not use Jagged2DBlittableArrayMarshaler", typeof(T).FullName));
            }
        }

        public Int32 BufferSize
        {
            get { return mByteArrayMarshaler.BufferSize; }
        }

        public void CSharpToBytes(T[][] input, MarshalBuffer marshalBuffer)
        {
            // if input is null, we assume it is for output, as the C++ size need the allocator for allocating .NET memory 
            // We create a dummy object and marshal it to the buffer
            if (input == null)
            {
                input = new T[1][];
            }

            var len = input.Length;
            var bytesLen = len * mArrayMarshaler.BufferSize;
            var innerMarshalBuffer = new MarshalBuffer(bytesLen);                
            for (int i = 0; i < len; i++)
            {
                mArrayMarshaler.CSharpToBytes(input[i], innerMarshalBuffer);
            }
            
            var arraysBytes = new Byte[bytesLen];
            innerMarshalBuffer.ResetOffset();
            Array.Copy(innerMarshalBuffer.Bytes, innerMarshalBuffer.Offset, arraysBytes, 0, bytesLen);
            // Saved the pinned memory handler to marshaler buffer because the innerMarshalBuffer maybe collected
            foreach (var gch in innerMarshalBuffer.GCHandles)
            {
                marshalBuffer.GCHandles.Add(gch);
            }
            marshalBuffer.HelperObjects.Enqueue(arraysBytes);
            mByteArrayMarshaler.CSharpToBytes(arraysBytes, marshalBuffer);            
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref T[][] result)
        {
            var arraysBytes = marshalBuffer.HelperObjects.Dequeue() as Byte[];
            mByteArrayMarshaler.BytesToCSharp(marshalBuffer, ref arraysBytes);
            var innerMarshalBuffer = new MarshalBuffer(arraysBytes.Length);
            innerMarshalBuffer.ResetOffset();
            Array.Copy(arraysBytes, 0, innerMarshalBuffer.Bytes, sizeof(Int32), arraysBytes.Length);
            var rank = arraysBytes.Length / mArrayMarshaler.BufferSize;
            result = new T[rank][];
            for(int i = 0; i < rank; i++)
            {
                mArrayMarshaler.BytesToCSharp(innerMarshalBuffer, ref result[i]);
            }
        }
    }
}