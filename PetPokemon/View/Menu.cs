using PetPokemon.Model;

namespace PetPokemon.View
{
    public static class Menu
    {
        public static void ShowBanner()
        {
            Console.WriteLine(@"
.·:''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''':·.
: :                                                             : :
: :   ____      _   ____       _     __                         : :
: :  |  _ \ ___| |_|  _ \ ___ | | __/_/ _ __ ___   ___  _ __    : :
: :  | |_) / _ \ __| |_) / _ \| |/ / _ \ '_ ` _ \ / _ \| '_ \   : :
: :  |  __/  __/ |_|  __/ (_) |   <  __/ | | | | | (_) | | | |  : :
: :  |_|   \___|\__|_|   \___/|_|\_\___|_| |_| |_|\___/|_| |_|  : :
: :                                                             : :
'·:.............................................................:·'
");
        }

        public static string GetUserName()
        {
            while (true)
            {
                Console.Write("Por favor, informe o seu nome: ");
                string username = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrEmpty(username))
                {
                    Console.WriteLine("Nome inválido. Tente novamente!");
                    continue;
                }

                return username;
            }
        }

        public static void GreetUser(string username)
        {
            Console.WriteLine($"Olá {username}, seja bem vindo ao centro virtual de adoção PetPokémon.");
        }

        public static void ShowAdoptionMenu()
        {
            Console.WriteLine();
            Console.WriteLine("===================== Menu de Adoção ====================");
            Console.WriteLine("1) Bulbasaur");
            Console.WriteLine("2) Charmander");
            Console.WriteLine("3) Squirtle");
            Console.WriteLine("0) Sair");
        }

        public static void ShowPokemonInfo(Pokemon pokemon)
        {
            if (pokemon is not null)
            {
                Console.WriteLine();
                Console.WriteLine($"Nome: {pokemon.Name}");
                Console.WriteLine($"Peso: {pokemon.Weight}");
                Console.WriteLine($"Altura: {pokemon.Height}");

                // Tipos (mostrando em ordem de slot)
                if (pokemon.Types is { Length: > 0 })
                {
                    var ordered = pokemon.Types.OrderBy(t => t.Slot).Select(t => t.Type.Name);
                    Console.WriteLine($"Tipos: {string.Join(", ", ordered)}");
                }
                else
                {
                    Console.WriteLine("Tipos: —");
                }

                // Habilidades (mostrando se é hidden)
                if (pokemon.Abilities is { Length: > 0 })
                {
                    var abilities = pokemon.Abilities
                        .OrderBy(a => a.Slot)
                        .Select(a => a.IsHidden ? $"{a.Ability.Name} (hidden)" : a.Ability.Name);
                    Console.WriteLine($"Habilidades: {string.Join(", ", abilities)}");
                }
                else
                {
                    Console.WriteLine("Habilidades: —");
                }
            }
        }

        public static void ShowPokemonNotFoundError()
        {
            Console.WriteLine();
            Console.WriteLine("Pokémon não encontrado. Tente novamente!");
        }

        public static int GetMenuChoice(int minValue, int maxValue)
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("Escolha uma opção do menu: ");
                string input = Console.ReadLine() ?? string.Empty;
                if (int.TryParse(input, out int choice) && choice >= minValue && choice <= maxValue)
                {
                    return choice;
                }
                Console.WriteLine();
                Console.WriteLine("Opção inválida. Tente novamente!");
            }
        }

        public static void ShowAdoptionPrompt(string pokemonName)
        {
            Console.WriteLine();
            Console.WriteLine($"Você deseja adotar um {pokemonName}? (s/n)");
        }

        public static bool GetAdoptionConfirmation()
        {
            while (true)
            {
                string input = Console.ReadLine() ?? string.Empty;
                if (input.Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                } else if (input.Equals("n", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                Console.WriteLine();
                Console.WriteLine("Opção inválida. Por favor, responda com 's' para sim ou 'n' para não.");
            }
        }

        public static void ConfirmAdoption(string username, string pokemonName)
        {
            Console.WriteLine();
            Console.WriteLine($"Parabéns {username}! Você adotou um {pokemonName}!");
        }

        public static void ShowPetActionsMenu()
        {
            Console.WriteLine();
            Console.WriteLine("===================== Menu de Ações ====================");
            Console.WriteLine("1) Brincar");
            Console.WriteLine("2) Alimentar");
            Console.WriteLine("3) Dormir");
            Console.WriteLine("4) Ver status do pet");
            Console.WriteLine("0) Sair");
        }

        public static void ShowPlayActionMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Você brincou com seu pet!");
        }

        public static void ShowFeedActionMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Você alimentou seu pet!");
        }

        public static void ShowSleepActionMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Seu pet dormiu bem!");
        }

        public static void FarewellUser(string username)
        {
            Console.WriteLine();
            Console.WriteLine($"Obrigado por visitar o centro virtual de adoção PetPokémon, {username}! Até a próxima!");
        }

        public static void ShowApiError()
        {
            Console.WriteLine();
            Console.WriteLine("Desculpe, não foi possível obter as informações do Pokémon no momento. Tente novamente mais tarde.");
        }

        public static void ShowPetStatus(Pet pet)
        {
            Console.WriteLine();
            Console.WriteLine("Status do seu pet:");
            Console.WriteLine($"Humor: {((Humor)pet.Humor).ToString().ToLower()}");
            Console.WriteLine($"Fome: {((Hunger)pet.Hunger).ToString().ToLower()}");
            Console.WriteLine($"Energia: {((Energy)pet.Energy).ToString().ToLower()}");
            Console.WriteLine($"Saúde: {((Health)pet.Health).ToString().ToLower()}");
            Console.WriteLine($"Nível: {pet.Level}");
        }
    }
}
