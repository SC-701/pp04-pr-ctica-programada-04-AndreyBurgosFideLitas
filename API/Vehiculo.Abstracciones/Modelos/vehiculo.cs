using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;

namespace Vehiculo.Abstracciones.Modelos
{
    public class VehiculoBase
    {
        [Required(ErrorMessage = "La propiedad placa es requerida")]
        [RegularExpression(@"[A-Za-z]{3}-[0-9]{3}",
            ErrorMessage = "El formato de la placa debe ser ABC-123")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "La propiedad color es requerida")]
        [StringLength(40, MinimumLength = 4,
            ErrorMessage = "El color debe tener entre 4 y 40 caracteres")]
        public string Color { get; set; }

        [Required(ErrorMessage = "La propiedad año es requerida")]
        [Range(1900, 2099,
            ErrorMessage = "El año debe estar entre 1900 y 2099")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "La propiedad precio es requerida")]
        [Range(0.01, double.MaxValue,
            ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La propiedad correo es requerida")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
        public string CorreoPropietario { get; set; }

        [Required(ErrorMessage = "La propiedad teléfono es requerida")]
        [Phone(ErrorMessage = "El teléfono no tiene un formato válido")]
        public string Telefono { get; set; }
    }

    public class VehiculoRequest : VehiculoBase
    {
        [Required(ErrorMessage = "La marca es requerida")]
        public Guid IdModelo { get; set; }
    }

    public class VehiculoResponse : VehiculoBase
    {
        public Guid Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
    }

    public class VehiculoDetalle : VehiculoResponse
    {
        public bool RevisionValida { get; set; }
        public bool RegistroValido { get; set; }
    }
}
