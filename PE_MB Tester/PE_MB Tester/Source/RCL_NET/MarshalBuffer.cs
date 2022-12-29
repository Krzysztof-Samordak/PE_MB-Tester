/******************************************************
 *       Copyright Keysight Technologies 2018-2021
 ******************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;

namespace Keysight.ApiCoreLibraries
{
    /// <summary>
    /// Class to hold the memory buffer
    /// </summary>
    internal class MarshalBuffer : IDisposable
    {
        private Int32 mOffset = 0;
        private Int32 mMaxSize = 0;

        /// <summary>
        /// Initializes a new instance of MarshalBuffer. This constructor allocates a memory with the input bufferSize
        /// </summary>
        /// <param name="bufferSize">The buffer length be allocated</param>
        public MarshalBuffer(Int32 bufferSize)
        {
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException("bufferSize for MarshalBuffer must be a positive number");
            
            //The first 4 bytes for a int32 tracing the buffer length for bounds check
            mMaxSize = bufferSize + sizeof(Int32);
            Bytes = new Byte[mMaxSize];
            BitConverter.GetBytes(mMaxSize).CopyTo(Bytes, 0);
            // Set the offset to the real data
            Offset = sizeof(Int32);

            GCHandle pinnedBytesHandle = GCHandle.Alloc(Bytes, GCHandleType.Pinned);
            GCHandles = new List<GCHandle> { pinnedBytesHandle };


            PinnedBytes = new IntPtr(pinnedBytesHandle.AddrOfPinnedObject().ToInt64());

            HelperObjects = new System.Collections.Queue();
        }

        /// <summary>
        /// Construct from a byte array where the marshaling is performed
        /// </summary>
        /// <param name="buffer"></param>
        public MarshalBuffer(Byte[] buffer)
        {
            if(buffer == null || buffer.Length == 0)
                throw new ArgumentOutOfRangeException("buffer for MarshalBuffer must not null or empty.");
            mMaxSize = buffer.Length;
            Bytes = buffer;
            Offset = 0;

            GCHandles = new List<GCHandle>();
            HelperObjects = new System.Collections.Queue();
        }

        /// <summary>
        /// Reset the offset of the buffer
        /// </summary>
        public void ResetOffset()
        {
            Offset = sizeof(Int32);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);           
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var h in GCHandles) h.Free();     
            }           
        }
        /// <summary>
        /// The in-memory array that contains the marshaling data
        /// </summary>
        public Byte[] Bytes { get; private set; }

        /// <summary>
        /// A 32-bit integer that represents the index in array at which marshaling begins
        /// 
        /// </summary>
        public Int32 Offset
        {
            set
            {
                if (value > mMaxSize)
                    throw new IndexOutOfRangeException("Offset of MarshalBuffer exceed the maximum size");
                mOffset = value;
            }

            get
            {
                // Check the offset here to avoid the client use the offset to access out-of-boundary memory
                if (mOffset >= mMaxSize)
                    throw new IndexOutOfRangeException("Offset of MarshalBuffer exceed the maximum size");
                return mOffset;
            }
        }
        public IList<GCHandle> GCHandles { get; private set; }
        public System.IntPtr PinnedBytes { get; private set; }

        /// <summary>
        /// A list of objects used to cache some objects, It depends on 
        /// </summary>
        public System.Collections.Queue HelperObjects { get; private set; }
    }
}
