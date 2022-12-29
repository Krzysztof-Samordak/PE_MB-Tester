/******************************************************
 *       Copyright Keysight Technologies 2018-2019
 ******************************************************/
using System;

namespace Keysight.KtEL30000
{
    /// <summary>
    /// Generic marshaler for array of element
    /// </summary>
    /// <typeparam name="_eleT">Element type</typeparam>
    /// <typeparam name="_marshalerT">the marshaler class of the element itself</typeparam>
    internal class NonBlittableArrayMarshaler<_eleT, _marshalerT> : Keysight.ApiCoreLibraries.ICustomMarshaler<_eleT[]> 
        where _marshalerT: Keysight.ApiCoreLibraries.ICustomMarshaler<_eleT>, new()
    {
        private _marshalerT mElementMarshaler = new _marshalerT();
        private ApiCoreLibraries.BlittableArrayMarshaler<Byte> mByteArrayMarshaler = new ApiCoreLibraries.BlittableArrayMarshaler<Byte>();

        public void CSharpToBytes(_eleT[] input, ApiCoreLibraries.MarshalBuffer marshalBuffer)
        {
            var byteArray = default(Byte[]);

            if (input != null)
            {
                var bytesLen = input.Length * mElementMarshaler.BufferSize;
                var innerMarshalBuffer = new Keysight.ApiCoreLibraries.MarshalBuffer(bytesLen);
                for (int i = 0; i < input.Length; i++)
                {
                    mElementMarshaler.CSharpToBytes(input[i], innerMarshalBuffer);
                }
                byteArray = new Byte[bytesLen];
                innerMarshalBuffer.ResetOffset();
                Array.Copy(innerMarshalBuffer.Bytes, innerMarshalBuffer.Offset, byteArray, 0, bytesLen);

                // Saved the pinned memory handler to marshaler buffer because the innerMarshalBuffer maybe collected
                foreach (var gch in innerMarshalBuffer.GCHandles)
                {
                    marshalBuffer.GCHandles.Add(gch);
                }
                marshalBuffer.HelperObjects.Enqueue(byteArray);
            }
            mByteArrayMarshaler.CSharpToBytes(byteArray, marshalBuffer);
        }

        public void BytesToCSharp(ApiCoreLibraries.MarshalBuffer marshalBuffer, ref _eleT[] result)
        {
            var arraysBytes = new Byte[0];
            // If the original bytes is not resized, use the original one
            if (marshalBuffer.HelperObjects.Count != 0)
            {
                arraysBytes = marshalBuffer.HelperObjects.Dequeue() as Byte[];
            }
            
            mByteArrayMarshaler.BytesToCSharp(marshalBuffer, ref arraysBytes);
            if(arraysBytes.Length > 0)
            {
                var innerMarshalBuffer = new ApiCoreLibraries.MarshalBuffer(arraysBytes.Length);
                innerMarshalBuffer.ResetOffset();
                Array.Copy(arraysBytes, 0, innerMarshalBuffer.Bytes, sizeof(Int32), arraysBytes.Length);
                var len = arraysBytes.Length / mElementMarshaler.BufferSize;
                result = new _eleT[len];
                for (int i = 0; i < len; i++)
                {
                    mElementMarshaler.BytesToCSharp(innerMarshalBuffer, ref result[i]);
                }
            }
        }

        public int BufferSize
        {
            get { return mByteArrayMarshaler.BufferSize; }
        }
    }
}
