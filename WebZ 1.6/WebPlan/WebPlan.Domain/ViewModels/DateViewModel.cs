using System.ComponentModel.DataAnnotations;

namespace WebPlan.Domain.ViewModels
{
    public class DateViewModel
    {
        [Required(ErrorMessage = "Дата была не выбрана")]
        [DataType(DataType.Date)]
        public DateOnly? Calendar { get; set; }
    }
}
