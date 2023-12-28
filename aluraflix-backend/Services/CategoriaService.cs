using AutoMapper;

using aluraflix_backend.Data.DAOs.IDAOs;
using aluraflix_backend.Data.DTOs;
using aluraflix_backend.Services.IServices;
using aluraflix_backend.Models;

namespace aluraflix_backend.Services;
public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaDAO _categoriaDAO;
    private readonly IMapper _mapper;

    public CategoriaService(ICategoriaDAO categoriaDAO, IMapper mapper)
    {
        _categoriaDAO = categoriaDAO;
        _mapper = mapper;
    }

    public IEnumerable<ReadCategoriaDTO> ExibirCategorias(int page = 1)
    {
        var categorias = _categoriaDAO.ExibirCategorias(page);

        var categoriasDTO = _mapper.Map<List<ReadCategoriaDTO>>(categorias);
        return categoriasDTO;
    }

    public ReadCategoriaDTO ExibirVideosPorCategoriaId(int id)
    {
        var videosDaCategoria = _categoriaDAO.ExibirVideosPorCategoriaId(id);

        var videosDaCategoriaDTO = _mapper.Map<ReadCategoriaDTO>(videosDaCategoria);

        return videosDaCategoriaDTO;
    }

    public ReadCategoriaDTO ExibirCategoriaPorId(int id)
    {
        var categoria = _categoriaDAO.ExibirCategoriaPorId(id);

        if(categoria is null)
        {
            throw new InvalidOperationException("Essa categoria não existe.");
        }

        var categoriaDTO = _mapper.Map<ReadCategoriaDTO>(categoria);

        return categoriaDTO;
    }

    public ReadCategoriaDTO CriarCategoria(CreateCategoriaDTO categoriaDTO)
    {
        try
        {
            var categoria = _mapper.Map<CreateCategoriaDTO, Categoria>(categoriaDTO);
            _categoriaDAO.Adicionar(categoria);
            _categoriaDAO.Salvar();

            return _mapper.Map<ReadCategoriaDTO>(categoria);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Ocorreu um erro. Tente novamente. {ex.Message}");
        }
    }

    public ReadCategoriaDTO AtualizarCategoria(int id, UpdateCategoriaDTO categoriaDTO)
    {
        try
        {
            var categoria = _categoriaDAO.ExibirCategoriaPorId(id);

            _mapper.Map(categoriaDTO, categoria);
            _categoriaDAO.Atualizar(categoria);
            _categoriaDAO.Salvar();

            return _mapper.Map<ReadCategoriaDTO>(categoria);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Ocorreu um erro durante a atualização. Tente novamente. {ex.Message}");
        }
    }

    public UpdateCategoriaDTO BuscarCategoriaParaAtualizarParcial(int id)
    {
        var categoria = _categoriaDAO.ExibirCategoriaPorId(id);

        return _mapper.Map<UpdateCategoriaDTO>(categoria);
    }

    public void DeletarCategoria(int id)
    {
        try
        {
            var categoria = _categoriaDAO.ExibirCategoriaPorId(id);

            _categoriaDAO.Deletar(categoria);
            _categoriaDAO.Salvar();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Ocorreu um erro. Tente novamente. {ex.Message}");
        }
    }
}
