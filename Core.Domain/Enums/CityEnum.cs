using System.ComponentModel;
using System.Xml.Linq;

namespace Core.Domain.Enums
{
    public enum CityEnum
    {
        Breda = 1,
        
        [Display(Name = "Den Bosch")]
        DenBosch,

        Tilburg
    }
}
