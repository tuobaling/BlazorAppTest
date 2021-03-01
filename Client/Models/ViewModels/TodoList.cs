using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace BlazorAppTest.Client.Models.ViewModels
{
    public partial class TodoList
    {
        public TodoList()
        {
            
        }


        public int Id { get; set; }
   
        public string Title { get; set; }

        public virtual ICollection<TodoItem> TodoItems { get; set; }
    }
}
