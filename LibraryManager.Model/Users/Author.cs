using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Model.Users;
public class Author
{
    public Author(string? name, string? nationality)
    {
        Name = name;
        Nationality = nationality;
    }

    public int? AuthorId { get; set; }
    public string? Name { get; set; }
    public string? Nationality { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }
        var other = (Author)obj;

        return base.Equals(AuthorId.HasValue && other.AuthorId.HasValue && AuthorId == other.AuthorId);
    }

    public override int GetHashCode()
    {
        return AuthorId.HasValue ? AuthorId.GetHashCode() : 0;
    }
    public override string ToString()
    {
        return $"Id: {AuthorId} Nome: {Name}, Nacionalidade: {Nationality}";
    }

}
