/* ==============================================================================
  * 类名称：AliyunConfigurationExtension
  * 类描述：
  * 创建人：jinwenming
  * 创建时间：8/5/2020 9:23:52 AM
  * 修改人：
  * 修改时间：
  * 修改备注：
  * @version 1.0
  * ==============================================================================*/

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace King.Configuration.Aliyun
{
    /// <summary>
    /// 
    /// </summary>
    public static class AliyunConfigurationExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationBuilder"></param>
        /// <returns></returns>
        private static IConfigurationBuilder AddAliyun(this IConfigurationBuilder configurationBuilder)
            => configurationBuilder.Add(new AliyunConfigurationSource());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationBuilder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddAliyun(
            this IConfigurationBuilder configurationBuilder,
            Action<IAliyunConfigurationSource> options)
        {
            var configSource = new AliyunConfigurationSource();
            options(configSource);
            return configurationBuilder.Add(configSource);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationBuilder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddAliyun(
            this IConfigurationBuilder configurationBuilder,
            IConfiguration configuration)
        {
            var aliyunConfigurationItems = configuration.GetSection("ConfigItems").Get<List<AliyunConfigurationItem>>();
            if (null == aliyunConfigurationItems) throw new ArgumentNullException(nameof(AliyunConfigurationItem));
            var configurationOption = configuration.Get<AliyunClientConfiguration>();
            if (null == configurationOption) throw new ArgumentNullException(nameof(AliyunClientConfiguration));

            var configSource = new AliyunConfigurationSource
            {
                AliyunConfigurationOptions = config =>
                {
                    config.Endpoint = configurationOption.Endpoint;
                    config.AccessKey = configurationOption.AccessKey;
                    config.SecretKey = configurationOption.SecretKey;
                    config.Namespace = configurationOption.Namespace;
                    config.ConfigItems = aliyunConfigurationItems;
                }
            };
            return configurationBuilder.Add(configSource);
        }
    }
}
