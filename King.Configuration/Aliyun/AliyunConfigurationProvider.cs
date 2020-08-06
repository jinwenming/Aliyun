/* ==============================================================================
  * 类名称：AliyunConfigurationProvider
  * 类描述：
  * 创建人：jinwenming
  * 创建时间：8/5/2020 9:30:09 AM
  * 修改人：
  * 修改时间：
  * 修改备注：
  * @version 1.0
  * ==============================================================================*/

using King.Configuration.Aliyun.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace King.Configuration.Aliyun
{
    /// <summary>
    /// 
    /// </summary>
    internal class AliyunConfigurationProvider : ConfigurationProvider
    {
        private readonly IAliyunConfigurationSource _aliyunConfigurationSource;
        private readonly IClientFactory _clientFactory;
        private readonly AliyunClientConfiguration option = new AliyunClientConfiguration();
        private Dictionary<string, string> data = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aliyunConfigurationSource"></param>
        public AliyunConfigurationProvider(
            IAliyunConfigurationSource aliyunConfigurationSource,
            IClientFactory clientFactory)
        {
            _aliyunConfigurationSource = aliyunConfigurationSource ?? throw new ArgumentException(nameof(AliyunConfigurationProvider));
            _aliyunConfigurationSource.AliyunConfigurationOptions(option);
            _clientFactory = clientFactory;
            _clientFactory.InitConfig(option, SetData);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Load()
        {
            _clientFactory.ListenConfig(option, SetData);
            base.Load();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public string SetData(string dataId, string group)
        {
            var response = _clientFactory.GetConfig(option.AccessKey, option.SecretKey, option.Namespace, dataId, group).Result;
            data = data.Merge(response.ToConfigDictionary());
            Data = data;
            return response.Md5();
        }
    }
}
