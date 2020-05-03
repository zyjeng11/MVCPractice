using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18Member : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;
            if(customer.MemberShipTypeId == 0 || customer.MemberShipTypeId == 1)            
                return ValidationResult.Success;
            if (customer.Birthday == null)
                return new ValidationResult("Birthdate is required");

            var age = DateTime.Today.Year - customer.Birthday.Year;

            return (age >= 18) ? ValidationResult.Success : new ValidationResult("未滿18歲不能訂閱");
        }
    }
}