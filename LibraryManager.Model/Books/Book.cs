using System;

namespace LibraryManager.Model.Books;

public class Book
{
    public int? BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int PublicationYear { get; set; }
    public BookStatus Status { get; set; }
    public Category? Category { get; set; }

    public override string ToString()
    {
        return $"[ID: {BookId}, Title: {Title}, Author: {Author}, ISBN: {ISBN}, Year: {PublicationYear}, Status: {Status}, Category: {Category?.Name ?? "No Category"}]";
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

