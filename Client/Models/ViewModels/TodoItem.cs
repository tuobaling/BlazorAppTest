﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace BlazorAppTest.Client.Models.ViewModels
{
    public partial class TodoItem
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public int ListId { get; set; }

        public virtual TodoList List { get; set; }
    }
}
