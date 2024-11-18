using System;

namespace LibraryManager.Model;

public enum BookStatus
{
    Available,
    Borrowed,
    Reserved
}

public enum LoanStatus
{
    Active,
    Completed,
    Overdue
}

