using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BlazorAppTest.Server.Models.DbEntity; //排序+刪除沒用的

namespace BlazorAppTest.Server.DataAccess
{
    public class TodoListDataAccessLayer //多的時候可以考慮<T>泛型
    {
        private readonly TodoList_Blazor_NET5Context _dbcontext;

        public TodoListDataAccessLayer(TodoList_Blazor_NET5Context dbcontext) => _dbcontext = dbcontext;

        //To Get all TodoLists details
        public IEnumerable<TodoList> GetAllTodoLists() => _dbcontext.TodoLists.Include(x => x.TodoItems).OrderByDescending(t => t.ListId).ToList(); //讀取的時候不會出錯，最多是空集合

        //To Add new TodoList record
        public TodoList AddTodoList(TodoList TodoList) //新增和修改才會出錯，而且是在SaveChanges()才會錯，所以把try catch把共用出去。
        {
            _dbcontext.TodoLists.Add(TodoList);
            SaveChanges();

            return TodoList;
        }

        //To Add new TodoItem record
        public TodoItem AddTodoItem(TodoItem todoItem) //新增和修改才會出錯，而且是在SaveChanges()才會錯，所以把try catch把共用出去。
        {
            _dbcontext.TodoItems.Add(todoItem);
            SaveChanges();

            return todoItem;
        }

        //To Update the records of a particluar TodoList
        public void UpdateTodoList(TodoList todoList) //新增和修改才會出錯，而且是在SaveChanges()才會錯，所以把try catch把共用出去。
        {
            _dbcontext.TodoLists.Attach(todoList); //透過Key直接對應資料庫物件，可省去find(id)，但必須傳入完整物件，否則缺少部分將會蓋過。
            _dbcontext.Entry(todoList).State = EntityState.Modified;
            SaveChanges();
        }

        //To Update the records of a particluar TodoItem
        public void UpdateTodoItem(TodoItem todoItem) //新增和修改才會出錯，而且是在SaveChanges()才會錯，所以把try catch把共用出去。
        {
            _dbcontext.TodoItems.Attach(todoItem);
            _dbcontext.Entry(todoItem).State = EntityState.Modified;
            SaveChanges();
        }

        //Get the details of a particular TodoList
        public TodoList GetTodoListData(int id) => _dbcontext.TodoLists.Find(id); //Find不會錯，最多回傳null

        //To Delete the record of a particular TodoList
        public void DeleteTodoList(int id) //新增和修改才會出錯，而且是在SaveChanges()才會錯，所以把try catch把共用出去。
        {
            var list = _dbcontext.TodoLists.Where(e => e.ListId == id).Include(e => e.TodoItems).First();
            _dbcontext.TodoLists.Remove(list);
            SaveChanges();
        }

        //To Delete the record of a particular TodoItem
        public void DeleteTodoItem(int id) //新增和修改才會出錯，而且是在SaveChanges()才會錯，所以把try catch把共用出去。
        {
            var item = _dbcontext.TodoItems.Find(id);
            _dbcontext.TodoItems.Remove(item);
            SaveChanges();
        }

        public int GetLastId() => _dbcontext.TodoLists.Select(a => a.ListId).OrderByDescending(t => t).FirstOrDefault(); //先寫.Select()再取值就不會因為null不能調用屬性而出錯，這裡ListId應該會跟資料庫一樣是not null才對，是否有手動修改為int?。

        public int GetLastSort() => (int)_dbcontext.TodoLists.Select(a => a.Sort).OrderByDescending(t => t).FirstOrDefault(); //先寫.Select()再取值就不會因為null不能調用屬性而出錯。

        public int GetLastSortByItem(int listId)
        {
            var queryResult = _dbcontext.TodoItems.Where(w => w.ListId == listId).Select(t => t.Sort).OrderByDescending(t => t).FirstOrDefault(); //先寫.Select()再取值就不會因為null不能調用屬性而出錯。
            return queryResult != null ? (int)queryResult : 1;
        }

        private void SaveChanges() //共用
        {
            try
            {
                _dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}