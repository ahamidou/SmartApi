using System;
using System.Web.Http;
using SmartApi.Infrastructure;
using SmartApi.Models;

namespace SmartApi.Controllers
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        [ActionName("GetValues")]
        public IHttpActionResult Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        [HttpGet]
        [Route("{id}")]
        [ActionName("GetValue")]
        public IHttpActionResult Get(int id)
        {
            return Ok("Value");
        }

        [HttpPost]
        [ActionName("PostCustomer")]
        public IHttpActionResult Post([FromBody] NewCustomerViewModel newCustomerModel)
        {
            newCustomerModel.Name += " {password}";
            return Created("", newCustomerModel);
        }

        [HttpPut]
        [Route("{id}")]
        [ActionName("PutValue")]
        public IHttpActionResult Put(int id, [FromBody]string value)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        [ActionName("DeleteValue")]
        public IHttpActionResult Delete(int id)
        {
            if (id == 0) throw new Exception("zero based stuff");
            if (id == 1) throw new ApiCustomException("number 1 in taken!!");
            return Ok();
        }
    }
}
