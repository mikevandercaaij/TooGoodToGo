using System.ComponentModel;
using System.Xml.Linq;

namespace Core.Domain.Enums
{
    public enum CityEnum
    {
        Breda,
        
        [Display(Name = "Den Bosch")]
        DenBosch,

        Tilburg
    }
}
