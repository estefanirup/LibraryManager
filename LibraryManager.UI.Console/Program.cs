using LibraryManager.Model.Books;
using LibraryManager.UI.Console.UI;

internal class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("[0] Sair");
            Console.WriteLine("[1] Gerenciar Usuario");
            Console.WriteLine("[2] Gerenciar Livro");
            Console.WriteLine("[3] Gerenciar Autor");
            Console.WriteLine("[4] Gerenciar Categoria");
            Console.WriteLine("[5] Gerenciar Emprestimo");
            Console.Write("Escolha uma opcao: ");
            string op = Console.ReadLine();
            Console.Clear();
            switch (op)
            {
                case "0":
                    System.Environment.Exit(1);
                    break;
                case "1":
                    UserUI userUI = new UserUI();
                    userUI.Menu();
                    break;
                case "2":
                    BookUI bookUI = new BookUI();
                    bookUI.Menu();
                    break;
                case "3":
                    AuthorUI authorUI = new AuthorUI();
                    authorUI.Menu();
                    break;
                case "4":
                    CategoryUI categoryUI = new CategoryUI();
                    categoryUI.Menu();
                    break;
                case "5":
                    LoanUI loanUI = new LoanUI();
                    loanUI.Menu();
                    break;
            }
        }
    }
}