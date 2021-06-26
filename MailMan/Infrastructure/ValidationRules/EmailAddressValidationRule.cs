using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Controls;

using MailMan.Services.EMailAddressValidator;

using Microsoft.Extensions.DependencyInjection;

namespace MailMan.Infrastructure.ValidationRules
{
    public class EmailAddressValidationRule : ValidationRule
    {
        private static IEmailAddressValidator addressValidator = App.Services.GetRequiredService<IEmailAddressValidator>();

        public override ValidationResult Validate(object value, CultureInfo culture)
        {
            if (value is not string address) throw new ArgumentException($"Invalid argument type {typeof(ValueTask)}", nameof(value));
            if (addressValidator.Check(address))
                return ValidationResult.ValidResult;
            return new ValidationResult(false, "Неверный формат адреса электронной почты");
        }
    }
}
