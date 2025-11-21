using System.Text.Json.Serialization;

namespace PetPokemon.Model
{
    public record Pokemon(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("height")] int Height,
        [property: JsonPropertyName("weight")] int Weight,
        [property: JsonPropertyName("abilities")] AbilityEntry[] Abilities,
        [property: JsonPropertyName("types")] TypeEntry[] Types
    );

    public record AbilityEntry(
        [property: JsonPropertyName("ability")] NamedResource Ability,
        [property: JsonPropertyName("is_hidden")] bool IsHidden,
        [property: JsonPropertyName("slot")] int Slot
    );

    public record TypeEntry(
        [property: JsonPropertyName("slot")] int Slot,
        [property: JsonPropertyName("type")] NamedResource Type
    );

    public record NamedResource(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("url")] string Url
    );
}
