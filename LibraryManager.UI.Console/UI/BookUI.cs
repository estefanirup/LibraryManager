using LibraryManager.Model.Books;
using LibraryManager.Model.Users;
using LibraryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.UI.Console.UI
{
    internal class BookUI
    {
        private Repository<Book> bookRepository;
        public BookUI()
        {
            bookRepository = new Repository<Book>();
        }
        public void Menu()
        {
            string op;
            do
            {
                System.Console.Clear();
                System.Console.WriteLine("[0] Voltar");
                System.Console.WriteLine("[1] Cadastrar Livro");
                System.Console.WriteLine("[2] Exibir Livros");
                System.Console.WriteLine("[3] Alterar Livros");
                System.Console.WriteLine("[4] Excluir Livro");
                System.Console.Write("Escolha uma opcao: ");
                op = System.Console.ReadLine();
                System.Console.Clear();

                switch (op)
                {
                    case "1":
                        //register();
                        System.Console.WriteLine("\nAperte Qualquer Tecla Para Voltar!");
                        System.Console.ReadKey();
                        break;
                    case "2":
                        //display();
                        System.Console.WriteLine("\nAperte Qualquer Tecla Para Voltar!");
                        System.Console.ReadKey();
                        break;
                    case "3":
                        //change();
                        System.Console.WriteLine("\nAperte Qualquer Tecla Para Voltar!");
                        System.Console.ReadKey();
                        break;
                    case "4":
                        //delete();
                        System.Console.WriteLine("\nAperte Qualquer Tecla Para Voltar!");
                        System.Console.ReadKey();
                        break;
                }
            } while (op != "0");
        }

    }
}