namespace aluraflix_backend.Data.DTOs;
    public class ReadCategoriaDTO
    {
        public int CategoriaID { get; set; }
        public string Titulo { get; set; }
        public string Cor { get; set; }
        public ICollection<ReadVideoDTO> Videos { get; set; }
    }
