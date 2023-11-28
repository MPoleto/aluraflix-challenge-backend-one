using System.ComponentModel.DataAnnotations;

namespace aluraflix_backend.Data.DTOs
{
    public class UpdateCategoriaDTO
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50, ErrorMessage = "O titulo da categoria precisa ter entre 3 e 50 caracteres", MinimumLength = 3)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [RegularExpression(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "A cor precisa estar no padrão hexadecimal, exemplo: #fff ou #ffffff")]
        [StringLength(7, ErrorMessage = "A cor da categoria precisa ter entre 4 e 7 caracteres", MinimumLength = 4)]
        public string Cor { get; set; }
    }
}