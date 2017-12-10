using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Attributes
{
    public class IsbnAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string stringValue = value as string;
            if (String.IsNullOrWhiteSpace(stringValue))
                return false;
            //todo: ADD validation
            return true;
        }
    }
}
