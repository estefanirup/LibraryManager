using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::LibraryManager.Model.Books;
using global::LibraryManager.Model.Users;
using global::LibraryManager.Repositories;
using LibraryManager.Model;
using LibraryManager.Model.Loans;

namespace LibraryManager.UI.Console.UI;

internal class LoanUI
{
    private Repository<Loan> loanRepository;
    private Repository<User> userRepository;
    private Repository<Book> bookRepository;
    public LoanUI()
    {
        loanRepository = new Repository<Loan>();
        userRepository = new Repository<User>();
        bookRepository = new Repository<Book>();
    }
    public void Menu()
    {
        string op;
        do
        {
            System.Console.Clear();
            System.Console.WriteLine("[0] Voltar");
            System.Console.WriteLine("[1] Fazer Emprestimo");
            System.Console.WriteLine("[2] Exibir Emprestimos");
            System.Console.WriteLine("[3] Devolver Emprestimo");
            System.Console.Write("Escolha uma opcao: ");
            op = System.Console.ReadLine();
            System.Console.Clear();

            switch (op)
            {
                case "1":
                    register();
                    System.Console.WriteLine("\nAperte Qualquer Tecla Para Voltar!");
                    System.Console.ReadKey();
                    break;
                case "2":
                    display();
                    System.Console.WriteLine("\nAperte Qualquer Tecla Para Voltar!");
                    System.Console.ReadKey();
                    break;
                case "3":
                    change();
                    System.Console.WriteLine("\nAperte Qualquer Tecla Para Voltar!");
                    System.Console.ReadKey();
                    break;
            }
        } while (op != "0");
    }

    public void register()
    {
        System.Console.WriteLine("=== Registro de Empréstimo ===");

        int userId;
        User user = null;
        do
        {
            System.Console.Write("Digite o ID do usuário: ");
            if (int.TryParse(System.Console.ReadLine(), out userId))
            {
                user = userRepository.GetById(userId);
                if (user == null)
                {
                    System.Console.WriteLine("Usuário não encontrado. Tente novamente.");
                }
            }
            else
            {
                System.Console.WriteLine("ID inválido. Por favor, insira um número válido.");
            }
        } while (user == null);

        int bookId;
        Book book = null;
        do
        {
            System.Console.Write("Digite o ID do livro: ");
            if (int.TryParse(System.Console.ReadLine(), out bookId))
            {
                book = bookRepository.GetById(bookId);
                if (book == null)
                {
                    System.Console.WriteLine("Livro não encontrado. Tente novamente.");
                }
                else if (book.Status != BookStatus.Available)
                {
                    System.Console.WriteLine("Livro não disponível para empréstimo. Escolha outro livro.");
                    book = null;
                }
            }
            else
            {
                System.Console.WriteLine("ID inválido. Por favor, insira um número válido.");
            }
        } while (book == null);

        DateTime loanDate = DateTime.Now;
        DateTime? returnDate = null;
        LoanStatus status = LoanStatus.Active;

        Loan loan = new Loan(userId, bookId, loanDate, returnDate, status);

        try
        {
            loanRepository.Create(loan);
            book.Status = BookStatus.Borrowed;
            bookRepository.Update(book);
            System.Console.WriteLine("\nEmpréstimo registrado com sucesso!");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Erro ao registrar empréstimo: {ex.Message}");
            System.Console.WriteLine($"Detalhes do erro: {ex.InnerException?.Message}");
        }
    }


    public void display()
    {
        System.Console.WriteLine("=== Lista de Emprestimos ===");
        List<Loan> loans = loanRepository.GetAll();
        foreach (var item in loans)
        {
            System.Console.WriteLine(item);
        }
    }

    public void change()
    {
        System.Console.WriteLine("=== Devolução de Livro ===");

        int loanId;
        Loan loan = null;
        do
        {
            System.Console.Write("Digite o ID do empréstimo: ");
            if (int.TryParse(System.Console.ReadLine(), out loanId))
            {
                loan = loanRepository.GetById(loanId);
                if (loan == null || loan.Status != LoanStatus.Active)
                {
                    System.Console.WriteLine("Empréstimo não encontrado ou já concluído. Tente novamente.");
                    loan = null;
                }
            }
            else
            {
                System.Console.WriteLine("ID inválido. Por favor, insira um número válido.");
            }
        } while (loan == null);

        DateTime returnDate = DateTime.Now;
        loan.ReturnDate = returnDate;
        loan.Status = LoanStatus.Completed;

        TimeSpan loanDuration = returnDate - loan.LoanDate;
        decimal lateFee = 0;

        if (loanDuration.TotalDays > 7)
        {
            lateFee = (decimal)(loanDuration.TotalDays - 7);
            System.Console.WriteLine($"Taxa de atraso: R${lateFee:0.00}");
            loan.Status = LoanStatus.Overdue;
        }

        loanRepository.Update(loan);

        Book book = bookRepository.GetById(loan.BookId);
        if (book != null)
        {
            book.Status = BookStatus.Available;
            bookRepository.Update(book);
        }

        System.Console.WriteLine("\nLivro devolvido com sucesso!");
    }
}
