using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Attributes
{
    public class MinAttribute : ValidationAttribute
    {
        private int _min;
        public MinAttribute(int min)
        {
            _min = min;
        }

        public override bool IsValid(object value)
        {
            int? intValue = value as int?;
            if (intValue.HasValue)
                return IsValid(intValue.Value);
            return false;
        }

        public bool IsValid(int value)
        {
            return value >= _min;
        }
    }
}
