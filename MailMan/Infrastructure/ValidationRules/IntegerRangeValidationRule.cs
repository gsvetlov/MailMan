using System.Globalization;
using System.Windows.Controls;

namespace MailMan.Infrastructure.ValidationRules
{
    class IntegerRangeValidationRule : ValidationRule
    {
        public int LowerBound { get; set; }
        public int UpperBound { get; set; }

        public override ValidationResult Validate(object obj, CultureInfo cultureInfo)
        {
            if (obj is not string str) return ResultFail($"Неверный тип объекта {obj.GetType()}");
            if (int.TryParse(str, out int value) is false) return ResultFail($"{str} невозможно перевести в число");
            if (value < LowerBound || value > UpperBound) return ResultFail($"Введите целое число в диапазоне {LowerBound} - {UpperBound}");
            return ValidationResult.ValidResult;
        }
        private ValidationResult ResultFail(string msg) => new ValidationResult(false, msg);
    }
}
