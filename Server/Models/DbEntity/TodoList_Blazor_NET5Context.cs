using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BlazorAppTest.Server.Models.DbEntity
{
    public partial class TodoList_Blazor_NET5Context : DbContext
    {
        public TodoList_Blazor_NET5Context()
        {
        }

        public TodoList_Blazor_NET5Context(DbContextOptions<TodoList_Blazor_NET5Context> options)
            : base(options)
        {
        }

        public virtual DbSet<TodoItem> TodoItems { get; set; }
        public virtual DbSet<TodoList> TodoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.Property(e => e.ItemId).HasComment("流水編號");

                entity.Property(e => e.Completed).HasComment("已完成");

                entity.Property(e => e.ListId).HasComment("清單編號");

                entity.Property(e => e.Sort).HasComment("順序");

                entity.Property(e => e.TaskName).HasComment("任務名稱");

                entity.HasOne(d => d.List)
                    .WithMany(p => p.TodoItems)
                    .HasForeignKey(d => d.ListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TodoItem_TodoList");
            });

            modelBuilder.Entity<TodoList>(entity =>
            {
                entity.Property(e => e.ListId).HasComment("流水編號");

                entity.Property(e => e.Sort).HasComment("順序");

                entity.Property(e => e.Title).HasComment("清單標題");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
