namespace aluraflix_backend.Data.DTOs;
    public class ReadVideoDTO
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Url { get; set; }
        public int CategoriaID { get; set; }
    }
