using System;
using LibraryManager.Model.Users;
using LibraryManager.Repositories;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using global::LibraryManager.Model.Books;
using global::LibraryManager.Repositories;

namespace LibraryManager.UI.Console.UI
{
	internal class LoginUI
	{
		private Repository<User> userRepository;
		public User LoggedUser { get; private set; }

		public LoginUI()
		{
			userRepository = new Repository<User>();
		}

		public void Menu()
		{
			string op;
			do
			{
				System.Console.Clear();
				System.Console.WriteLine("=== Bem-vindo ao LibraryManager ===");
				System.Console.WriteLine("[1] Login");
				System.Console.WriteLine("[2] Registrar-se");
				System.Console.WriteLine("[0] Sair");
				System.Console.Write("Escolha uma opção: ");
				op = System.Console.ReadLine();
				System.Console.Clear();

				switch (op)
				{
					case "1":
						Login();
						break;
					case "2":
						Register();
						break;
					case "0":
						Environment.Exit(0);
						break;
					default:
						System.Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar...");
						System.Console.ReadKey();
						break;
				}
			} while (LoggedUser == null);
		}

		private void Login()
		{
			System.Console.WriteLine("=== Login ===");
			System.Console.Write("Digite seu e-mail: ");
			string email = System.Console.ReadLine()?.Trim().ToLower();

			var user = userRepository.GetAll().FirstOrDefault(u => u.Email.ToLower() == email);

			if (user != null)
			{
				LoggedUser = user;
				System.Console.WriteLine($"\nBem-vindo, {user.Name}!");
				System.Console.WriteLine("Pressione qualquer tecla para continuar...");
				System.Console.ReadKey();
			}
			else
			{
				System.Console.WriteLine("\nUsuário não encontrado! Pressione qualquer tecla para tentar novamente...");
				System.Console.ReadKey();
			}
		}

		private void Register()
		{
			System.Console.WriteLine("=== Registro de Novo Usuário ===");

			string name, email;
			do
			{
				System.Console.Write("Digite seu nome: ");
				name = System.Console.ReadLine();
			} while (string.IsNullOrWhiteSpace(name));

			do
			{
				System.Console.Write("Digite seu e-mail: ");
				email = System.Console.ReadLine()?.Trim().ToLower();
			} while (string.IsNullOrWhiteSpace(email) || userRepository.GetAll().Any(u => u.Email.ToLower() == email));

			System.Console.Write("Deseja se tornar Administrador? (S/N): ");
			string isAdminChoice = System.Console.ReadLine()?.Trim().ToUpper();

			string userType = "User";

			if (isAdminChoice == "S")
			{
				System.Console.Write("Digite o código de administrador: ");
				string secretCode = System.Console.ReadLine();

				if (secretCode == "ADM2024")
				{
					userType = "Admin";
					System.Console.WriteLine("Parabéns! Você se tornou um administrador.");
				}
				else
				{
					System.Console.WriteLine("Código incorreto! Você será registrado como usuário comum.");
				}
			}

			User newUser = new User(
				name: name,
				email: email,
				userType: userType,
				registerDate: DateTime.Now
			);

			try
			{
				userRepository.Create(newUser);
				System.Console.WriteLine($"\nUsuário registrado com sucesso como '{userType}'! Agora faça o login.");
				System.Console.WriteLine("Pressione qualquer tecla para continuar...");
				System.Console.ReadKey();
			}
			catch (Exception ex)
			{
				System.Console.WriteLine($"Erro ao registrar usuário: {ex.Message}");
				System.Console.WriteLine("Pressione qualquer tecla para tentar novamente...");
				System.Console.ReadKey();
			}
		}
	}
}
