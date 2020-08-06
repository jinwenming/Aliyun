/* ==============================================================================
  * 类名称：DictionaryExtension
  * 类描述：
  * 创建人：jinwenming
  * 创建时间：8/5/2020 10:45:03 AM
  * 修改人：
  * 修改时间：
  * 修改备注：
  * @version 1.0
  * ==============================================================================*/
using System.Collections.Generic;
using System.Linq;

namespace King.Configuration.Aliyun.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static T Merge<T, K, V>(this T first, params IDictionary<K, V>[] second)
            where T : IDictionary<K, V>, new()
        {
            T dict = new T();
            var items = new List<IDictionary<K, V>> { first }.Concat(second);
            foreach (IDictionary<K, V> item in items)
            {
                foreach (KeyValuePair<K, V> p in item)
                {
                    dict[p.Key] = p.Value;
                }
            }
            return dict;
        }
    }
}
