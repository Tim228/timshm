using System.ComponentModel.DataAnnotations;

namespace WebPlan.Domain.Entity
{
    public class DataRecord
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? Calendar { get; set; }

        [DataType(DataType.Time)]
        public TimeOnly? Time { get; set; }

        [DataType(DataType.PhoneNumber)]
        public decimal Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public Role? Role { get; set; }

        public DataRecord() { }
        public DataRecord(Role role)
        {
            Role = role;
        }
    }
}
