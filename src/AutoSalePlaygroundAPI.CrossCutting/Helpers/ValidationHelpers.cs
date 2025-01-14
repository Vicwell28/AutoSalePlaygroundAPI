using FluentValidation;
using System.Text.RegularExpressions;

namespace AutoSalePlaygroundAPI.CrossCutting.Helpers
{
    public static class ValidationHelpers
    {
        public static bool BeAValidString(string value)
        {
            return !string.IsNullOrWhiteSpace(value) && value.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }

        public static bool BeAValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);

                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool BeAValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            var regex = @"^\+?[1-9]\d{1,14}$";

            return Regex.IsMatch(phoneNumber, regex);
        }

        public static bool BeAValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            return Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        public static bool BeAValidDateInRange(DateTime date, DateTime minDate, DateTime maxDate)
        {
            return date >= minDate && date <= maxDate;
        }

        public static bool MatchRegex(string value, string pattern)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return Regex.IsMatch(value, pattern);
        }

        public static bool BeAValidEnum<TEnum>(TEnum value) where TEnum : struct, Enum
        {
            return Enum.IsDefined(typeof(TEnum), value);
        }

        public static bool NotContainForbiddenWords(string value, List<string> forbiddenWords)
        {
            if (string.IsNullOrWhiteSpace(value))
                return true;

            foreach (var word in forbiddenWords)
            {
                if (value.Contains(word, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return true;
        }

        public static bool HaveMinimumAge(DateTime birthDate, int minimumAge)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age >= minimumAge;
        }

        //Métodos de extensión
        public static IRuleBuilderOptions<T, string> ValidEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("El correo electrónico es requerido.")
                .Must(BeAValidEmail).WithMessage("El correo electrónico no es válido.");
        }

        public static IRuleBuilderOptions<T, string> ValidPhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("El número de teléfono es requerido.")
                .Must(BeAValidPhoneNumber).WithMessage("El número de teléfono no es válido.");
        }

        public static IRuleBuilderOptions<T, string> ValidUrl<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(url => string.IsNullOrEmpty(url) || BeAValidUrl(url))
                .WithMessage("La URL no es válida.");
        }

        public static IRuleBuilderOptions<T, DateTime> DateInRange<T>(this IRuleBuilder<T, DateTime> ruleBuilder, DateTime minDate, DateTime maxDate)
        {
            return ruleBuilder
                .Must(date => BeAValidDateInRange(date, minDate, maxDate))
                .WithMessage($"La fecha debe estar entre {minDate.ToShortDateString()} y {maxDate.ToShortDateString()}.");
        }
    }
}
