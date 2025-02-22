using System;
using LibraryManager.Model;
using LibraryManager.Model.Books;
using LibraryManager.Model.Loans;
using LibraryManager.Model.Users;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Persistence;

public class LibraryManagerContext : DbContext 
{
    public DbSet<Book> Books { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Loan> Loans { get; set; }

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

        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Author");
            entity.HasKey(a => a.AuthorId);
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.ToTable("Loan");
            entity.HasKey(l => l.LoanId);

            entity.HasOne(l => l.User)
                  .WithMany()
                  .HasForeignKey(l => l.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(l => l.Book)
                  .WithMany()
                  .HasForeignKey(l => l.BookId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(l => l.Status)
                  .HasConversion(
                      v => v.ToString(),
                      v => (LoanStatus)Enum.Parse(typeof(LoanStatus), v));
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=C:\\Users\\enzod\\OneDrive\\Documentos\\.POO\\LibraryManager\\db\\POO_LibraryManage.db"); 
    }
}
