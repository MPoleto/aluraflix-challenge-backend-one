namespace aluraflix_backend.Models;
    public class Categoria
    {
        public int CategoriaID { get; set; }
        public string Titulo { get; set; }
        public string Cor { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
