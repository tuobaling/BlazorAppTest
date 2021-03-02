using BlazorAppTest.Server.Models.DbEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAppTest.Server.DataAccess
{
    public class TodoListDataAccessLayer
    {
        private TodoList_Blazor_NET5Context _dbcontext;

        public TodoListDataAccessLayer(TodoList_Blazor_NET5Context dbcontext) => _dbcontext = dbcontext;

        //To Get all TodoLists details   
        public IEnumerable<TodoList> GetAllTodoLists()
        {
            try
            {
                return _dbcontext.TodoLists.Include(x => x.TodoItems).ToList();
            }
            catch
            {
                throw;
            }
        }

        //To Add new TodoList record     
        public void AddTodoList(TodoList TodoList)
        {
            try
            {
                _dbcontext.TodoLists.Add(TodoList);
                _dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }


        //To Add new TodoItem record     
        public void AddTodoItem(TodoItem todoItem)
        {
            try
            {
                _dbcontext.TodoItems.Add(todoItem);
                _dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particluar TodoList    
        public void UpdateTodoList(TodoList TodoList)
        {
            try
            {
                _dbcontext.Entry(TodoList).State = EntityState.Modified;
                _dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //Get the details of a particular TodoList    
        public TodoList GetTodoListData(int id)
        {
            try
            {
                TodoList TodoList = _dbcontext.TodoLists.Find(id);
                return TodoList;
            }
            catch
            {
                throw;
            }
        }

        //To Delete the record of a particular TodoList    
        public void DeleteTodoList(int id)
        {
            try
            {
                TodoList emp = _dbcontext.TodoLists.Find(id);
                _dbcontext.TodoLists.Remove(emp);
                _dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public int GetLastId()
        {
            try
            {
                return _dbcontext.TodoLists.OrderByDescending(t => t.Id).First().Id;
            }
            catch
            {
                throw;
            }

        }

        public int GetLastSort()
        {
            try
            {
                return _dbcontext.TodoLists.OrderByDescending(t => t.Sort).First().Sort;
            }
            catch
            {
                throw;
            }

        }
    }
}
