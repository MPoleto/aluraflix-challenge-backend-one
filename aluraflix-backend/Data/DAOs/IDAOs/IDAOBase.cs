namespace aluraflix_backend.Data.DAOs.IDAOs;
    public interface IDAOBase<T>
    {
        IEnumerable<T> Exibir();
        void Adicionar(T entity);
        void Atualizar(T entity);
        void Deletar(T entity);
        void Salvar();
    }