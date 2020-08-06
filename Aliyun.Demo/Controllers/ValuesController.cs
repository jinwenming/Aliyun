/* ==============================================================================
  * 类名称：ValuesController
  * 类描述：
  * 创建人：jinwenming
  * 创建时间：8/5/2020 9:20:37 AM
  * 修改人：
  * 修改时间：
  * 修改备注：
  * @version 1.0
  * ==============================================================================*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Aliyun.Demo.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AppSettings _settings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsSnapshot"></param>
        public ValuesController(
            IOptionsSnapshot<AppSettings> optionsSnapshot)
        {
            _settings = optionsSnapshot.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_settings);
        }
    }
}
