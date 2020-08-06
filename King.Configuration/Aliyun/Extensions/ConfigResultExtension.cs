/* ==============================================================================
  * 类名称：ConfigResultExtension
  * 类描述：
  * 创建人：jinwenming
  * 创建时间：8/5/2020 10:45:43 AM
  * 修改人：
  * 修改时间：
  * 修改备注：
  * @version 1.0
  * ==============================================================================*/
using King.Configuration.Aliyun.Parsers;
using System;
using System.Collections.Generic;

namespace King.Configuration.Aliyun.Extensions
{
    public static class ConfigResultExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        internal static IDictionary<string, string> ToConfigDictionary(this string result)
        {
            var dict = new Dictionary<string, string>();
            if (result is null) return dict;
            try
            {
                var dictionary = JsonConfigurationParser.Parse(result);
                return dictionary;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return dict;
            }
        }
    }
}
