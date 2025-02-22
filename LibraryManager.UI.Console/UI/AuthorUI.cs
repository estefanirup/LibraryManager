using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::LibraryManager.Model.Users;
using global::LibraryManager.Repositories;

namespace LibraryManager.UI.Console.UI;

internal class AuthorUI
{
    private Repository<Author> authorRepository;
    public AuthorUI()
    {
        authorRepository = new Repository<Author>();
    }
    public void Menu()
    {
        string op;
        do
        {
            System.Console.Clear();
            System.Console.WriteLine("[0] Voltar");
            System.Console.WriteLine("[1] Cadastrar Autor");
            System.Console.WriteLine("[2] Exibir Autores");
            System.Console.WriteLine("[3] Alterar Autor");
            System.Console.WriteLine("[4] Excluir Autor");
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
        } while (op != "0");
    }

    public void register()
    {
        System.Console.WriteLine("=== Cadastro de Autor ===");

        string _name;

        do
        {
            System.Console.Write("Digite o nome do autor: ");
            _name = System.Console.ReadLine();

            if (string.IsNullOrWhiteSpace(_name))
            {
                System.Console.WriteLine("O nome não pode estar vazio. Por favor, tente novamente.");
            }
        } while (string.IsNullOrWhiteSpace(_name));

        string _nationality;
        do
        {
            System.Console.Write("Digite a nacionalidade do autor: ");
            _nationality = System.Console.ReadLine();

            if (string.IsNullOrWhiteSpace(_nationality))
            {
                System.Console.WriteLine("Nacionalidade inválida. Por favor, insira uma nacionalidade válida.");
            }
        } while (string.IsNullOrWhiteSpace(_nationality));

        Author author = new Author(
            name: _name,
            nationality: _nationality
        );

        try
        {
            authorRepository.Create(author);
            System.Console.WriteLine("\nAutor cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Erro ao cadastrar Autor: {ex.Message}");
        }
    }

    public void display()
    {
        System.Console.WriteLine("=== Lista de Autores ===");
        List<Author> authors = authorRepository.GetAll();
        foreach (var item in authors)
        {
            System.Console.WriteLine(item);
        }
    }

    public void change()
    {
        display();

        System.Console.WriteLine("\n=== Altere um Autor ===");

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

        Author author = authorRepository.GetById(id);

        string _name;

        System.Console.Write("Voce deseja alterar o nome do autor? ('S' para Sim): ");
        string op = System.Console.ReadLine();
        if (op == "S" || op == "s")
        {
            do
            {
                System.Console.Write("Digite o novo nome do autor: ");
                _name = System.Console.ReadLine();

                if (string.IsNullOrWhiteSpace(_name))
                {
                    System.Console.WriteLine("O nome não pode estar vazio. Por favor, tente novamente.");
                }
                else
                {
                    author.Name = _name;
                }
            } while (string.IsNullOrWhiteSpace(_name));
        }

        System.Console.Write("Voce deseja alterar a nacionalidade do autor? ('S' para Sim): ");
        op = System.Console.ReadLine();
        if (op == "S" || op == "s")
        {
            string _nationality;
            do
            {
                System.Console.Write("Digite a nova nacionalidade do autor: ");
                _nationality = System.Console.ReadLine();

                if (string.IsNullOrWhiteSpace(_nationality))
                {
                    System.Console.WriteLine("Nacionalidade inválida. Por favor, insira uma nacionalidade válida.");
                }
                else
                {
                    author.Nationality = _nationality;
                }
            } while (string.IsNullOrWhiteSpace(_nationality));
        }

        try
        {
            authorRepository.Update(author);
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

        System.Console.WriteLine("\n=== Delete um Autor ===");
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
            authorRepository.Delete(id);
            System.Console.WriteLine($"Item com ID {id} deletado com sucesso.");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Erro ao deletar o item: {ex.Message}");
        }
    }

    public bool itemExists(int id)
    {
        List<Author> authors = authorRepository.GetAll();
        return authors.Any(item => item.AuthorId == id);
    }
}