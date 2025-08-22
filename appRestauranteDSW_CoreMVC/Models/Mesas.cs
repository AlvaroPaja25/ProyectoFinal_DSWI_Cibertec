using System.ComponentModel.DataAnnotations;

namespace appRestauranteDSW_CoreMVC.Models
{
    public class Mesas
    {
        [Display(Name = "Id de Mesas")] public int? id { get; set; }
        [Display(Name = "Cantidad de Asientos")] public int? cantidad_asientos { get; set; }
        [Display(Name = "Estado de la Mesa")] public string? estado { get; set; }
    }
}
