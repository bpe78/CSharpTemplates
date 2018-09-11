using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Template.WebAPI.Interfaces;

namespace Template.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IAppSettings _settings;

        public ValuesController(IAppSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        // GET api/values
        [HttpGet]
        [SwaggerOperation("GetValues")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK)]
        [SwaggerResponse((int)System.Net.HttpStatusCode.NotFound)]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [SwaggerOperation("GetValue")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK)]
        [SwaggerResponse((int)System.Net.HttpStatusCode.NotFound)]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
