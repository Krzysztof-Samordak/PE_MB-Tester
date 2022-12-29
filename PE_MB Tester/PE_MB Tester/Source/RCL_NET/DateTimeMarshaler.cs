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
    internal class DateTimeMarshaler : ICustomMarshaler<DateTime>
    {
        private DoubleMarshaler mDoubleMarshaler = new DoubleMarshaler();

        public void CSharpToBytes(DateTime input, MarshalBuffer marshalBuffer)
        {
            mDoubleMarshaler.CSharpToBytes(Convert.ToDouble(input.Ticks) / 10000000, marshalBuffer);
            mDoubleMarshaler.CSharpToBytes(0.0, marshalBuffer);
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref DateTime result)
        {
            double secondsTotal = 0;
            double secondsFractional = 0;
            mDoubleMarshaler.BytesToCSharp(marshalBuffer, ref secondsTotal);
            mDoubleMarshaler.BytesToCSharp(marshalBuffer, ref secondsFractional);
            result = new DateTime(Convert.ToInt64((secondsTotal + secondsFractional) * 10000000));
        }

        public int BufferSize
        {
            get { return mDoubleMarshaler.BufferSize * 2; }
        }
    }
}
