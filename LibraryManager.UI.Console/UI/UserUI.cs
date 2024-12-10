using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Model;
using LibraryManager.Model.Users;
using LibraryManager.Repositories;

namespace LibraryManager.UI.Console.UI
{
    internal class UserUI
    {
        public void Menu()
        {
            while (true)
            {
                System.Console.Clear();
                System.Console.WriteLine("[0] Voltar");
                System.Console.WriteLine("[1] Cadastrar Usuario");
                System.Console.WriteLine("[2] Exibir Usuarios");
                System.Console.WriteLine("[3] Alterar Usuario");
                System.Console.WriteLine("[4] Excluir Usuario");
                System.Console.Write("Escolha uma opcao: ");
                string op = System.Console.ReadLine();
                System.Console.Clear();
                
                if (op == "0"){
                    break;
                }

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
            Repository<User> userRepository = new Repository<User>();
            System.Console.WriteLine("Cadastre");
            User user = new User(
            name: "João Silva",
            email: "joao.silva@email.com",
            userType: "Admin",
            registerDate: DateTime.Now
            );

            userRepository.Create(user);

        }

        public void exibir()
        {
            Repository<User> userRepository = new Repository<User>();
            List<User> users = userRepository.GetAll();
            System.Console.WriteLine(users[0].Name);

        }

        public void alterar()
        {

        }

        public void excluir()
        {

        }
    }
}
