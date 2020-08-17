using System;
using System.Diagnostics;
using System.Threading.Tasks;
using github_webscraping.Business;
using github_webscraping.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        /// Return all files on github repository
        /// </summary>
        /// <remarks>
        /// Return all files on github repository, grouped by extension with total number lines and size.
        /// </remarks>
        /// <param name="baseUrl">The Url for searching</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
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
