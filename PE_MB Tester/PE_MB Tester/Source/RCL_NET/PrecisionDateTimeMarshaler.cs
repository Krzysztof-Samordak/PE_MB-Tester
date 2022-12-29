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
    internal class PrecisionDateTimeMarshaler : ICustomMarshaler<Ivi.Driver.PrecisionDateTime>
    {
        private DoubleMarshaler mDoubleMarshaler = new DoubleMarshaler();

        public void CSharpToBytes(Ivi.Driver.PrecisionDateTime input, MarshalBuffer marshalBuffer)
        {
            mDoubleMarshaler.CSharpToBytes(input.SecondsSinceEpoch, marshalBuffer);
            mDoubleMarshaler.CSharpToBytes(input.SecondsFractional, marshalBuffer);
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref Ivi.Driver.PrecisionDateTime result)
        {
            double secondsTotal = 0, secondsFractional = 0;
            mDoubleMarshaler.BytesToCSharp(marshalBuffer, ref secondsTotal);
            mDoubleMarshaler.BytesToCSharp(marshalBuffer, ref secondsFractional);
            result = new Ivi.Driver.PrecisionDateTime(secondsTotal, secondsFractional);
        }

        public int BufferSize
        {
            get { return mDoubleMarshaler.BufferSize * 2; }
        }
    }
}
