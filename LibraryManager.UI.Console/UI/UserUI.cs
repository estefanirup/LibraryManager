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
            string op;
            do
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
                    case "4":
                        delete();
                        System.Console.WriteLine("\nAperte Qualquer Tecla Para Voltar!");
                        System.Console.ReadKey();
                        break;
                }
            } while (op != "0") ;
        }

        public void register()
        {
            System.Console.WriteLine("=== Cadastro de Usuário ===");

            string _name;

            do
            {
                System.Console.Write("Digite o nome do usuário: ");
                _name = System.Console.ReadLine();

                if (string.IsNullOrWhiteSpace(_name))
                {
                    System.Console.WriteLine("O nome não pode estar vazio. Por favor, tente novamente.");
                }
            }while (string.IsNullOrWhiteSpace(_name));

            string _email;
            do
            {
                System.Console.Write("Digite o e-mail do usuário: ");
                _email = System.Console.ReadLine();

                if (string.IsNullOrWhiteSpace(_email))
                {
                    System.Console.WriteLine("E-mail inválido. Por favor, insira um e-mail válido.");
                }
            } while (string.IsNullOrWhiteSpace(_email));
            
            User user = new User(
                name: _name,
                email: _email,
                userType: "User",
                registerDate: DateTime.Now
            );

            try
            {
                userRepository.Create(user);
                System.Console.WriteLine("\nUsuário cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Erro ao cadastrar usuário: {ex.Message}");
            }
        }

        public void display()
        {
            System.Console.WriteLine("=== Lista de Usuários ===");
            List<User> users = userRepository.GetAll();
            foreach (var item in users)
            {
                System.Console.WriteLine(item);
            }
        }

        public void change()
        {
            display();

            System.Console.WriteLine("\n=== Altere um Usuário ===");

            int id;
            bool isValid;
            do
            {
                System.Console.WriteLine("\nEscolha o ID para ser Alterado:");
                string input = System.Console.ReadLine();
                isValid = int.TryParse(input, out id);

                if (!isValid)
                {
                    System.Console.WriteLine("Entrada inválida. Por favor, insira um número válido.");
                }
                else if (!itemExists(id))
                {
                    System.Console.WriteLine($"Nenhum item encontrado com o ID {id}. Por favor, tente novamente.");
                    isValid = false;
                }
            } while (!isValid);

            User user = userRepository.GetById(id);

            string _name;

            System.Console.Write("Voce deseja alterar o nome do usuário? ('S' para Sim): ");
            string op = System.Console.ReadLine();
            if (op == "S" || op == "s")
            {
                do
                {
                    System.Console.Write("Digite o novo nome do usuário: ");
                    _name = System.Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(_name))
                    {
                        System.Console.WriteLine("O nome não pode estar vazio. Por favor, tente novamente.");
                    }
                    else
                    {
                        user.Name= _name;
                    }
                } while (string.IsNullOrWhiteSpace(_name));
            }

            System.Console.Write("Voce deseja alterar o email do usuário? ('S' para Sim): ");
            op = System.Console.ReadLine();
            if (op == "S" || op == "s")
            {
                string _email;
                do
                {
                    System.Console.Write("Digite o novo e-mail do usuário: ");
                    _email = System.Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(_email))
                    {
                        System.Console.WriteLine("E-mail inválido. Por favor, insira um e-mail válido.");
                    }
                    else
                    {
                        user.Email= _email;
                    }
                } while (string.IsNullOrWhiteSpace(_email));
            }

            try
            {
                userRepository.Update(user);
                System.Console.WriteLine($"Item com ID {id} alterado com sucesso.");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Erro ao deletar o item: {ex.Message}");
            }
        }

        public void delete()
        {
            display();

            System.Console.WriteLine("\n=== Delete um Usuário ===");
            int id;
            bool isValid;
            do 
            { 
                System.Console.WriteLine("\nEscolha o ID para ser deletado:");
                string input = System.Console.ReadLine();
                isValid = int.TryParse(input, out id);

                if (!isValid)
                {
                    System.Console.WriteLine("Entrada inválida. Por favor, insira um número válido.");
                }
                else if (!itemExists(id))
                {
                    System.Console.WriteLine($"Nenhum item encontrado com o ID {id}. Por favor, tente novamente.");
                    isValid = false;
                }
            } while (!isValid);

            try
            {
                userRepository.Delete(id);
                System.Console.WriteLine($"Item com ID {id} deletado com sucesso.");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Erro ao deletar o item: {ex.Message}");
            }
        }

        public bool itemExists(int id)
        {
            List<User> users = userRepository.GetAll();
            return users.Any(item=> item.UserId==id);
        }
    }
}
