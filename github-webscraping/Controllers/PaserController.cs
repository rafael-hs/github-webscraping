using System.Threading.Tasks;
using github_webscraping.Business;
using github_webscraping.Shared;
using Microsoft.AspNetCore.Mvc;


namespace github_webscraping.Controllers
{
    //[ApiVersion("1")]
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
            return Ok(response);
        }

        //// GET api/<PaserController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<PaserController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<PaserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<PaserController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
