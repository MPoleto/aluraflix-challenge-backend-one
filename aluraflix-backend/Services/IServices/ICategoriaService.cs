using aluraflix_backend.Data.DTOs;

namespace aluraflix_backend.Services.IServices;
    public interface ICategoriaService
    {
        IEnumerable<ReadCategoriaDTO> ExibirCategorias(int page = 1);
        ReadCategoriaDTO ExibirVideosPorCategoriaId(int id);
        ReadCategoriaDTO ExibirCategoriaPorId(int id);
        ReadCategoriaDTO CriarCategoria(CreateCategoriaDTO categoriaDTO);
        ReadCategoriaDTO AtualizarCategoria(int id, UpdateCategoriaDTO categoriaDTO);
        UpdateCategoriaDTO BuscarCategoriaParaAtualizarParcial(int id);
        void DeletarCategoria(int id);
    }
