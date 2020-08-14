using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace github_webscraping.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class PaserController : ControllerBase
    {
        // GET: api/<PaserController>
        [HttpGet]
        public IActionResult PaserUrl()
        {
            var html = @"https://github.com/rafael-hs/solid-concept-ts/blob/master/.gitignore";

            var web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            char[] charsToTrim = { '*', ' ', '\'', '/', '[', ']', '\n' };

            var node = htmlDoc.DocumentNode.Descendants(0).Where(e => e.HasClass("text-mono"));
            var list = new List<string>();
            foreach (var item in node)
            {
                if (item.Name.Equals("div"))
                    list.Add($"{item.FirstChild.InnerText.Trim(charsToTrim)[0]} {item.LastChild.InnerText.Trim(charsToTrim)[0]}{item.LastChild.InnerText.Trim(charsToTrim)[1]}");
                //item.InnerText.Trim();
            }

            return Ok(list);
        }

        // GET api/<PaserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PaserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PaserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PaserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
