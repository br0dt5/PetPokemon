using PetPokemon;
using RestSharp;

var options = new RestClientOptions("https://pokeapi.co");
var client = new RestClient(options);

int MapChoiceToId(int choice) => choice switch
{
    1 => 1, // Bulbasaur
    2 => 4, // Charmander
    3 => 7, // Squirtle
    _ => -1
};

async Task<Pokemon?> GetPokemonAsync(int id)
{
    try
    {
        var request = new RestRequest($"/api/v2/pokemon/{id}/", Method.Get);
        var pokemon = await client.GetAsync<Pokemon>(request);

        return pokemon;
    }
    catch (Exception ex)
    {
        Menu.ShowApiError();
        return null;
    }
}

Menu.ShowBanner();
var username = Menu.GetUserName();
Menu.GreetUser(username);

while (true)
{
    Menu.ShowMenu();
    var opt = Menu.GetMenuChoice();

    if (opt == 0)
    {
        Menu.FarewellUser(username);
        break;
    }

    var id = MapChoiceToId(opt);

    var pokemon = await GetPokemonAsync(id)!;

    if (pokemon is null)
    {
        continue;
    }

    Menu.ShowPokemonInfo(pokemon);
    Menu.ShowAdoptionPrompt(pokemon.Name);
    var confirmation = Menu.GetAdoptionConfirmation();
    if (confirmation)
    {
        Menu.ConfirmAdoption(username, pokemon.Name);
        Menu.FarewellUser(username);
        break;
    }
}
