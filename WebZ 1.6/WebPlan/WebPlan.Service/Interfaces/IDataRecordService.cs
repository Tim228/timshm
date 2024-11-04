using WebPlan.Domain.Entity;

namespace WebPlan.Service.Interfaces
{
    public interface IDataRecordService
    {
        Task Create(DataRecord dataRecord);
        IQueryable<DataRecord> RecordService();
        Task<List<DataRecord>> ListService();
        IQueryable<DataRecord> Profile();
        Task DeleteService(int? id);
        Task<DataRecord> EditService(int? id);
        Task<DataRecord> EditSaveService(DataRecord dataRecord);
        IEnumerable<DataRecord> GoogleResposeService();
    }
}
