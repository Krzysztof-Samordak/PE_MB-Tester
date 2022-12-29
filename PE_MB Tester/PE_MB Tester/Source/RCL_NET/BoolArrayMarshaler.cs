/******************************************************
 *       Copyright Keysight Technologies 2018-2021
 ******************************************************/
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Keysight.ApiCoreLibraries
{
    internal class BoolArrayMarshaler : ICustomMarshaler<Boolean[]>
    {
        private BlittableArrayMarshaler<Int32> mInt32ArrayMarshaler = new BlittableArrayMarshaler<Int32>();
        private Int32[] mInt32ArrayBuffer;
             

        public Int32 BufferSize
        {
            get { return mInt32ArrayMarshaler.BufferSize; }
        }

        public void CSharpToBytes(Boolean[] input, MarshalBuffer marshalBuffer)
        {
            if (input != null)
            {
                mInt32ArrayBuffer = Array.ConvertAll(input, value => Convert.ToInt32(value));
            }
            mInt32ArrayMarshaler.CSharpToBytes(mInt32ArrayBuffer, marshalBuffer);
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref Boolean[] result)
        {
            mInt32ArrayMarshaler.BytesToCSharp(marshalBuffer, ref mInt32ArrayBuffer);
            result = Array.ConvertAll(mInt32ArrayBuffer, value => value != Convert.ToInt32(false));
        }
    }
}