using System.Collections.Generic;
using Library;
using Library.Repository;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private IRepository<Dog> DogRepository { get; }

        public DogsController(IRepository<Dog> dogRepository)
        {
            DogRepository = dogRepository;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IList<Dog>> Get()
        {
            var dogs = DogRepository.Get();
            return Ok(dogs);
            // return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /* 
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
        } */
    }
}
