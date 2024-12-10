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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /*if (!optionsBuilder.IsConfigured){
            optionsBuilder.UseSqlite(
                $"Data Source = ../LibraryManager.UI.Console/POO_LibraryManager"
            );
        }*/
        optionsBuilder.UseSqlite("Data Source=POO_LibraryManager.db");
    }
}
