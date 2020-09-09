using System;
using System.ComponentModel.DataAnnotations;

namespace SoccerStatistics.Api.Database.Validations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PositionInFormationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext) => value switch
            {
                uint position when position < 1 => new ValidationResult("Position number cannot be less than 1."),
                uint position when position > 10 => new ValidationResult("Position number cannot be greater than 10."),
                _ => ValidationResult.Success
            };
    }
}
