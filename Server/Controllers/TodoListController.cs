using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAppTest.Server.Models.DbEntity;
using BlazorAppTest.Server.DataAccess;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;

namespace BlazorAppTest.Server.Controllers
{
    public class TodoListController : Controller
    {
        //private TodoList_Blazor_NET5Context _dbcontext;
        //public TodoListController(TodoList_Blazor_NET5Context dbcontext) => _dbcontext = dbcontext;
        //public IActionResult Index()
        //{
        //    var todolist = _dbcontext.Set<TodoList>().ToList();
        //    return View();
        //}

        //0302 Edit
        private readonly TodoListDataAccessLayer objtodoList;

        public TodoListController(TodoListDataAccessLayer _objtodoList)
        {
            objtodoList = _objtodoList;
        }

        [HttpGet]
        [Route("api/[controller]/Index")]
        public IEnumerable<TodoList> Index() => objtodoList.GetAllTodoLists();

        [HttpPost]
        [Route("api/[controller]/CreateList")]
        public HttpResponseMessage CreateList([FromBody] string jsonRequest)
        {
            var todoList = JsonSerializer.Deserialize<TodoList>(jsonRequest);
            objtodoList.AddTodoList(todoList);
            return new HttpResponseMessage { Content = new StringContent("Is Scuessed !", Encoding.GetEncoding("UTF-8"), "application/json") };
        }

        [HttpGet]
        [Route("api/[controller]/GetLastSort")]
        public int GetLastSort() => objtodoList.GetLastSort();

        [HttpGet]
        [Route("api/[controller]/GetLastSortByItem")]
        public int GetLastSortByItem(int listId) => objtodoList.GetLastSortByItem(listId);

        [HttpPost]
        [Route("api/[controller]/CreateTask")]
        public HttpResponseMessage CreateTask([FromBody] string jsonRequest)
        {
            var todoItem = JsonSerializer.Deserialize<TodoItem>(jsonRequest);
            objtodoList.AddTodoItem(todoItem);
            return new HttpResponseMessage { Content = new StringContent("Is Scuessed !", Encoding.GetEncoding("UTF-8"), "application/json") };
        }

        [HttpGet]
        [Route("api/[controller]/Details/{id}")]
        public TodoList Details(int id) => objtodoList.GetTodoListData(id);

        [HttpPut]
        [Route("api/[controller]/Edit")]
        public void Edit([FromBody] TodoList todoList)
        {
            if (ModelState.IsValid)
                objtodoList.UpdateTodoList(todoList);
        }

        [HttpPut]
        [Route("api/[controller]/EditByItem")]
        public HttpResponseMessage EditByItem([FromBody] string jsonRequest)
        {
            var todoItem = JsonSerializer.Deserialize<TodoItem>(jsonRequest);
            objtodoList.UpdateTodoItem(todoItem);
            return new HttpResponseMessage { Content = new StringContent("Is Scuessed !", Encoding.GetEncoding("UTF-8"), "application/json") };
        }

        [HttpDelete]
        [Route("api/[controller]/Delete/{id}")]
        public void Delete(int id)
        {
            objtodoList.DeleteTodoList(id);
        }
    }
}
