/* ==============================================================================
  * 接口名称：IAliyunConfigurationSource
  * 类描述：
  * 创建人：jinwenming
  * 创建时间：8/5/2020 9:37:06 AM
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
    public interface IAliyunConfigurationSource : IConfigurationSource
    {
        /// <summary>
        /// 
        /// </summary>
        Action<AliyunClientConfiguration> AliyunConfigurationOptions { get; set; }
    }
}
