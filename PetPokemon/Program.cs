using PetPokemon;
using RestSharp;

var options = new RestClientOptions("https://pokeapi.co");
var client = new RestClient(options);

int? ReadOption()
{
    Console.Write("Escolha uma opção (1-Bulbasaur, 2-Charmander, 3-Squirtle, 0-Sair): ");
    var input = Console.ReadLine();
    if (int.TryParse(input, out var value))
        return value;
    return null;
}

int MapChoiceToId(int choice) => choice switch
{
    1 => 1, // Bulbasaur
    2 => 4, // Charmander
    3 => 7, // Squirtle
    _ => -1
};

async Task ShowPokemonAsync(int id)
{
    try
    {
        var request = new RestRequest($"/api/v2/pokemon/{id}/", Method.Get);
        var pokemon = await client.GetAsync<Pokemon>(request);
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

            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Resposta vazia da API.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao consultar API: {ex.Message}");
    }
}

while (true)
{
    Console.WriteLine("===== Menu PetPokemon =====");
    Console.WriteLine("1) Bulbasaur");
    Console.WriteLine("2) Charmander");
    Console.WriteLine("3) Squirtle");
    Console.WriteLine("0) Sair");
    var opt = ReadOption();

    if (opt is null)
    {
        Console.WriteLine("Entrada inválida. Tente novamente.");
        continue;
    }

    if (opt == 0)
    {
        Console.WriteLine("Encerrando...");
        break;
    }

    var id = MapChoiceToId(opt.Value);
    if (id <= 0)
    {
        Console.WriteLine("Opção inválida. Tente novamente.");
        continue;
    }

    await ShowPokemonAsync(id);
    Console.WriteLine("Pressione Enter para continuar...");
    Console.ReadLine();
}
