using System.ComponentModel.DataAnnotations;

namespace Api_Sat_2023.DAL.Entities
{
    public class Country : AuditBase
    {
        [Display(Name="País")] //Para pintar el nombre bien bonito en el front 
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")] //Logitud de caráteres +
                                                                    //máxima que esta propiedad me permite tener
        [Required(ErrorMessage = "¡El campo {0} es obligatorio")]
        public string Name { get; set; } //varchar(50)
    }
}
