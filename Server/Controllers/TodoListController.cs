using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using BlazorAppTest.Server.Models.DbEntity;
using BlazorAppTest.Server.DataAccess; //排序+刪除沒用的

namespace BlazorAppTest.Server.Controllers
{
    [Route("api/[controller]")] //可於controller上方先進行基底Route，裡面的Route都會繼續延續下去，比較短
    public class TodoListController : Controller
    {
        private readonly TodoListDataAccessLayer objtodoList;

        public TodoListController(TodoListDataAccessLayer _objtodoList) => objtodoList = _objtodoList;

        [HttpGet]
        [Route("Index")]
        public IEnumerable<TodoList> Index() => objtodoList.GetAllTodoLists(); //單純喜歡一行

        [HttpPost]
        [Route("CreateList")]
        public TodoList CreateList([FromBody] TodoList todoList) //改為物件傳遞，將由WebApi的機制自動反序列化回物件
        {
            objtodoList.AddTodoList(todoList);
            return todoList;
        }

        [HttpGet]
        [Route("GetLastSort")]
        public int GetLastSort() => objtodoList.GetLastSort();

        [HttpGet]
        [Route("GetLastSortByItem/{listId}")]
        public int GetLastSortByItem(int listId) => objtodoList.GetLastSortByItem(listId);

        [HttpPost]
        [Route("CreateTask")]
        public TodoItem CreateTask([FromBody] TodoItem todoItem) //改為物件傳遞，將由WebApi的機制自動反序列化回物件
        {
            objtodoList.AddTodoItem(todoItem);
            return todoItem;
        }

        [HttpGet]
        [Route("Details/{id}")]
        public TodoList Details(int id) => objtodoList.GetTodoListData(id);

        [HttpPut]
        [Route("Edit")]
        public void Edit([FromBody] TodoList todoList)
        {
            if (ModelState.IsValid) //既然有If....那似乎就要有else，畢竟回傳void，先擱置
                objtodoList.UpdateTodoList(todoList);
        }

        [HttpPut]
        [Route("EditByItem")]
        public HttpResponseMessage EditByItem([FromBody] TodoItem todoItem) //改為物件傳遞，將由WebApi的機制自動反序列化回物件
        {
            objtodoList.UpdateTodoItem(todoItem);
            return new HttpResponseMessage { Content = new StringContent("Is Scuessed !", Encoding.GetEncoding("UTF-8"), "application/json") }; //編輯是可以不用回傳物件本身，因與畫面一致，等就看訊息要放這邊還是放其他地方，寫到再來改
        }

        [HttpDelete]
        [Route("DeleteList/{itemId}")] //拼錯
        public void Delete(int itemId) => objtodoList.DeleteTodoList(itemId); //如果有子項目，會錯，需要做額外設定

        [HttpDelete]
        [Route("DeleteItem/{itemId}")]
        public void DeleteItem(int itemId) => objtodoList.DeleteTodoItem(itemId);
    }
}