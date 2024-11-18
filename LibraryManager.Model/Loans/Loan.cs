using System;
using LibraryManager.Model.Books;
using LibraryManager.Model.Users;

namespace LibraryManager.Model.Loans;

public class Loan
{
    public Loan(User user, Book book, DateTime loanDate, DateTime? returnDate, LoanStatus status)
    {
        User = user;
        Book = book;
        LoanDate = loanDate;
        ReturnDate = returnDate;
        Status = status;
    }

    public int? LoanId { get; set; }
    public User User { get; set; }
    public Book Book { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public LoanStatus Status { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() !=  GetType())
        {
            return false;
        }
        var other = (Loan)obj;

        return base.Equals(LoanId.HasValue && other.LoanId.HasValue && LoanId == other.LoanId);
    }

    public override int GetHashCode()
    {
        return LoanId.HasValue ? LoanId.GetHashCode() : 0;
    }
    
}
