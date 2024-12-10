namespace LibraryManager.Model.Users;

public class User
{
    public User(string? name, string? email, string? userType, DateTime? registerDate)
    {
        Name = name;
        Email = email;
        UserType = userType;
        RegisterDate = registerDate;
    }

    public int? UserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? UserType { get; set; }
    public DateTime? RegisterDate { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() !=  GetType())
        {
            return false;
        }
        var other = (User)obj;

        return base.Equals(UserId.HasValue && other.UserId.HasValue && UserId == other.UserId);
    }

    public override int GetHashCode()
    {
        return UserId.HasValue ? UserId.GetHashCode() : 0;
    }
    public override string ToString()
    {
        return $"Id: {UserId} Nome: {Name}, Email: {Email}, Data Registro: {RegisterDate:dd/MM/yyyy}";
    }

}
