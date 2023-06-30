using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AVN.Models
{
    public class AcademicYearVM : BasicVM<int>
    {
        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("C")]
        [YearRange(2000, ErrorMessage = "Пожалуйста, введите действительный год между 2000 и текущим годом")]
        public int? YearFrom { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DisplayName("До")]
        [YearRange(2000, ErrorMessage = "Пожалуйста, введите действительный год между 2000 и текущим годом")]
        [GreaterThanYearTo(ErrorMessage = "Значение 'До' должно быть больше значения 'С'")]
        public int? YearTo { get; set; }

        [DisplayName("Учебный год")]
        public string? Name { get; set; }
    }
    public class GreaterThanYearToAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var yearTo = value as int?;
            var yearFrom = (int?)validationContext.ObjectType.GetProperty("YearFrom")?.GetValue(validationContext.ObjectInstance);

            if (yearFrom.HasValue && yearTo.HasValue && yearTo <= yearFrom)
            {
                return new ValidationResult(ErrorMessage);
            }

            if (yearFrom.HasValue && yearTo.HasValue && (yearTo - yearFrom) > 1)
            {
                return new ValidationResult("Значение 'До' должно быть больше значения 'С' на один год");
            }

            return ValidationResult.Success;
        }
    }
}
