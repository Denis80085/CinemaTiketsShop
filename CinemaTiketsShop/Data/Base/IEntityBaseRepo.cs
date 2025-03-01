namespace CinemaTiketsShop.Data.Base
{
    public interface IEntityBaseRepo<T> where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T?> Create(T entity);
        Task<T?> Update(int id, T entity);
        Task<T?> Delete(T entity);
    }
}
