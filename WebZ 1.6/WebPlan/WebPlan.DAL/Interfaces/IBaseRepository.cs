using WebPlan.Domain.Entity;

namespace WebPlan.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task Create(T entity);
        IQueryable<T> GetAll();
        Task Delete(T entity);
        Task<DataRecord> Update(DataRecord entity);
    }
}
