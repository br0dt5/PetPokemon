using PetPokemon.Model;
using PetPokemon.View;
using RestSharp;

namespace PetPokemon.Controller
{
    class MainController
    {
        private readonly RestClient _client;
        private const string BaseUrl = "https://pokeapi.co";

        public MainController()
        {
            _client = HandleApiConnection();
        }

        public async Task StartGame()
        {
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

        }

        static RestClient HandleApiConnection()
        {
            var options = new RestClientOptions(BaseUrl);
            return new RestClient(options);
        }

        static int MapChoiceToId(int choice) => choice switch
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
                var pokemon = await _client.GetAsync<Pokemon>(request);

                return pokemon;
            }
            catch (Exception ex)
            {
                Menu.ShowApiError();
                return null;
            }
        }
    }
}
