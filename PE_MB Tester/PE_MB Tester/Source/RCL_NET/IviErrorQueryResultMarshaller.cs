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
    internal class IviErrorQueryResultMarshaler : ICustomMarshaler<Ivi.Driver.ErrorQueryResult>
    {
        private Int32Marshaler mInt32Marshaler = new Int32Marshaler();
        private StringMarshaler mStringMarshaler = new StringMarshaler();

        //  This is never used because ErrorQueryResult is only used to pass the self test result
        //    back from the C# API.
        public void CSharpToBytes(Ivi.Driver.ErrorQueryResult input, MarshalBuffer marshalBuffer)
        {
            mInt32Marshaler.CSharpToBytes(input.Code, marshalBuffer);
            mStringMarshaler.CSharpToBytes(input.Message, marshalBuffer);
        }

        //  This is used to pass the self test result back from the C# API.
        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref Ivi.Driver.ErrorQueryResult result)
        {
            Int32 code = default(Int32);
            String message = "";
            mInt32Marshaler.BytesToCSharp(marshalBuffer, ref code);
            mStringMarshaler.BytesToCSharp(marshalBuffer, ref message);
            result = new Ivi.Driver.ErrorQueryResult(code, message);
        }

        public int BufferSize
        {
            get
            {
                return mInt32Marshaler.BufferSize + mStringMarshaler.BufferSize;
            }
        }
    }
}