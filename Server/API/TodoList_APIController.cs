using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAppTest.Server.Models.DbEntity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorAppTest.Server.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoList_APIController : ControllerBase
    {
        private TodoList_Blazor_NET5Context _dbcontext;
        public TodoList_APIController(TodoList_Blazor_NET5Context dbcontext) => _dbcontext = dbcontext;

        // GET: api/<TodoList_APIController>
        [HttpGet]
        public IEnumerable<TodoList> Get()
        {
            var todolist = _dbcontext.Set<TodoList>().ToList();

            return todolist;
        }

        // GET api/<TodoList_APIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TodoList_APIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TodoList_APIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TodoList_APIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
