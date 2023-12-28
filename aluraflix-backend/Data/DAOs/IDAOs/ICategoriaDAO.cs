using aluraflix_backend.Models;

namespace aluraflix_backend.Data.DAOs.IDAOs;
    public interface ICategoriaDAO : IDAOBase<Categoria>
    {
        IEnumerable<Categoria> ExibirCategorias(int page = 1);
        Categoria ExibirCategoriaPorId(int id);
        Categoria ExibirVideosPorCategoriaId(int id);
    }