/* ==============================================================================
  * 类名称：JsonConfigurationParser
  * 类描述：
  * 创建人：jinwenming
  * 创建时间：8/5/2020 10:46:23 AM
  * 修改人：
  * 修改时间：
  * 修改备注：
  * @version 1.0
  * ==============================================================================*/
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace King.Configuration.Aliyun.Parsers
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonConfigurationParser
    {
        private JsonConfigurationParser() { }

        private readonly IDictionary<string, string> _data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private readonly Stack<string> _context = new Stack<string>();
        private string _currentPath;

        public static IDictionary<string, string> Parse(string json) => new JsonConfigurationParser().ParseJson(json);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private IDictionary<string, string> ParseJson(string json)
        {
            _data.Clear();

            var jsonConfig = JObject.Parse(json);
            VisitJObject(jsonConfig);

            return _data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jObject"></param>
        private void VisitJObject(JObject jObject)
        {
            foreach (var property in jObject.Properties())
            {
                EnterContext(property.Name);
                VisitProperty(property);
                ExitContext();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        private void VisitProperty(JProperty property)
        {
            VisitToken(property.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        private void VisitToken(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    VisitJObject(token.Value<JObject>());
                    break;

                case JTokenType.Array:
                    VisitArray(token.Value<JArray>());
                    break;

                case JTokenType.Integer:
                case JTokenType.Float:
                case JTokenType.String:
                case JTokenType.Boolean:
                case JTokenType.Bytes:
                case JTokenType.Raw:
                case JTokenType.Null:
                    VisitPrimitive(token.Value<JValue>());
                    break;

                default:
                    throw new FormatException("Unsupported JSON token");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        private void VisitArray(JArray array)
        {
            for (int index = 0; index < array.Count; index++)
            {
                EnterContext(index.ToString());
                VisitToken(array[index]);
                ExitContext();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void VisitPrimitive(JValue data)
        {
            var key = _currentPath;

            if (_data.ContainsKey(key))
            {
                throw new FormatException("Duplicate Key");
            }
            _data[key] = data.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private void EnterContext(string context)
        {
            _context.Push(context);
            _currentPath = ConfigurationPath.Combine(_context.Reverse());
        }

        /// <summary>
        /// 
        /// </summary>
        private void ExitContext()
        {
            _context.Pop();
            _currentPath = ConfigurationPath.Combine(_context.Reverse());
        }
    }
}
