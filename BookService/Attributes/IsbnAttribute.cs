﻿using System;
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
            return IsValidIsbn10(stringValue) || IsValidIsbn13(stringValue);
        }

        private static bool IsValidIsbn10(string isbn10)
        {
            if (string.IsNullOrEmpty(isbn10))
            {
                return false;
            }

            if (isbn10.Contains("-"))
            {
                isbn10 = isbn10.Replace("-", "");
            }

            if (isbn10.Length != 10)
            {
                return false;
            }

            long temp;
            if (!long.TryParse(isbn10.Substring(0, isbn10.Length - 1), out temp))
            {
                return false;
            }

            var sum = 0;
            for (var i = 0; i < 9; i++)
            {
                sum += (isbn10[i] - '0') * (i + 1);
            }

            var result = false;
            var remainder = sum % 11;
            var lastChar = isbn10[isbn10.Length - 1];

            if (lastChar == 'X')
            {
                result = (remainder == 10);
            }
            else if (int.TryParse(lastChar.ToString(), out sum))
            {
                result = (remainder == lastChar - '0');
            }

            return result;
        }

        /// <summary>
        /// Validate ISBN13
        /// </summary>
        /// <param name="isbn13"></param>
        /// <returns></returns>
        private static bool IsValidIsbn13(string isbn13)
        {
            if (string.IsNullOrEmpty(isbn13))
            {
                return false;
            }

            if (isbn13.Contains("-"))
            {
                isbn13 = isbn13.Replace("-", "");
            }

            if (isbn13.Length != 13)
            {
                return false;
            }

            long temp;
            if (!long.TryParse(isbn13, out temp))
            {
                return false;
            }

            var sum = 0;
            for (var i = 0; i < 12; i++)
            {
                sum += (isbn13[i] - '0') * (i % 2 == 1 ? 3 : 1);
            }

            var remainder = sum % 10;
            var checkDigit = 10 - remainder;
            if (checkDigit == 10)
            {
                checkDigit = 0;
            }
            var result = (checkDigit == isbn13[12] - '0');
            return result;
        }
    }
}
