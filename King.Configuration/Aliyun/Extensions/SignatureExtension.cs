/* ==============================================================================
  * 类名称：SignatureExtension
  * 类描述：
  * 创建人：jinwenming
  * 创建时间：8/5/2020 10:41:07 AM
  * 修改人：
  * 修改时间：
  * 修改备注：
  * @version 1.0
  * ==============================================================================*/
using System;
using System.Security.Cryptography;
using System.Text;

namespace King.Configuration.Aliyun.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class SignatureExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptText"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string HmacSHA1Encrypt(string encryptText, string encryptKey)
        {
            var hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(encryptKey));
            var dataBuffer = Encoding.UTF8.GetBytes(encryptText);
            var hashBytes = hmacsha1.ComputeHash(dataBuffer);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
