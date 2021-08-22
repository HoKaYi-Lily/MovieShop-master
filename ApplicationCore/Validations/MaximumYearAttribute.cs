using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Validations
{
    public class MaximumYearAttribute : ValidationAttribute
    {
        public int Year { get; }
        public MaximumYearAttribute(int year)
        {
            //the one we want the year to be max
            Year = year;
        }

        //overrude IsValid
        //public override bool IsValid(object value)
        //{
        //    return base.IsValid(value);
        //}
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            //object? value is the one user entered
            var year = ((DateTime?)value)?.Year;
            if (year < Year)
            {
                return new ValidationResult($"Year should not be less than {Year}");
            }
            return ValidationResult.Success;
        }
    }

}

//inherit from validation and override some rules
