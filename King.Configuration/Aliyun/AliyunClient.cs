/* ==============================================================================
  * 类名称：AliyunClient
  * 类描述：
  * 创建人：jinwenming
  * 创建时间：8/5/2020 10:39:44 AM
  * 修改人：
  * 修改时间：
  * 修改备注：
  * @version 1.0
  * ==============================================================================*/
using King.Configuration.Aliyun.Extensions;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace King.Configuration.Aliyun
{
    internal static class AliyunClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static async Task<string> GetConfig(this IClientFactory clientFactory, string accessKey, string secretKey, string namespaceName,
            string dataId, string group)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var timeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            string signContent = SignatureExtension.HmacSHA1Encrypt(namespaceName + "+" + group + "+" + timeStamp, secretKey);

            var httpClient = clientFactory.CreateHttpClient();
            httpClient.DefaultRequestHeaders.Add("Spas-AccessKey", accessKey);
            httpClient.DefaultRequestHeaders.Add("timeStamp", timeStamp);
            httpClient.DefaultRequestHeaders.Add("Spas-Signature", signContent);
            var url = $"/diamond-server/config.co?dataId={dataId}&group={group}&tenant={namespaceName}";
            var response = await httpClient.GetStringAsync(url);
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientFactory"></param>
        /// <param name="option"></param>
        /// <param name="func"></param>
        public static void InitConfig(this IClientFactory clientFactory, AliyunClientConfiguration option, Func<string, string, string> func)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var timeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            string signContent = SignatureExtension.HmacSHA1Encrypt($"{option.Namespace}+{timeStamp}" + timeStamp, option.SecretKey);
            var httpClient = clientFactory.CreateHttpClient();
            httpClient.DefaultRequestHeaders.Add("Spas-AccessKey", option.SecretKey);
            httpClient.DefaultRequestHeaders.Add("timeStamp", timeStamp);
            httpClient.DefaultRequestHeaders.Add("Spas-Signature", signContent);
            httpClient.DefaultRequestHeaders.Add("longPullingTimeout", "30000");
            httpClient.Timeout = TimeSpan.FromMilliseconds(30000);
            var url = "/diamond-server/config.co";
            StringBuilder sb = new StringBuilder();
            var configItems = option.ConfigItems.ToList();
            string a = ((char)2).ToString();
            string b = ((char)1).ToString();
            sb.Clear();
            foreach (var item in configItems)
            {
                sb.Append($"{item.DataId}{a}{item.Group}{a}{item.ContentMD5}{a}{option.Namespace}{b}");
            }
            string content = $"Probe-Modify-Request={sb}";
            HttpContent httpContent = new StringContent(content, Encoding.GetEncoding("GBK"), "application/json");
            var response = httpClient.PostAsync(url, httpContent).Result;
            string result = response.Content.ReadAsStringAsync().Result;

            if (!string.IsNullOrEmpty(result))
            {
                //Console.WriteLine(result);
                var configArray = Regex.Split(result, "%01", RegexOptions.IgnoreCase);
                foreach (var data in configArray)
                {
                    var array = Regex.Split(data, "%02", RegexOptions.IgnoreCase);
                    var configItem =
                        configItems.FirstOrDefault(item => item.DataId == array[0] && item.Group == array[1]);
                    if (null != configItem)
                    {
                        configItem.ContentMD5 = func(configItem.DataId, configItem.Group);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientFactory"></param>
        /// <param name="option"></param>
        /// <param name="func"></param>
        public static async void ListenConfig(this IClientFactory clientFactory, AliyunClientConfiguration option, Func<string, string, string> func)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var timeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            string signContent = SignatureExtension.HmacSHA1Encrypt($"{option.Namespace}+{timeStamp}" + timeStamp, option.SecretKey);
            var httpClient = clientFactory.CreateHttpClient();
            httpClient.DefaultRequestHeaders.Add("Spas-AccessKey", option.SecretKey);
            httpClient.DefaultRequestHeaders.Add("timeStamp", timeStamp);
            httpClient.DefaultRequestHeaders.Add("Spas-Signature", signContent);
            httpClient.DefaultRequestHeaders.Add("longPullingTimeout", "30000");
            httpClient.Timeout = TimeSpan.FromMilliseconds(30000);
            var url = "/diamond-server/config.co";
            StringBuilder sb = new StringBuilder();
            var configItems = option.ConfigItems.ToList();
            string a = ((char)2).ToString();
            string b = ((char)1).ToString();
            while (true)
            {
                sb.Clear();
                foreach (var item in configItems)
                {
                    if (string.IsNullOrEmpty(item.ContentMD5))
                    {
                        continue;
                    }
                    sb.Append($"{item.DataId}{a}{item.Group}{a}{item.ContentMD5}{a}{option.Namespace}{b}");
                }

                string content = $"Probe-Modify-Request={sb}";
                HttpContent httpContent = new StringContent(content, Encoding.GetEncoding("GBK"), "application/json");
                var response = await httpClient.PostAsync(url, httpContent);
                string result = await response.Content.ReadAsStringAsync();

                //Console.WriteLine(string.IsNullOrEmpty(result) ? "未改变" : "改变");

                if (!string.IsNullOrEmpty(result))
                {
                    //Console.WriteLine(result);
                    var configArray = Regex.Split(result, "%01", RegexOptions.IgnoreCase);
                    foreach (var data in configArray)
                    {
                        var array = Regex.Split(data, "%02", RegexOptions.IgnoreCase);
                        var configItem =
                            configItems.FirstOrDefault(item => item.DataId == array[0] && item.Group == array[1]);
                        if (null != configItem)
                        {
                            configItem.ContentMD5 = func(configItem.DataId, configItem.Group);
                        }
                    }
                }
            }
        }
    }
}
