using System.ComponentModel.DataAnnotations;

namespace CustomersApi.Dtos
{
    public class CreateCustomerDto
    {
        [Required (ErrorMessage = "El nombre propio tiene que estar especificado")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido propio tiene que estar especificado")]
        public string LastName { get; set; }
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El formato del email no es válido.")]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
