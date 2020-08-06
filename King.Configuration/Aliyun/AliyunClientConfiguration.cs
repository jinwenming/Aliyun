/* ==============================================================================
  * 类名称：AliyunClientConfiguration
  * 类描述：
  * 创建人：jinwenming
  * 创建时间：8/5/2020 9:36:41 AM
  * 修改人：
  * 修改时间：
  * 修改备注：
  * @version 1.0
  * ==============================================================================*/
using System.Collections.Generic;

namespace King.Configuration.Aliyun
{
    public class AliyunClientConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<AliyunConfigurationItem> ConfigItems { get; set; } = new List<AliyunConfigurationItem>();
    }

    public class AliyunConfigurationItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string DataId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ContentMD5 { get; set; }
    }
}
