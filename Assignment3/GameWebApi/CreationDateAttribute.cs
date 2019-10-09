using GameWebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi
{
    public class CreationDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            //var itm = (Item)validationContext.ObjectInstance;
            var itm = validationContext.ObjectInstance;
            var x = ((DateTime)value);

            if (x > DateTime.Now)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Date is not in the past";
        }
    }
}
