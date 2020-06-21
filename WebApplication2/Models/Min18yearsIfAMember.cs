using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Min18yearsIfAMember: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var cutomer = (Customer)validationContext.ObjectInstance;
            if (cutomer.MemberShipTypeId==MemberShipType.Unknown || cutomer.MemberShipTypeId == MemberShipType.PayAsYouGo)
                return ValidationResult.Success;

            if (cutomer.BirthDate == null)
                return new ValidationResult("Birthdate is required");
            var age = DateTime.Today.Year - cutomer.BirthDate.Value.Year;
            return (age>=18) ? ValidationResult.Success : new ValidationResult("customer should be at least 18 years old.");
        }
    }
}