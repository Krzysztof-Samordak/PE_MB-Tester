/******************************************************
 *       Copyright Keysight Technologies 2018-2021
 ******************************************************/
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Keysight.ApiCoreLibraries
{
    /// <summary>
    /// AllocationHelper is used to allow allocating managed memory from unmanaged code
    /// AllocationHelper provides a delegate that is used to be invoked from C++
    /// </summary>
    internal class AllocationHelper
    {
        private static readonly Object mLocker = new Object();
        private static Dictionary<IntPtr, GCHandle> mPinned = new Dictionary<System.IntPtr, GCHandle>();
        public delegate IntPtr AllocationDelegate(IntPtr originalHandle, Int32 size);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static AllocationDelegate GetAllocateMemoryDelegate<T>()
        {
            if (allocationDelegateDicts.ContainsKey(typeof(T)))
            {
                return allocationDelegateDicts[typeof(T)];
            }
            else
            {
                throw new ArgumentException(string.Format("{0} is not supported in GetAllocateMemoryDelegate", typeof(T).FullName));
            }
        }

        static readonly Dictionary<Type, AllocationDelegate> allocationDelegateDicts = new Dictionary<Type, AllocationDelegate>
        {
            { typeof(System.Byte),  new AllocationDelegate(AllocateMemoryGeneric<System.Byte>) },
            { typeof(System.SByte), new AllocationDelegate(AllocateMemoryGeneric<System.SByte>) },
            { typeof(System.Int16), new AllocationDelegate(AllocateMemoryGeneric<System.Int16>) },
            { typeof(System.UInt16), new AllocationDelegate(AllocateMemoryGeneric<System.UInt16>) },
            { typeof(System.Int32), new AllocationDelegate(AllocateMemoryGeneric<System.Int32>) },
            { typeof(System.UInt32), new AllocationDelegate(AllocateMemoryGeneric<System.UInt32>) },
            { typeof(System.Int64), new AllocationDelegate(AllocateMemoryGeneric<System.Int64>) },
            { typeof(System.UInt64), new AllocationDelegate(AllocateMemoryGeneric<System.UInt64>) },
            { typeof(System.IntPtr), new AllocationDelegate(AllocateMemoryGeneric<System.IntPtr>) },
            { typeof(System.UIntPtr), new AllocationDelegate(AllocateMemoryGeneric<System.UIntPtr>) },
            { typeof(System.Single), new AllocationDelegate(AllocateMemoryGeneric<System.Single>) },
            { typeof(System.Double), new AllocationDelegate(AllocateMemoryGeneric<System.Double>) },
        };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Object RetrieveMemory(IntPtr id)
        {
            lock (mLocker)
            {
                Object dotMemory = null;
                if (mPinned.ContainsKey(id))
                {
                    var gcHandle = mPinned[id];
                    dotMemory = gcHandle.Target;
                    gcHandle.Free();
                    mPinned.Remove(id);
                }
                return dotMemory;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalHandle"></param>
        /// <param name="size"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private static IntPtr AllocateMemoryGeneric<T>(IntPtr originalHandle, Int32 size)
        {
            lock (mLocker)
            {
                T[] temp = default(T[]);

                try
                {
                    temp = new T[size];
                }
                catch 
                //Typically it's for System.OutOfMemoryExceptions  
                {
                    return IntPtr.Zero;
                }
                
                var pinned = GCHandle.Alloc(temp, GCHandleType.Pinned);
                var id = pinned.AddrOfPinnedObject();
                mPinned[id] = pinned;
                return id;
            }
        }
    }
}
