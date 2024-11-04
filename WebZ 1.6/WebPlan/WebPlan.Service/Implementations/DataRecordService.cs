using WebPlan.Service.Interfaces;
using WebPlan.Domain.Entity;
using WebPlan.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebPlan.Service.Implementations
{
    public class DataRecordService : IDataRecordService
    {
        private readonly IBaseRepository<DataRecord> _dataRecordRepository;

        public DataRecordService(IBaseRepository<DataRecord> dataRecordRepository)
        {
            _dataRecordRepository = dataRecordRepository;
        }

        public static object? dateSelect;
        public static string? emailSelect;

        public IQueryable<DataRecord> RecordService()
        {
            IQueryable<DataRecord> recordIQuer = _dataRecordRepository.GetAll();
            var records = recordIQuer.Where(p => p.Calendar == (DateOnly?)dateSelect);
            return records;
        }

        public async Task Create(DataRecord dataRecord)
        {
            try
            {
                dataRecord.Calendar = (DateOnly?)dateSelect;

                IQueryable<DataRecord> recordIQuer = _dataRecordRepository.GetAll();
                var numbers = recordIQuer
                    .Where(p => p.Phone == dataRecord.Phone);
                foreach (var number in numbers)
                {
                    if (number != null)
                    {
                        //return "Record";
                    }
                }
                
                dataRecord.Email = emailSelect;  //Запись email в модель базы данных

                if (dataRecord.Phone != 70000000000) //номер выступает неким паролем к без записному List
                {
                    dataRecord.Role = new Role("user");
                    await _dataRecordRepository.Create(dataRecord);
                }
                else
                    dataRecord.Role = new Role("admin");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<DataRecord>> ListService()
        {
            var response = _dataRecordRepository.GetAll().ToListAsync();

            return await response;
        }

        public IQueryable<DataRecord> Profile()
        {
            var response = _dataRecordRepository.GetAll();
            return response;
        }

        public async Task DeleteService(int? id)
        {            
            DataRecord dataRecord = new DataRecord { Id = id.Value };
            await _dataRecordRepository.Delete(dataRecord);
        }

        public async Task<DataRecord> EditService(int? id)
        {
            var dataRecord =  _dataRecordRepository.GetAll().FirstOrDefaultAsync(p => p.Id == id);
            return await dataRecord;
        }

        public async Task<DataRecord> EditSaveService(DataRecord dataRecord)
        {
            var response = _dataRecordRepository.Update(dataRecord);
            return await response;
        }

        public IEnumerable<DataRecord> GoogleResposeService()
        {
            IEnumerable<DataRecord> recordIEnum = _dataRecordRepository.GetAll();
            return recordIEnum;
        }
    }
}
