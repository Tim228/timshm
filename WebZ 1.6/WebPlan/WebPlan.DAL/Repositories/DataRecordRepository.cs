using WebPlan.DAL.Interfaces;
using WebPlan.Domain.Entity;

namespace WebPlan.DAL.Repositories
{
    public class DataRecordRepository : IBaseRepository<DataRecord>
    {
        private readonly AppDbContext _appDbContext;

        public DataRecordRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Create(DataRecord entity)
        {
            await _appDbContext.DataRecords.AddAsync(entity); //если не записывает в бд, то использовать return entity
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<DataRecord> GetAll()
        {
            return _appDbContext.DataRecords;
        }

        public async Task Delete(DataRecord entity)
        {
            _appDbContext.DataRecords.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<DataRecord> Update(DataRecord entity)
        {
            _appDbContext.DataRecords.Update(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }
    }
}
