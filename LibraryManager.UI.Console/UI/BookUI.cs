using LibraryManager.Model;
using LibraryManager.Model.Books;
using LibraryManager.Model.Users;
using LibraryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryManager.UI.Console.UI
{
    internal class BookUI
    {
        private Repository<Book> bookRepository;
        private Repository<Author> authorRepository;
        private Repository<Category> categoryRepository;
        public BookUI()
        {
            bookRepository = new Repository<Book>();
            authorRepository = new Repository<Author>();
            categoryRepository = new Repository<Category>();
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
            System.Console.WriteLine("=== Cadastro de Livro ===");

            string title;
            do
            {
                System.Console.Write("Digite o título do livro: ");
                title = System.Console.ReadLine();

                if (string.IsNullOrWhiteSpace(title))
                {
                    System.Console.WriteLine("O título não pode estar vazio. Por favor, tente novamente.");
                }
            } while (string.IsNullOrWhiteSpace(title));

            string isbn;
            do
            {
                System.Console.Write("Digite o ISBN do livro: ");
                isbn = System.Console.ReadLine();

                if (string.IsNullOrWhiteSpace(isbn))
                {
                    System.Console.WriteLine("O ISBN não pode estar vazio. Por favor, insira um ISBN válido.");
                }
            } while (string.IsNullOrWhiteSpace(isbn));

            int publicationYear;
            do
            {
                System.Console.Write("Digite o ano de publicação do livro: ");
            } while (!int.TryParse(System.Console.ReadLine(), out publicationYear) || publicationYear < 1000 || publicationYear > DateTime.Now.Year);

            int authorId;
            Author author = null;
            do
            {
                System.Console.Write("Digite o ID do autor: ");
                if (int.TryParse(System.Console.ReadLine(), out authorId))
                {
                    author = authorRepository.GetById(authorId);
                    if (author == null)
                    {
                        System.Console.WriteLine("Autor não encontrado. Tente novamente.");
                    }
                }
                else
                {
                    System.Console.WriteLine("ID inválido. Por favor, insira um número válido.");
                }
            } while (author == null);

            int categoryId;
            Category category = null;
            do
            {
                System.Console.Write("Digite o ID da categoria (opcional): ");
                if (int.TryParse(System.Console.ReadLine(), out categoryId))
                {
                    category = categoryRepository.GetById(categoryId);
                    if (category == null)
                    {
                        System.Console.WriteLine("Categoria não encontrada. Tente novamente.");
                    }
                }
                else
                {
                    category = null;
                    break;
                }
            } while (category == null);

            Book book = new Book
            {
                Title = title,
                AuthorId = authorId,
                ISBN = isbn,
                PublicationYear = publicationYear,
                Status = BookStatus.Available,
                CategoryId = categoryId
            };

            try
            {
                bookRepository.Create(book);
                System.Console.WriteLine("\nLivro cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Erro ao cadastrar livro: {ex.Message}");
                System.Console.WriteLine($"Detalhes do erro: {ex.InnerException?.Message}");
            }
        }
        public void display()
        {
            System.Console.WriteLine("=== Lista de Livros ===");
            List<Book> books = bookRepository.GetAll();
            foreach (var item in books)
            {
                System.Console.WriteLine(item);
            }
        }
        public void change()
        {
            System.Console.WriteLine("=== Alterar Livro ===");

            int bookId;
            Book book = null;
            do
            {
                System.Console.Write("Digite o ID do livro que deseja alterar: ");
                if (int.TryParse(System.Console.ReadLine(), out bookId))
                {
                    book = bookRepository.GetById(bookId);
                    if (book == null)
                    {
                        System.Console.WriteLine("Livro não encontrado. Tente novamente.");
                    }
                }
                else
                {
                    System.Console.WriteLine("ID inválido. Por favor, insira um número válido.");
                }
            } while (book == null);

            System.Console.Write("Deseja alterar o título? ('S' para Sim): ");
            if (System.Console.ReadLine().Trim().ToUpper() == "S")
            {
                string _title;
                do
                {
                    System.Console.Write("Digite o novo título: ");
                    _title = System.Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(_title));
                book.Title = _title;
            }

            System.Console.Write("Deseja alterar o ISBN? ('S' para Sim): ");
            if (System.Console.ReadLine().Trim().ToUpper() == "S")
            {
                string _isbn;
                do
                {
                    System.Console.Write("Digite o novo ISBN: ");
                    _isbn = System.Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(_isbn));
                book.ISBN = _isbn;
            }

            System.Console.Write("Deseja alterar o ano de publicação? ('S' para Sim): ");
            if (System.Console.ReadLine().Trim().ToUpper() == "S")
            {
                int publicationYear;
                do
                {
                    System.Console.Write("Digite o novo ano de publicação: ");
                } while (!int.TryParse(System.Console.ReadLine(), out publicationYear));
                book.PublicationYear = publicationYear;
            }

            System.Console.Write("Deseja alterar o autor? ('S' para Sim): ");
            if (System.Console.ReadLine().Trim().ToUpper() == "S")
            {
                int authorId;
                Author author = null;
                do
                {
                    System.Console.Write("Digite o ID do novo autor: ");
                    if (int.TryParse(System.Console.ReadLine(), out authorId))
                    {
                        author = authorRepository.GetById(authorId);
                        if (author == null)
                        {
                            System.Console.WriteLine("Autor não encontrado. Tente novamente.");
                        }
                    }
                } while (author == null);
                book.Author = author;
            }

            System.Console.Write("Deseja alterar a categoria? ('S' para Sim): ");
            if (System.Console.ReadLine().Trim().ToUpper() == "S")
            {
                int categoryId;
                Category category = null;
                do
                {
                    System.Console.Write("Digite o ID da nova categoria: ");
                    if (int.TryParse(System.Console.ReadLine(), out categoryId))
                    {
                        category = categoryRepository.GetById(categoryId);
                        if (category == null)
                        {
                            System.Console.WriteLine("Categoria não encontrada. Tente novamente.");
                        }
                    }
                } while (category == null);
                book.Category = category;
            }

            try
            {
                bookRepository.Update(book);
                System.Console.WriteLine("\nLivro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Erro ao atualizar livro: {ex.Message}");
            }
        }
        public void delete()
        {
            System.Console.WriteLine("=== Deletar Livro ===");

            int bookId;
            bool isValid;
            do
            {
                System.Console.Write("Digite o ID do livro que deseja deletar: ");
                string input = System.Console.ReadLine();
                isValid = int.TryParse(input, out bookId);

                if (!isValid)
                {
                    System.Console.WriteLine("Entrada inválida. Por favor, insira um número válido.");
                }
                else if (bookRepository.GetById(bookId) == null)
                {
                    System.Console.WriteLine($"Nenhum livro encontrado com o ID {bookId}. Por favor, tente novamente.");
                    isValid = false;
                }
            } while (!isValid);

            try
            {
                bookRepository.Delete(bookId);
                System.Console.WriteLine($"Livro com ID {bookId} deletado com sucesso.");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Erro ao deletar o livro: {ex.Message}");
            }
        }
    }
}