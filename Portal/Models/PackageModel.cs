using Core.Domain.Entities;
using Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Portal.Models
{
    public class PackageModel
    {
        public IList<string>? SelectedProducts { get; set; } 
        public IList<SelectListItem>? AvailableProducts { get; set; }

        public PackageModel()
        {
            SelectedProducts = new List<string>();
            AvailableProducts = new List<SelectListItem>();
        }

        [Required(ErrorMessage = "Vul een naam voor het pakket in!")]
        public string? Name { get; set; }

        public ICollection<Product>? Products { get; set; }

        [Required(ErrorMessage = "Geef aan wanneer het pakket opgehaald moet worden!")]
        public DateTime? PickUpTime { get; set; }

        [Required(ErrorMessage = "Geef aan tot wanneer het pakket opgehaald mag worden!")]
        public DateTime? LatestPickUpTime { get; set; }

        [Required(ErrorMessage = "Geef aan hoe duur het pakket moet worden!")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Geef het type pakket aan!")]
        public MealtypeEnum? MealType { get; set; }

        public Student? ReservedBy { get; set; }
    }
}
