/* ==============================================================================
  * 类名称：AliyunClientFactory
  * 类描述：
  * 创建人：jinwenming
  * 创建时间：8/5/2020 10:30:37 AM
  * 修改人：
  * 修改时间：
  * 修改备注：
  * @version 1.0
  * ==============================================================================*/

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace King.Configuration.Aliyun
{
    /// <summary>
    /// 
    /// </summary>
    internal class AliyunClientFactory : IClientFactory
    {
        private readonly IAliyunConfigurationSource _aliyunConfigurationSource;
        private string serverIp;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aliyunConfigurationSource"></param>
        public AliyunClientFactory(IAliyunConfigurationSource aliyunConfigurationSource)
        {
            _aliyunConfigurationSource = aliyunConfigurationSource;
            serverIp = GetServerIp().ConfigureAwait(false).GetAwaiter().GetResult();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HttpClient CreateHttpClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri($"http://{serverIp}:8080")
            };
            return client;
        }

        /// <summary>
        /// 获取服务器IP
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetServerIp()
        {
            var aliyunConfigurationOptions = _aliyunConfigurationSource.AliyunConfigurationOptions;
            AliyunClientConfiguration option = new AliyunClientConfiguration();
            aliyunConfigurationOptions(option);

            string serverIp = "139.196.135.144";
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"http://{option.Endpoint}:8080");
                string result = await httpClient.GetStringAsync("/diamond-server/diamond");
                serverIp = result.Replace("\n", "");
            };

            return serverIp;
        }
    }
}
