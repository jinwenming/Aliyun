/* ==============================================================================
  * 类名称：AliyunConfigurationSource
  * 类描述：
  * 创建人：jinwenming
  * 创建时间：8/5/2020 9:31:28 AM
  * 修改人：
  * 修改时间：
  * 修改备注：
  * @version 1.0
  * ==============================================================================*/

using Microsoft.Extensions.Configuration;
using System;

namespace King.Configuration.Aliyun
{
    /// <summary>
    /// 
    /// </summary>
    internal class AliyunConfigurationSource : IAliyunConfigurationSource
    {
        public Action<AliyunClientConfiguration> AliyunConfigurationOptions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            var clientFactory = new AliyunClientFactory(this);
            return new AliyunConfigurationProvider(this, clientFactory);
        }
    }
}
