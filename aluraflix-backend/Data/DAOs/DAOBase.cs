using aluraflix_backend.Data.DAOs.IDAOs;
using Microsoft.EntityFrameworkCore;

namespace aluraflix_backend.Data.DAOs;
public abstract class DAOBase<T> : IDAOBase<T> where T : class
{
    private AluraflixContext _context;

    protected DAOBase(AluraflixContext context)
    {
        _context = context;
    }

    public IEnumerable<T> Exibir()
    {           
        return _context.Set<T>().AsNoTracking();
    }

    public void Adicionar(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Atualizar(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Deletar(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void Salvar()
    {
        _context.SaveChanges();
    }
}