using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAppTest.Server.Models.DbEntity;
using BlazorAppTest.Server.DataAccess;

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
        public void CreateList([FromBody] TodoList todoList)
        {
            objtodoList.AddTodoList(todoList);
        }

        //[HttpGet("GetLastId")]
        //[Route("api/[controller]/GetLastId")]
        //public int GetLastId() => objtodoList.GetLastId();

        [HttpGet]
        [Route("api/[controller]/GetLastSort")]
        public int GetLastSort() => objtodoList.GetLastSort();

        [HttpPost]
        [Route("api/[controller]/CreateTask")]
        public void CreateTask([FromBody] int listId, string newTodoText)
        {
            if (ModelState.IsValid)
            {
                TodoItem todoItem = new TodoItem
                {
                    ListId = listId,
                    TaskName = newTodoText
                };
                objtodoList.AddTodoItem(todoItem);
            }
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

        [HttpDelete]
        [Route("api/[controller]/Delete/{id}")]
        public void Delete(int id)
        {
            objtodoList.DeleteTodoList(id);
        }
    }
}
