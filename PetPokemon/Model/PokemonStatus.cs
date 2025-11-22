using System.ComponentModel.DataAnnotations;

namespace PetPokemon.Model
{
    public enum Humor
    {
        [Display(Name = "Depressivo")]
        Depressivo = 1,
        [Display(Name = "Triste")]
        Triste = 2,
        [Display(Name = "Neutro")]
        Neutro = 3,
        [Display(Name = "Feliz")]
        Feliz = 4,
        [Display(Name = "Muito feliz")]
        MuitoFeliz = 5
    }

    public enum Hunger
    {
        [Display(Name = "Morto de fome")]
        Faminto = 1,
        [Display(Name = "Com fome")]
        ComFome = 2,
        [Display(Name = "Sem fome")]
        SemFome = 3,
        [Display(Name = "Satisfeito")]
        Satisfeito = 4,
        [Display(Name = "Cheio")]
        Cheio = 5
    }

    public enum Energy
    {
        [Display(Name = "Exausto")]
        Exausto = 1,
        [Display(Name = "Cansado")]
        Cansado = 2,
        [Display(Name = "Acordado")]
        Acordado = 3,
        [Display(Name = "Cheio de energia")]
        Energético = 4,
        [Display(Name = "Muito agitado")]
        Agitado = 5
    }

    public enum Health
    {
        [Display(Name = "Crítica")]
        Crítica = 1,
        [Display(Name = "Precária")]
        Precária = 2,
        [Display(Name = "Boa")]
        Boa = 3,
        [Display(Name = "Saudável")]
        Saudável = 4,
        [Display(Name = "Excelente")]
        Excelente = 5
    }

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            var display = member?.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
            return display?.Name ?? value.ToString();
        }
    }
}
