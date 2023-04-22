﻿using Contracts.Exceptions;
using Contracts.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Utils
{
    public static class CommonHelper
    {
        public static string GenereateRandonAlphaNumeric()
        {
            return $"{Guid.NewGuid().ToString().Remove(5).ToUpper()}-{Guid.NewGuid().ToString().Remove(5).ToUpper()}";
        }

        public static bool IsValidEmail(this string email)
        {
            var e = new EmailAddressAttribute();
            return (!string.IsNullOrWhiteSpace(email) && e.IsValid(email));
        }

        public static string ToNigeriaMobile(this string str)
        {
            const string naijaPrefix = "234";
            if (string.IsNullOrEmpty(str))
                return str;

            str = str.TrimStart('+');
            var prefix = str.Remove(3);

            if (prefix.Equals(naijaPrefix))
            {
                return str;
            }
            str = str.TrimStart('0');
            str = naijaPrefix + str;
            return str;
        }

        public static string RandomNumber(int length)
        {
            var rand = new Random(0);

            var otRand = string.Empty;

            for (int i = 0; i < length; i++)
            {

                int temp = rand.Next(9);
                otRand += temp;
            }

            return otRand;
        }
        public static string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
}
