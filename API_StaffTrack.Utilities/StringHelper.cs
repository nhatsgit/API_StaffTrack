﻿using System.Text;
using System.Text.RegularExpressions;

namespace API_StaffTrack.Utilities
{
    public static class StringHelper
    {
        public static string ToUnsignString(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            input = input.Replace(".", "");
            input = input.Replace(" ", "_");
            input = input.Replace(",", "");
            input = input.Replace(";", "");
            input = input.Replace(":", "");
            input = input.Replace("+", "");
            input = input.Replace("  ", "_");
            input = input.Replace("____", "_");
            input = input.Replace("___", "_");
            input = input.Replace("__", "_");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD).ToLower();
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            while (str2.Contains("--"))
            {
                str2 = str2.Replace("--", "_").ToLower();
            }
            return str2;
        }
        public static string ToUrlSlug(string value)
        {
            //First to lower case 
            value = value.ToLowerInvariant();

            //Remove all accents
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);

            value = Encoding.ASCII.GetString(bytes);

            //Replace spaces 
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

            //Remove invalid chars 
            value = Regex.Replace(value, @"[^\w\s\p{Pd}]", "", RegexOptions.Compiled);

            //Trim dashes from end 
            value = value.Trim('-', '_');

            //Replace double occurences of - or \_ 
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return value;
        }
        public static string ToUrlClean(string s, string replaceCharacter = "_")
        {
            if (!string.IsNullOrEmpty(s))
                s = s.Trim();
            s = s.Replace(".", "");
            s = s.Replace(",", "");
            s = s.Replace(";", "");
            s = s.Replace(":", "");
            s = s.Replace("+", "");
            s = s.Replace(" ", replaceCharacter);
            s = s.Replace("  ", replaceCharacter);
            s = s.Replace("____", replaceCharacter);
            s = s.Replace("___", replaceCharacter);
            s = s.Replace("__", replaceCharacter);
            s = s.Replace("/", replaceCharacter);
            s = s.Replace("-", replaceCharacter);
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').Replace(" ", replaceCharacter).ToLower();
        }
        public static string ToTelephoneNumerSendSMS(string input)
        {
            input = input.Replace(" ", string.Empty);
            input = input.Replace(".", string.Empty);
            input = input.Replace("+84", "0");
            input = input.Replace("(+84)", "0");
            return input;
        }
    }
}
