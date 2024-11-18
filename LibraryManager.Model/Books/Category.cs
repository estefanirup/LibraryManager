using System;

namespace LibraryManager.Model.Books;

public class Category
{
    public Category() {}

    public int? CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }


    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }
        var other = (Category)obj;

        return base.Equals(
            CategoryId.HasValue && other.CategoryId.HasValue &&
                CategoryId == other.CategoryId
        );
    }

    public override int GetHashCode()
    {
        return CategoryId.HasValue ? CategoryId.GetHashCode() : 0;
    }
}