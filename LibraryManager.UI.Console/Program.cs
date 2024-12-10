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
            }
        }
    }
}