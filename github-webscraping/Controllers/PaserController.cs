using System.Threading.Tasks;
using github_webscraping.Business;
using github_webscraping.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace github_webscraping.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class PaserController : ControllerBase
    {
        private readonly IGitHubRepoBusiness _gitHubRepoBusiness;
        public PaserController(IGitHubRepoBusiness gitHubRepoBusiness)
        {
            _gitHubRepoBusiness = gitHubRepoBusiness;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <example>
        ///     "https://github.com/rafael-hs/ngrx-demo"
        /// </example>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        [HttpPost("get-data-repository")]
        public async Task<IActionResult> PaserUrl([FromBody] string baseUrl)
        {
            if (string.IsNullOrEmpty(baseUrl))
                return BadRequest(ReturnApi.Empty);

            var response = await Task.FromResult(_gitHubRepoBusiness.RepositoryMapping(baseUrl));
            return Ok(JsonConvert.SerializeObject(response));
        }
    }
}
