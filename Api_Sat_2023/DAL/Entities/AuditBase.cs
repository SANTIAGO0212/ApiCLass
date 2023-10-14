using System.ComponentModel.DataAnnotations;

namespace Api_Sat_2023.DAL.Entities
{
    public class AuditBase
    {
        [Key] //DataAnnotation me sirve para definir esta propiedad ID es un PK
        [Required] //Para campos obligatorios, o sea que deben tomar un valor  (no permite nulls)
        public virtual Guid Id { get; set; } //Será la PK de todas las tablas de mi BD
        public virtual DateTime? CreatedDate { get; set; } //Campos nuleables, notación elvis(?)
        public virtual DateTime? ModifiedBase { get; set; }
    }
}
