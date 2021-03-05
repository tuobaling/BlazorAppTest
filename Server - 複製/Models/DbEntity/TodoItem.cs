using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BlazorAppTest.Server.Models.DbEntity
{
    [Table("TodoItem")]
    public partial class TodoItem
    {
        [Key]
        public int ItemId { get; set; }
        [Required]
        public string TaskName { get; set; }
        public int ListId { get; set; }
        public bool? Completed { get; set; }
        public int? Sort { get; set; }

        [ForeignKey(nameof(ListId))]
        [InverseProperty(nameof(TodoList.TodoItems))]
        public virtual TodoList List { get; set; }
    }
}
