using System.ComponentModel.DataAnnotations;

namespace aluraflix_backend.Data.DTOs
{
    public class UpdateVideoDTO
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50, ErrorMessage = "O titulo do vídeo precisa ter entre 3 e 50 caracteres", MinimumLength = 3)]
        public string Titulo { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100, ErrorMessage = "A descrição do vídeo precisa ter entre 5 e 100 caracteres", MinimumLength = 5)]
        public string Descricao { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Url)]
        public string Url { get; set; }
    }
}