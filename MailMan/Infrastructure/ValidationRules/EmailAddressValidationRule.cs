using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

using MailMan.Services.EMailAddressValidator;

namespace MailMan.Infrastructure.ValidationRules
{
    public class EmailAddressValidationRule : ValidationRule
    {
        private static IEmailAddressValidator addressValidator = (IEmailAddressValidator)App.Services.GetService(typeof(IEmailAddressValidator));
               
        public override ValidationResult Validate(object value, CultureInfo cultureInfo, BindingExpressionBase owner) => base.Validate(value, cultureInfo, owner);
        public override ValidationResult Validate(object value, CultureInfo cultureInfo, BindingGroup owner) => base.Validate(value, cultureInfo, owner);


        public override ValidationResult Validate(object value, CultureInfo culture)
        {
            if (value is not string address) throw new ArgumentException(nameof(value), $"Invalid argument type {typeof(ValueTask)}");
            if (addressValidator.Check(address))
                return ValidationResult.ValidResult;
            return new ValidationResult(false, "Неверный формат алреса электронной почты");
        }
    }
}
