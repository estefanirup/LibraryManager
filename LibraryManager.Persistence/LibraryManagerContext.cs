using System;
using LibraryManager.Model.Books;
using LibraryManager.Model.Users;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Persistence;

public class LibraryManagerContext : DbContext 
{
    public DbSet<Book> Books { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(u => u.UserId);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Books");
            entity.HasKey(b => b.BookId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");
            entity.HasKey(c => c.CategoryId);
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // criei uma pasta pra ficar o banco, ai fica sempre nessa, antes tava dando problema porque criava a pasta nos arquivos da compilacao
        optionsBuilder.UseSqlite("Data Source=../../../../db/POO_LibraryManage.db"); 
    }
}
