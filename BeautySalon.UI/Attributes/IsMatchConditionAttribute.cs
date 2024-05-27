using System.ComponentModel.DataAnnotations;
using System.Reflection;
using CommunityToolkit.Diagnostics;

namespace BeautySalon.UI.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
internal sealed class IsMatchConditionAttribute(string conditionMethodName, string errorMessage) : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        MethodInfo method =
            validationContext.ObjectType.GetMethod(conditionMethodName, BindingFlags.Instance | BindingFlags.NonPublic)
            ?? ThrowHelper.ThrowArgumentNullException<MethodInfo>(nameof(conditionMethodName));
        
        return method.ReturnType == typeof(bool)
            ? CheckValidationMethod(method, validationContext)
            : ThrowHelper.ThrowArgumentException<ValidationResult>(
                $"{nameof(conditionMethodName)} without bool return type");
    }

    private ValidationResult CheckValidationMethod(MethodInfo method, ValidationContext context) =>
        ((bool)method.Invoke(context.ObjectInstance, null)!
            ? ValidationResult.Success
            : new ValidationResult(errorMessage))!;
}