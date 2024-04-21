using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BeautySalon.UI.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
internal partial class LatinOnlyAttribute(string errorMessage) : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return base.IsValid(value);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null || value is not string str)
        {
            return null;
        }

        if (GetLatinRegex().IsMatch(str))
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult(errorMessage);
        }
    }

    [GeneratedRegex(@"^[a-zA-Z_0-9\s]*$", RegexOptions.Compiled)]
    private static partial Regex GetLatinRegex();
}