using System;
using LibraryManager.UI.Console.UI;

internal class Program
{
    private static void Main(string[] args)
    {
        LoginUI loginUI = new LoginUI();
        loginUI.Menu();

        UserUI userUI = new UserUI();
        BookUI bookUI = new BookUI();
        AuthorUI authorUI = new AuthorUI();
        CategoryUI categoryUI = new CategoryUI();
        LoanUI loanUI = new LoanUI();

        bool isAdmin = loginUI.LoggedUser.UserType == "Admin";

        while (true)
        {
            Console.Clear();
            Console.WriteLine("[0] Sair");
            if (isAdmin) Console.WriteLine("[1] Gerenciar Usuário");
            if (isAdmin) Console.WriteLine("[2] Gerenciar Livro");
            if (isAdmin) Console.WriteLine("[3] Gerenciar Autor");
            if (isAdmin) Console.WriteLine("[4] Gerenciar Categoria");
            Console.WriteLine("[5] Gerenciar Empréstimo");
            Console.Write("Escolha uma opção: ");

            string op = Console.ReadLine()?.Trim() ?? "";
            Console.Clear();
            switch (op)
            {
                case "0":
                    Environment.Exit(0);
                    break;
                case "1":
                    if (isAdmin) userUI.Menu();
                    else Console.WriteLine("Acesso negado! Apenas administradores podem gerenciar usuários.");
                    break;
                case "2":
                    if (isAdmin) bookUI.Menu();
                    else Console.WriteLine("Acesso negado! Apenas administradores podem gerenciar Livros.");
                    break;
                case "3":
                    if (isAdmin) authorUI.Menu();
                    else Console.WriteLine("Acesso negado! Apenas administradores podem gerenciar Autores.");
                    break;
                case "4":
                    if (isAdmin) categoryUI.Menu();
                    else Console.WriteLine("Acesso negado! Apenas administradores podem gerenciar Categorias.");
                    break;
                case "5":
                    loanUI.Menu();
                    break;
                default:
                    Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
