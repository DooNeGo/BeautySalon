using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BeautySalon.UI.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
internal sealed partial class LatinOnlyAttribute(string errorMessage) : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var str = value?.ToString();
        return string.IsNullOrWhiteSpace(str) ? null :
                GetLatinRegex().IsMatch(str) ? ValidationResult.Success : new ValidationResult(errorMessage);
    }

    [GeneratedRegex(@"^[a-zA-Z_0-9]*$", RegexOptions.Compiled)]
    private static partial Regex GetLatinRegex();
}