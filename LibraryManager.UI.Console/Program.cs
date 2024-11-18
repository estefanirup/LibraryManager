using LibraryManager.Model;
using LibraryManager.Model.Books;
using LibraryManager.Persistence;

internal class Program
{
    private static void Main(string[] args)
    {
        var category = new Category
        {
            CategoryId = 1,
            Name = "Fantasy",
            Description = "Genre of speculative fiction which involves themes of the supernatural, magic, and imaginary worlds and creatures."
        };

        var book = new Book
        {
            BookId = 2,
            Title = "Fourth Wing",
            Author = "Rebecca Yarros",
            ISBN = "9781649373403",
            PublicationYear = 2023,
            Status = BookStatus.Available,
            Category = category
        };

        var context = new LibraryManagerContext();
        context.Add(book);
        context.SaveChanges();

        Console.WriteLine(book.BookId);
    }
}