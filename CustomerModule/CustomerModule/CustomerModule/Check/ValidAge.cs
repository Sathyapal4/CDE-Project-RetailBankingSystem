using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerModule.Check
{
    public class ValidAge : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dob = DateTime.Parse(value.ToString());
            int age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.DayOfYear < dob.DayOfYear)
                age--;
            return age >= 18;
        }
    }
}
