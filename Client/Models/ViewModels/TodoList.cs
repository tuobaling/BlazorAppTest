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
            TodoItems = new List<TodoItem>();
        }


        public int? ListId { get; set; }
   
        public string Title { get; set; }

        public int? Sort { get; set; }
        public virtual List<TodoItem> TodoItems { get; set; }
    }
}
