using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BeautySalon.UI.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
internal sealed partial class PhoneNumberAttribute(string errorMessage) : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var str = value?.ToString();
        return string.IsNullOrWhiteSpace(str) ? null :
                GetPhoneRegex().IsMatch(str) ? ValidationResult.Success : new ValidationResult(errorMessage);
    }

    [GeneratedRegex(@"^\+375(25|29|33|44)[0-9]{7}$", RegexOptions.Compiled)]
    private static partial Regex GetPhoneRegex();
}