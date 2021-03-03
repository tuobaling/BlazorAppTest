using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace BlazorAppTest.Client.Models.ViewModels
{
    public partial class TodoItem
    {
        public int ItemId { get; set; }
        public string TaskName { get; set; }
        public int ListId { get; set; }

        public bool Completed { get; set; }
        public int Sort { get; set; }
    }
}
