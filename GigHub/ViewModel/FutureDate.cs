using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.ViewModel
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var IsValid = DateTime.TryParseExact(Convert.ToString(value), "d MM yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime dateTime);
            return (IsValid && dateTime > DateTime.Now);
            //return base.IsValid(value);
        }
    }
}