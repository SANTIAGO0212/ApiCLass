using System.ComponentModel.DataAnnotations;

namespace Api_Sat_2023.DAL.Entities
{
    public class State : AuditBase
    {
        [Display(Name = "Estado/Departamento")] //Para pintar el nombre bien bonito en el front 
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")] //Logitud de caráteres +
                                                                                        //máxima que esta propiedad me permite tener
        [Required(ErrorMessage = "¡El campo {0} es obligatorio")]
        public string Name { get; set; } //varchar(50)

        [Display(Name = "País")]
        //Relación con country
        public Country? Country { get; set; } // Esto representa un OBJETO DE COUNTRY

        [Display(Name = "Id País")]
        public Guid CountryId { get; set; } // FOREIGN KEY
    }
}
