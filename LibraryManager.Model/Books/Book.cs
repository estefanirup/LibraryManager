using LibraryManager.Model.Users;
using System;

namespace LibraryManager.Model.Books;

public class Book
{
    public Book() {}
    public Book(string title, Author author, string isbn, int publicationYear, Category category)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;
        Status = BookStatus.Available;
        Category = category;
    }
    public int? BookId { get; set; }
    public int AuthorId { get; set; }
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public Author? Author { get; set; }
    public string ISBN { get; set; }
    public int PublicationYear { get; set; }
    public BookStatus Status { get; set; }
    public Category? Category { get; set; }

    public override string ToString()
    {
        return $"[ID: {BookId}, Title: {Title}, Author: {AuthorId}, ISBN: {ISBN}, Year: {PublicationYear}, Status: {Status}, Category: {CategoryId}]";
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType()){
            return false;
        }
        var other = (Book)obj;

        return base.Equals(
            BookId.HasValue && other.BookId.HasValue &&
                BookId == other.BookId
        );
    }

    public override int GetHashCode()
    {
        return BookId.HasValue ? BookId.GetHashCode() : 0;
    }
}

