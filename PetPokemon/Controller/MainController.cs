using PetPokemon.Model;
using PetPokemon.View;
using RestSharp;

namespace PetPokemon.Controller
{
    class MainController
    {
        private readonly RestClient _client;
        private const string BaseUrl = "https://pokeapi.co";
        private bool _isAdopting = false;
        private bool _closeApp = false;

        public MainController()
        {
            _client = HandleApiConnection();
        }

        public async Task Start()
        {
            await HandleUI();
        }

        async Task HandleUI()
        {
            Menu.ShowBanner();
            var username = Menu.GetUserName() ?? "usuário(a)";
            Menu.GreetUser(username);

            while (true)
            {
                await HandleAdoptionMenu(username);

                if (_isAdopting)
                {
                    var pet = StartAdoption();
                    HandlePetActionsMenu(pet);
                }

                if (_closeApp)
                {
                    Menu.FarewellUser(username);
                    break;
                }
            }
        }

        async Task HandleAdoptionMenu(string username)
        {
            while (true)
            {
                Menu.ShowAdoptionMenu();
                var opt = Menu.GetMenuChoice(0, 3);

                if (opt == 0)
                {
                    _closeApp = true;
                    return;
                }

                var id = MapChoiceToId(opt);

                var pokemon = await GetPokemonAsync(id)!;

                if (pokemon is null)
                {
                    Menu.ShowPokemonNotFoundError();
                    continue;
                }

                Menu.ShowPokemonInfo(pokemon);
                Menu.ShowAdoptionPrompt(pokemon.Name);
                var confirmation = Menu.GetAdoptionConfirmation();
                if (confirmation)
                {
                    _isAdopting = true;
                    Menu.ConfirmAdoption(username, pokemon.Name);
                    return;
                }
            }
        }

        static Pet StartAdoption() => new Pet();

        void HandlePetActionsMenu(Pet pet)
        {
            while (true)
            {
                Menu.ShowPetActionsMenu();
                var opt = Menu.GetMenuChoice(0, 4);

                if (opt == 0)
                {
                    _closeApp = true;
                    return;
                }

                switch (opt)
                {
                    case 1:
                        pet.Play(); break;
                    case 2:
                        pet.Feed(); break;
                    case 3:
                        pet.Sleep(); break;
                    case 4:
                        pet.ShowStatus(); break;
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
