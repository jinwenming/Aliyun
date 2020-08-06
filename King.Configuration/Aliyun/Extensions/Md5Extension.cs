/* ==============================================================================
  * 类名称：Md5Extension
  * 类描述：
  * 创建人：jinwenming
  * 创建时间：8/5/2020 10:50:10 AM
  * 修改人：
  * 修改时间：
  * 修改备注：
  * @version 1.0
  * ==============================================================================*/
using System.Security.Cryptography;
using System.Text;

namespace King.Configuration.Aliyun.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class Md5Extension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Md5(this string input)
        {
            using (var md5 = MD5.Create())
            {
                var contentMD5 = GetMd5Hash(md5, input);
                //Console.WriteLine(contentMD5);
                return contentMD5;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.GetEncoding("GBK").GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
