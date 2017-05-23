﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sculptor
{
    public class NewModelValidation : ValidationRule
    {
        double minValue;
        double maxValue;

        public double MinValue
        {
            get { return this.minValue; }
            set { this.minValue = value; }
        }

        public double MaxValue
        {
            get { return this.maxValue; }
            set { this.maxValue = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double margin;

            // Is a number?
            if (!double.TryParse((string)value, out margin))
            {
                return new ValidationResult(false, "Not a number.");
            }

            // Is in range?
            if ((margin < this.minValue) || (margin > this.maxValue))
            {
                string msg = string.Format("Margin must be between {0} and {1}.", this.minValue, this.maxValue);
                return new ValidationResult(false, msg);
            }

            // Number is valid
            return new ValidationResult(true, null);
        }
    }
}
