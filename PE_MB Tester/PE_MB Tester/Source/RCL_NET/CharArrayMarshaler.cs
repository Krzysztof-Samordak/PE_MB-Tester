/******************************************************
 *       Copyright Keysight Technologies 2018-2021
 ******************************************************/
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Keysight.ApiCoreLibraries
{
    /// <summary>
    /// CharArrayMarshaler is a class to converting from/to byte[] to char[].
    /// Note that this class assume the char[] is in the ASCII character range or in the Unicode C0 Controls and Basic Latin, and C1 Controls and Latin-1 Supplement ranges, from U+0000 to U+00FF.
    /// If the character is out of this range, an OverflowException will be thrown.
    /// </summary>
    internal class CharArrayMarshaler: ICustomMarshaler<Char[]>
    {
        private BlittableArrayMarshaler<Byte> mByteArrayMarshaler = new BlittableArrayMarshaler<Byte>();

        public Int32 BufferSize
        {
            get { return mByteArrayMarshaler.BufferSize; }
        }

        public void CSharpToBytes(Char[] input, MarshalBuffer marshalBuffer)
        {
            var interimBytes = default(Byte[]);
            if(input != null)
            {
                interimBytes = new Byte[input.Length];
                try
                {
                    interimBytes = Array.ConvertAll(input, s => Convert.ToByte(s));
                }
                catch (System.OverflowException e)
                {
                    throw new System.SystemException("CharArrayMarshaler only support 8-bit characters", e);
                }
            }

            mByteArrayMarshaler.CSharpToBytes(interimBytes, marshalBuffer);
            marshalBuffer.HelperObjects.Enqueue(interimBytes);
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref Char[] result)
        {
            // The data may updated to interimBytes
            var interimBytes = default(Byte[]);
            if(marshalBuffer.HelperObjects.Count > 0)
            {
                interimBytes = marshalBuffer.HelperObjects.Dequeue() as Byte[];
            }                
            mByteArrayMarshaler.BytesToCSharp(marshalBuffer, ref interimBytes);
            if(interimBytes != null)
            {
                result = Array.ConvertAll(interimBytes, s => Convert.ToChar(s));
            }
        }
    }
}