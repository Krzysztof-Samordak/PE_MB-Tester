/******************************************************
 *       Copyright Keysight Technologies 2018-2021
 ******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keysight.ApiCoreLibraries
{
    /// <summary>
    /// Base class for all the tree node
    /// </summary>
    public class KapiTreeNode : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <param name="cppApiObject"></param>
        internal KapiTreeNode(String name, KapiTreeNode parent, Object cppApiObject)
        {
            TreeNodeName = name;
            Parent = parent;
            _cppApiObject = cppApiObject;
        }
        /// <summary>
        /// 
        /// </summary>
        public String TreeNodeName { get; private set; }
        internal KapiTreeNode Parent { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        protected Object _cppApiObject = null;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing && _cppApiObject != null)
            {
                (_cppApiObject as IDisposable).Dispose();
            }
        }
    }
}
