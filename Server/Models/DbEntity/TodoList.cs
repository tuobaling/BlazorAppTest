using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BlazorAppTest.Server.Models.DbEntity
{
    [Serializable]
    [Table("TodoList")]
    public partial class TodoList
    {
        public TodoList()
        {
            TodoItems = new HashSet<TodoItem>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Sort { get; set; }

        [InverseProperty(nameof(TodoItem.List))]
        public virtual ICollection<TodoItem> TodoItems { get; set; }
    }
}
