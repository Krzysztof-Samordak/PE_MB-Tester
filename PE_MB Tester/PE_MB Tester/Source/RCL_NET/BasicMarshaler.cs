/******************************************************
 *       Copyright Keysight Technologies 2018-2021
 ******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysight.ApiCoreLibraries
{
    internal class BoolMarshaler : ICustomMarshaler<Boolean>
    {
        public void CSharpToBytes(Boolean input, MarshalBuffer marshalBuffer)
        {
            BitConverter.GetBytes(input).CopyTo(marshalBuffer.Bytes, marshalBuffer.Offset);
            marshalBuffer.Offset += BufferSize;
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref Boolean result)
        {
            result = BitConverter.ToBoolean(marshalBuffer.Bytes, marshalBuffer.Offset);;
            marshalBuffer.Offset += BufferSize;
        }

        public Int32 BufferSize
        {
            get { return sizeof(Boolean); }
        }
    }

    /// <summary>
    /// CharMarshaler is a class to converting from/to byte to char. 
    /// Note that this class assume the char is in the ASCII character range or in the Unicode C0 Controls and Basic Latin, and C1 Controls and Latin-1 Supplement ranges, from U+0000 to U+00FF.
    /// If the character is out of this range, an OverflowException will be thrown.
    /// </summary>
    internal class CharMarshaler : ICustomMarshaler<Char>
    {
        public void CSharpToBytes(Char input, MarshalBuffer marshalBuffer)
        {
            marshalBuffer.Bytes[marshalBuffer.Offset] = Convert.ToByte(input);
            //BitConverter.GetBytes(input).CopyTo(marshalBuffer.Bytes, marshalBuffer.Offset);
            marshalBuffer.Offset += BufferSize;
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref Char result)
        {
            //result = BitConverter.ToChar(marshalBuffer.Bytes, marshalBuffer.Offset);;
            result = Convert.ToChar(marshalBuffer.Bytes[marshalBuffer.Offset]);
            marshalBuffer.Offset += BufferSize;
        }

        public Int32 BufferSize
        {
            get {  return 1; }
        }
    }

    internal class ByteMarshaler : ICustomMarshaler<Byte>
    {
        public void CSharpToBytes(Byte input, MarshalBuffer marshalBuffer)
        {
            marshalBuffer.Bytes[marshalBuffer.Offset] = input;
            marshalBuffer.Offset += BufferSize;
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref Byte result)
        {
            result = marshalBuffer.Bytes[marshalBuffer.Offset];
            marshalBuffer.Offset += BufferSize;
        }

        public Int32 BufferSize
        {
            get { return sizeof(Byte); }
        }
    }

    internal class SByteMarshaler : ICustomMarshaler<SByte>
    {
        private ByteMarshaler mByteMarshaler = new ByteMarshaler();
        public void CSharpToBytes(SByte input, MarshalBuffer marshalBuffer)
        {
            mByteMarshaler.CSharpToBytes((Byte)input, marshalBuffer);
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref SByte result)
        {
            Byte b = (Byte)result;
            mByteMarshaler.BytesToCSharp(marshalBuffer, ref b);
            result = (SByte)b;
        }

        public Int32 BufferSize
        {
            get { return mByteMarshaler.BufferSize; }
        }
    }


    internal class Int16Marshaler : ICustomMarshaler<Int16>
    {
        public void CSharpToBytes(Int16 input, MarshalBuffer marshalBuffer)
        {
            BitConverter.GetBytes(input).CopyTo(marshalBuffer.Bytes, marshalBuffer.Offset);
            marshalBuffer.Offset += BufferSize;
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref Int16 result)
        {
            result = BitConverter.ToInt16(marshalBuffer.Bytes, marshalBuffer.Offset);;
            marshalBuffer.Offset += BufferSize;
        }

        public Int32 BufferSize
        {
            get{ return sizeof(Int16); }
        }
    }

    internal class UInt16Marshaler : ICustomMarshaler<UInt16>
    {
        public void CSharpToBytes(UInt16 input, MarshalBuffer marshalBuffer)
        {
            BitConverter.GetBytes(input).CopyTo(marshalBuffer.Bytes, marshalBuffer.Offset);
            marshalBuffer.Offset += BufferSize;
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref UInt16 result)
        {
            result = BitConverter.ToUInt16(marshalBuffer.Bytes, marshalBuffer.Offset);;
            marshalBuffer.Offset += BufferSize;
        }

        public Int32 BufferSize
        {
            get{ return sizeof(UInt16); }
        }
    }

    internal class Int32Marshaler : ICustomMarshaler<Int32>
    {
        public void CSharpToBytes(Int32 input, MarshalBuffer marshalBuffer)
        {
            BitConverter.GetBytes(input).CopyTo(marshalBuffer.Bytes, marshalBuffer.Offset);
            marshalBuffer.Offset += BufferSize;
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref Int32 result)
        {
            result = BitConverter.ToInt32(marshalBuffer.Bytes, marshalBuffer.Offset);;
            marshalBuffer.Offset += BufferSize;
        }

        public Int32 BufferSize
        {
            get{ return sizeof(Int32);}
        }
    }

    internal class UInt32Marshaler : ICustomMarshaler<UInt32>
    {
        public void CSharpToBytes(UInt32 input, MarshalBuffer marshalBuffer)
        {
            BitConverter.GetBytes(input).CopyTo(marshalBuffer.Bytes, marshalBuffer.Offset);
            marshalBuffer.Offset += BufferSize;
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref UInt32 result)
        {
            result = BitConverter.ToUInt32(marshalBuffer.Bytes, marshalBuffer.Offset);;
            marshalBuffer.Offset += BufferSize;
        }

        public Int32 BufferSize
        {
            get{ return sizeof(UInt32);}
        }
    }

    internal class Int64Marshaler : ICustomMarshaler<Int64>
    {
        public void CSharpToBytes(Int64 input, MarshalBuffer marshalBuffer)
        {
            BitConverter.GetBytes(input).CopyTo(marshalBuffer.Bytes, marshalBuffer.Offset);
            marshalBuffer.Offset += BufferSize;
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref Int64 result)
        {
            result = BitConverter.ToInt64(marshalBuffer.Bytes, marshalBuffer.Offset);;
            marshalBuffer.Offset += BufferSize;
        }

        public Int32 BufferSize
        {
            get{ return sizeof(Int64); }
        }
    }

    internal class UInt64Marshaler : ICustomMarshaler<UInt64>
    {
        public void CSharpToBytes(UInt64 input, MarshalBuffer marshalBuffer)
        {
            BitConverter.GetBytes(input).CopyTo(marshalBuffer.Bytes, marshalBuffer.Offset);
            marshalBuffer.Offset += BufferSize;
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref UInt64 result)
        {
            result = BitConverter.ToUInt64(marshalBuffer.Bytes, marshalBuffer.Offset);;
            marshalBuffer.Offset += BufferSize;
        }

        public Int32 BufferSize
        {
            get{ return sizeof(UInt64); }
        }
    }

    internal class IntPtrMarshaler : ICustomMarshaler<IntPtr>
    {
        private Int64Marshaler mInt64Marshaler = new Int64Marshaler();
        public void CSharpToBytes(IntPtr input, MarshalBuffer marshalBuffer)
        {
            mInt64Marshaler.CSharpToBytes((Int64)input, marshalBuffer);            
        }
        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref IntPtr result)
        {
            Int64 anInt64 = (Int64)result;
            mInt64Marshaler.BytesToCSharp(marshalBuffer, ref anInt64);
            result = (IntPtr)anInt64;
        }

        public Int32 BufferSize
        {
            get { return mInt64Marshaler.BufferSize; }
        }
    }


    internal class SingleMarshaler : ICustomMarshaler<Single>
    {
        public void CSharpToBytes(Single input, MarshalBuffer marshalBuffer)
        {
            BitConverter.GetBytes(input).CopyTo(marshalBuffer.Bytes, marshalBuffer.Offset);
            marshalBuffer.Offset += BufferSize;
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref Single result)
        {
            result = BitConverter.ToSingle(marshalBuffer.Bytes, marshalBuffer.Offset);;
            marshalBuffer.Offset += BufferSize;
        }

        public Int32 BufferSize
        {
            get{  return sizeof(Single); }
        }
    }

    internal class DoubleMarshaler : ICustomMarshaler<Double>
    {
        public void CSharpToBytes(Double input, MarshalBuffer marshalBuffer)
        {
            BitConverter.GetBytes(input).CopyTo(marshalBuffer.Bytes, marshalBuffer.Offset);
            marshalBuffer.Offset += BufferSize;
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref Double result)
        {
            result = BitConverter.ToDouble(marshalBuffer.Bytes, marshalBuffer.Offset);
            marshalBuffer.Offset += BufferSize;
        }

        public Int32 BufferSize
        {
            get{ return sizeof(Double);}
        }
    }

    internal class EnumMarshaler<T> : ICustomMarshaler<T> where T : struct, IConvertible
    {
        private Int32Marshaler mInt32Marshaler = new Int32Marshaler();
        public void CSharpToBytes(T input, MarshalBuffer marshalBuffer)
        {
            mInt32Marshaler.CSharpToBytes(Convert.ToInt32(input), marshalBuffer);
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref T result)
        {
            Int32 b = Convert.ToInt32(result);
            mInt32Marshaler.BytesToCSharp(marshalBuffer, ref b);
            result = (T)(object)b;
        }

        public Int32 BufferSize
        {
            get { return mInt32Marshaler.BufferSize; }
        }
    }
    /// <summary>
    /// .NET marshaler for System.String
    ///     - The String is converted to a Byte[] and passed by reference
    ///     - This marshaler is used to compose other structure containing a String member, 
    ///       The types who used this marshaler need to set its marshal approach as "ByteSerializationByRef"
    /// </summary>
    internal class StringMarshaler : ICustomMarshaler<String>
    {
        public void CSharpToBytes(String input, MarshalBuffer marshalBuffer)
        {
            var stringBytes = default(Byte[]);
            if(input != null)
            {
                stringBytes = Encoding.UTF8.GetBytes(input);
            }
            mByteArrayMarshaler.CSharpToBytes(stringBytes, marshalBuffer);
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref String result)
        {
            var stringBytes = default(Byte[]);
            mByteArrayMarshaler.BytesToCSharp(marshalBuffer, ref stringBytes);
            if(stringBytes != null)
            {
                result = Encoding.UTF8.GetString(stringBytes);
            }
        }

        public Int32 BufferSize
        {
            get { return mByteArrayMarshaler.BufferSize; }
        }
        
        private BlittableArrayMarshaler<Byte> mByteArrayMarshaler = new BlittableArrayMarshaler<byte>();        
    }


    /// <summary>
    /// Marshaling class to only be used in composing other classes. It puts in only the actual string, and thus is variable length.
    /// </summary>
    internal class VariableLengthStringMarshaler : ICustomMarshaler<String>
    {
        public VariableLengthStringMarshaler()
        {
        }

        public Int32 BufferSize
        {
            get { throw new Exception("No fixed size"); }
        }

        public void CSharpToBytes(String input, MarshalBuffer marshalBuffer)
        {
            var encoder = new UTF8Encoding();

            var stringAsBytes = encoder.GetBytes(input);

            // The +1 allows for the null byte at the end
            if (stringAsBytes.Length + 1 > marshalBuffer.Bytes.Length - marshalBuffer.Offset)
                throw new Exception("Exceeded maximum buffer size");

            stringAsBytes.CopyTo(marshalBuffer.Bytes, marshalBuffer.Offset);
            marshalBuffer.Offset += stringAsBytes.Length;
            marshalBuffer.Bytes[marshalBuffer.Offset++] = 0;
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref String result)
        {
            var length = 0;
            // look for terminating semaphore, but don't go off the end
            while (marshalBuffer.Bytes[marshalBuffer.Offset + length] != 0)
            {

                length++;
                if (marshalBuffer.Offset + length >= marshalBuffer.Bytes.Length)
                    throw new Exception("Malformed data - no null found");
            }

            result = Encoding.UTF8.GetString(marshalBuffer.Bytes, marshalBuffer.Offset, length);

            // Note the extra step in Length is for the semaphore
            marshalBuffer.Offset += length + 1;
        }
    }

}