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

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-P6IQ2NC;Database=TodoList_Blazor_NET5;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.Property(e => e.Id).HasComment("流水編號");

                entity.Property(e => e.ListId).HasComment("清單編號");

                entity.Property(e => e.TaskName).HasComment("任務名稱");

                entity.HasOne(d => d.List)
                    .WithMany(p => p.TodoItems)
                    .HasForeignKey(d => d.ListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TodoItem_TodoList");
            });

            modelBuilder.Entity<TodoList>(entity =>
            {
                entity.Property(e => e.Id).HasComment("流水編號");

                entity.Property(e => e.Title).HasComment("清單標題");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
