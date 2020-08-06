/* ==============================================================================
  * 接口名称：IClientFactory
  * 类描述：
  * 创建人：jinwenming
  * 创建时间：8/5/2020 10:28:45 AM
  * 修改人：
  * 修改时间：
  * 修改备注：
  * @version 1.0
  * ==============================================================================*/

using System.Net.Http;

namespace King.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IClientFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        HttpClient CreateHttpClient();
    }
}
