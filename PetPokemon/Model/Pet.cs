using PetPokemon.View;

namespace PetPokemon.Model
{
    public class Pet
    {
        // Pet attributes
        public string? Name { get; init; }
        public int Height { get; init; }
        public int Weight { get; init; }
        public int Level { get; private set; }
        
        // Pet status
        public int Humor { get; private set; }
        public int Hunger { get; private set; }
        public int Energy { get; private set; }
        public int Health { get; private set; }

        public Pet()
        {
            Random random = new();
            Humor = random.Next(1, 6);
            Hunger = random.Next(1, 6);
            Energy = random.Next(1, 6);
            Health = 5;
            Level = 1;
        }

        public void ShowAttributes()
        {
            Menu.ShowPetAttributes(this);
        }

        public void ShowStatus()
        {
            Menu.ShowPetStatus(this);
        }

        public void Play()
        {
            Humor = Math.Min(Humor + 1, 5);
            Hunger = Math.Max(Hunger - 1, 1);
            Energy = Math.Max(Energy - 1, 1);
            Menu.ShowPlayActionMessage();
            CheckHealth();
        }

        public void Feed()
        {
            Hunger = Math.Min(Hunger + 1, 5);
            Energy = Math.Min(Energy + 1, 5);
            Menu.ShowFeedActionMessage();
            CheckHealth();
        }

        public void Sleep()
        {
            Energy = Math.Min(Energy + 2, 5);
            Hunger = Math.Max(Hunger - 1, 1);
            Menu.ShowSleepActionMessage();
            CheckHealth();
        }

        public void CheckHealth()
        {
            if ((Hunger <= 1) || (Energy <= 1))
            {
                Health = Math.Max(Health - 1, 1);
            }
            else if ((Hunger >= 4) && (Energy >= 4))
            {
                Health = Math.Min(Health + 1, 5);
            }
        }
    }
}
