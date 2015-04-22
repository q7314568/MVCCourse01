using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MVC5Course01.Utils.Validation
{
    public class CheckCellPhoneAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Regex regex = new Regex(@"\d{4}-\d{6}");

            if (!regex.IsMatch(value.ToString()))
            {
                string sErrorMessage = "手機格式必須符合xxxx-xxxxxx的格式";
                return new ValidationResult(sErrorMessage);
            }
            else
            {
                return ValidationResult.Success;

            }

            //if (!(dtAge.Year >= 18 && dtAge.Year <= 30))
            //{ return new ValidationResult(sErrorMessage); }
        }
    }
}