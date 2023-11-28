namespace aluraflix_backend.Models;

    public class Video
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Url { get; set; }
        public int? CategoriaID { get; set; }
        public virtual Categoria Categoria { get; set; }

    }