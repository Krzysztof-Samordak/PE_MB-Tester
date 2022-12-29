/******************************************************
 *       Copyright Keysight Technologies 2018-2021
 ******************************************************/
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Keysight.ApiCoreLibraries
{
    internal class EnumArrayMarshaler<T> : ICustomMarshaler<T[]> where T : struct, IConvertible
    {
        private BlittableArrayMarshaler<Int32> mInt32ArrayMarshaler = new BlittableArrayMarshaler<Int32>();
        

        /// <summary>
        /// Check the T should be Enum type in constructor
        /// </summary>
        public EnumArrayMarshaler()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type in EnumArrayMarshaler<T>");
            }
        }

        public Int32 BufferSize
        {
            get { return mInt32ArrayMarshaler.BufferSize; }
        }

        public void CSharpToBytes(T[] input, MarshalBuffer marshalBuffer)
        {
            var int32ArrayBuffer = default(Int32[]);
            if(input != null)
            {
                int32ArrayBuffer = Array.ConvertAll(input, value => Convert.ToInt32(value));
            }            
            mInt32ArrayMarshaler.CSharpToBytes(int32ArrayBuffer, marshalBuffer);
            // -the memory int32ArrayBuffer is holding could be reused so it need to be cached
            // -it cannot be cached in this class  (EnumArrayMarshaler) because there could be more EnumArrayMarshaler 
            //  Enum array member in some data structure and the EnumArrayMarshaler many used to handle multiple instance
            //  see https://jira.it.keysight.com/browse/NGCR-686
            marshalBuffer.HelperObjects.Enqueue(int32ArrayBuffer);
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref T[] result)
        {

            var int32ArrayBuffer = default(Int32[]);
            if(marshalBuffer.HelperObjects.Count > 0)
            {
                int32ArrayBuffer = marshalBuffer.HelperObjects.Dequeue() as Int32[];
            }                
            mInt32ArrayMarshaler.BytesToCSharp(marshalBuffer, ref int32ArrayBuffer);  
            if(int32ArrayBuffer != null)
            {
                result = Array.ConvertAll(int32ArrayBuffer, value => (T)(Object)value);
            }
        }
    }
}