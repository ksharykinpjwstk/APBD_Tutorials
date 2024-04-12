using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tutorial5.Showcase.Controllers.Controllers
{
    [ApiController]
    public class WeatherController : ControllerBase
    {
        /*DEPENDENCY INJECTION
         private readonly IServiceModel ServiceModel;
         public WeatherController(IServiceModel serviceModel) 
         {
            ServiceModel = serviceModel;
         }
         */
        // GET: api/<WeatherController>
        [Route("api/weathers")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/weathers/{id}")]
        // GET api/<WeatherController>/5
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        [Route("api/weathers")]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WeatherController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WeatherController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
