namespace RevenueRecognitionSystem.Validation;

using System;
using System.ComponentModel.DataAnnotations;

public class FutureDateRangeAttribute(int minDays, int maxDays) : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not DateTime dateTime) return new ValidationResult(GetErrorMessage());
        
        var minDate = DateTime.Now.AddDays(minDays);
        var maxDate = DateTime.Now.AddDays(maxDays);

        if (dateTime < minDate || dateTime > maxDate)
        {
            return new ValidationResult(GetErrorMessage());
        }
            
        return ValidationResult.Success;

    }

    private string GetErrorMessage()
    {
        return $"The date must be between {minDays} and {maxDays} days in the future.";
    }
}