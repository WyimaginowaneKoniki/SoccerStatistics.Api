using System;
using System.ComponentModel.DataAnnotations;

namespace SoccerStatistics.Api.Database.Validations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null || value.ToString() == "")
                return new ValidationResult("Formation cannot be empty.");

            var formation = value.ToString().Split("-");

            if (formation.Length < 2)
                return new ValidationResult("Formation should have at least 3 'lines' divided by '-'.");

            var sumOfPlayers = 0u;

            foreach (var line in formation)
            {
                if (uint.TryParse(line, out uint numberOfPlayersInLine))
                {
                    if (numberOfPlayersInLine < 1 || numberOfPlayersInLine > 8)
                        return new ValidationResult("Number of players in one line should be between 1 and 8.");

                    sumOfPlayers += numberOfPlayersInLine;
                }
                else
                    return new ValidationResult("Formation should contain only '-' and digits.");

            }

            if (sumOfPlayers != 10)
                return new ValidationResult("Sum of players in formation should be equal to 10.");

            return ValidationResult.Success;
        }
    }
}
