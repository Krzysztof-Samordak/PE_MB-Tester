/******************************************************
 *       Copyright Keysight Technologies 2018-2021
 ******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Reflection;


namespace Keysight.ApiCoreLibraries
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class KapiCollectionRepeatedCapability<T> : KapiTreeNode, IEnumerable<T>, IEnumerable where T : KapiTreeNode
    {
        /// <summary>
        /// Dictionary of interfaces of repeated capabilities and keys (key => C)
        /// </summary>
        private Dictionary<String, T> _dict;       


        /// <summary>
        /// constructor of instance class used to create the instance
        /// </summary>
        private System.Reflection.ConstructorInfo _instanceCtor = null;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <param name="cppApiObject"></param>
        internal KapiCollectionRepeatedCapability(String name, KapiTreeNode parent, Object cppApiObject):
            base(name, parent, cppApiObject)
        {
            _dict = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);

            var instanceType = typeof(T);
            
            this._instanceCtor = instanceType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, 
                null, new Type[] { typeof(String), typeof(KapiTreeNode), typeof(Object) }, null);            

            this.BuildCollection();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Key of repeated capability</param>
        /// <returns>Interface of repeated capability</returns>
        public T this[String key]
        {
            get
            {                
                if(!Keys.Contains(key, StringComparer.CurrentCultureIgnoreCase))
                {
                    if (_dict.ContainsKey(key))
                        _dict.Remove(key);
                    //throw new IndexOutOfRangeException(String.Format("Not supported repcap instance name: {0}", key));
                    throw new KeyNotFoundException(String.Format("Not supported repcap instance name: {0}", key));
                }

                if(_dict.ContainsKey(key))
                {
                    return (T)_dict[key];
                }
                else
                {
                    T val = (T)CreateInstance(key, this, this.CppApiObjectOfInstance(key));
                    _dict.Add(key, val);
                    return val;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected List<T> BuildCollection()
        {
            List<T> list = new List<T>();

            foreach (var key in Keys)
            {
                T it = this[key];
                list.Add(it);
            }

            return list;
        }

        #region IEnumerable<KeyValuePair<string,T>> Members
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.BuildCollection().GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.BuildCollection().GetEnumerator();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private Object CppApiObjectOfInstance(String key)
        {   
            var indexer = _cppApiObject.GetType().GetProperties().Where(p => p.GetIndexParameters().Length != 0).FirstOrDefault();
            if (indexer != null)
            {   
                return indexer.GetValue(_cppApiObject, new Object[] { key});
            }
            else
            {
                return null;
            }   
        }

        private List<String> Keys
        {
            get
            {
                var keysProperty = this._cppApiObject.GetType().GetProperty("Keys");
                var keys = keysProperty.GetValue(this._cppApiObject, new Object[] { });
                return (keys as ICollection<string>).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <param name="cppApiObject"></param>
        /// <returns></returns>
        private Object CreateInstance(String name, KapiTreeNode parent, Object cppApiObject)
        {
            return this._instanceCtor.Invoke(new Object[] { name, parent, cppApiObject });
        }

    }
}
