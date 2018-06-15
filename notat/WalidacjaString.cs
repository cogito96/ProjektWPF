using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace notat
{

    class WalidacjaString : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || value == "")
                return new ValidationResult(false, "Pole musi być uzupełnione!");

            else
                return ValidationResult.ValidResult;
        }
    }
}