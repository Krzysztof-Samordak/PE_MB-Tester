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
    internal class TimeSpanMarshaler : ICustomMarshaler<TimeSpan>
    {
        private DoubleMarshaler mDoubleMarshaler = new DoubleMarshaler();

        public void CSharpToBytes(TimeSpan input, MarshalBuffer marshalBuffer)
        {
            mDoubleMarshaler.CSharpToBytes(input.TotalSeconds, marshalBuffer);
            mDoubleMarshaler.CSharpToBytes(0.0, marshalBuffer);
        }

        public void BytesToCSharp(MarshalBuffer marshalBuffer, ref TimeSpan result)
        {
            double secondsTotal = 0, secondsFractional = 0;
            mDoubleMarshaler.BytesToCSharp(marshalBuffer, ref secondsTotal);
            mDoubleMarshaler.BytesToCSharp(marshalBuffer, ref secondsFractional);
            long ticks = Convert.ToInt64((secondsTotal + secondsFractional) * 10000000);
            result = new TimeSpan(ticks);
        }

        public int BufferSize
        {
            get { return mDoubleMarshaler.BufferSize * 2; }
        }
    }
}
