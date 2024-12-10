using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LibraryManager.Model;
using LibraryManager.Model.Books;
using LibraryManager.Model.Users;
using LibraryManager.Persistence;
using LibraryManager.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.UI.Console.UI
{
    internal class UserUI
    {
        private Repository<User> userRepository;
        public UserUI() {
            userRepository = new Repository<User>();
        }
        public void Menu()
        {
            string? op="Oi";
            while (op != "0")
            {
                System.Console.Clear();
                System.Console.WriteLine("[0] Voltar");
                System.Console.WriteLine("[1] Cadastrar Usuario");
                System.Console.WriteLine("[2] Exibir Usuarios");
                System.Console.WriteLine("[3] Alterar Usuario");
                System.Console.WriteLine("[4] Excluir Usuario");
                System.Console.Write("Escolha uma opcao: ");
                op = System.Console.ReadLine();
                System.Console.Clear();

                switch (op)
                {
                    case "1":
                        cadastrar();
                        break;
                    case "2":
                        exibir();
                        break;
                    case "3":
                        alterar();
                        break;
                    case "4":
                        excluir();
                        break;
                }
            }
        }

        public void cadastrar()
        {
        System.Console.WriteLine("Cadastre");
            User user = new User(
            name: "João Silva",
            email: "joao.silva@email.com",
            userType: "Admin",
            registerDate: DateTime.Now
            );

            userRepository.Create(user);
            System.Console.WriteLine("se deus quis, adicionou");
            System.Console.ReadKey();
        }

        public void exibir()
        {
            List<User> users = userRepository.GetAll();
            foreach (var item in users)
            {
                System.Console.WriteLine(item);
            }
            System.Console.WriteLine("\nAperte Qualquer Tecla Para Voltar!");
            System.Console.ReadKey();

        }

        public void alterar()
        {

        }

        public void excluir()
        {

        }
    }
}
