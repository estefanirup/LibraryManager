using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::LibraryManager.Model.Users;
using global::LibraryManager.Repositories;
using LibraryManager.Model.Books;

namespace LibraryManager.UI.Console.UI;
internal class CategoryUI
{
    private Repository<Category> categoryRepository;
    public CategoryUI()
    {
        categoryRepository = new Repository<Category>();
    }
    public void Menu()
    {
        string op;
        do
        {
            System.Console.Clear();
            System.Console.WriteLine("[0] Voltar");
            System.Console.WriteLine("[1] Cadastrar Categoria");
            System.Console.WriteLine("[2] Exibir Categorias");
            System.Console.WriteLine("[3] Alterar Categoria");
            System.Console.WriteLine("[4] Excluir Categoria");
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
        System.Console.WriteLine("=== Cadastro de Categoria ===");

        string _name;

        do
        {
            System.Console.Write("Digite o nome da categoria: ");
            _name = System.Console.ReadLine();

            if (string.IsNullOrWhiteSpace(_name))
            {
                System.Console.WriteLine("O nome não pode estar vazio. Por favor, tente novamente.");
            }
        } while (string.IsNullOrWhiteSpace(_name));

        string _description;
        do
        {
            System.Console.Write("Digite uma descricao para a categoria: ");
            _description = System.Console.ReadLine();

            if (string.IsNullOrWhiteSpace(_description))
            {
                System.Console.WriteLine("Descricao inválida. Por favor, insira uma descricao válida.");
            }
        } while (string.IsNullOrWhiteSpace(_description));

        Category category = new Category(
            name: _name,
            description: _description
        );

        try
        {
            categoryRepository.Create(category);
            System.Console.WriteLine("\nCategoria cadastrada com sucesso!");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Erro ao cadastrar Categoria: {ex.Message}");
        }
    }

    public void display()
    {
        System.Console.WriteLine("=== Lista de Categorias ===");
        List<Category> categorys = categoryRepository.GetAll();
        foreach (var item in categorys)
        {
            System.Console.WriteLine(item);
        }
    }

    public void change()
    {
        display();

        System.Console.WriteLine("\n=== Altere uma Categoria ===");

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

        Category category = categoryRepository.GetById(id);

        string _name;

        System.Console.Write("Voce deseja alterar o nome da categoria? ('S' para Sim): ");
        string op = System.Console.ReadLine();
        if (op == "S" || op == "s")
        {
            do
            {
                System.Console.Write("Digite o novo nome da categoria: ");
                _name = System.Console.ReadLine();

                if (string.IsNullOrWhiteSpace(_name))
                {
                    System.Console.WriteLine("O nome não pode estar vazio. Por favor, tente novamente.");
                }
                else
                {
                    category.Name = _name;
                }
            } while (string.IsNullOrWhiteSpace(_name));
        }

        System.Console.Write("Voce deseja alterar a descricao da categoria? ('S' para Sim): ");
        op = System.Console.ReadLine();
        if (op == "S" || op == "s")
        {
            string _description;
            do
            {
                System.Console.Write("Digite a nova descricao da categoria: ");
                _description = System.Console.ReadLine();

                if (string.IsNullOrWhiteSpace(_description))
                {
                    System.Console.WriteLine("Descricao inválida. Por favor, insira uma descricao válida.");
                }
                else
                {
                    category.Description = _description;
                }
            } while (string.IsNullOrWhiteSpace(_description));
        }

        try
        {
            categoryRepository.Update(category);
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

        System.Console.WriteLine("\n=== Delete uma Categoria ===");
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
            categoryRepository.Delete(id);
            System.Console.WriteLine($"Item com ID {id} deletado com sucesso.");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Erro ao deletar o item: {ex.Message}");
        }
    }

    public bool itemExists(int id)
    {
        List<Category> categorys = categoryRepository.GetAll();
        return categorys.Any(item => item.CategoryId == id);
    }
}
