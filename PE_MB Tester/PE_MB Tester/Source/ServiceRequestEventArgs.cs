using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using MindWorks.Nimbus;
using Ivi.Driver;

namespace Keysight.KtEL30000
{
	public sealed class ServiceRequestEventArgs : EventArgs
    {
        private Keysight.KtEL30000.StatusByteFlags mStatusByte;

        public ServiceRequestEventArgs(Keysight.KtEL30000.StatusByteFlags statusByte)
        {
            mStatusByte = statusByte;
        }

        public Keysight.KtEL30000.StatusByteFlags StatusByte
        {
            get
            {
                return mStatusByte;
            }

        }
    }
}