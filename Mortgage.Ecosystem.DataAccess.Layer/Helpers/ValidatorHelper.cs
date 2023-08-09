using System.Text.RegularExpressions;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    public static class ValidatorHelper
    {
        #region Verify that the input string is a number (with decimals)
        // Verify that the input string is a positive number with a decimal point
        // <param name="str">Enter character</param>
        // <returns>Returns a value of type bool</returns>
        public static bool IsDecimalNumber(this string str)
        {
            return Regex.IsMatch(str, "^([0]|([1-9]+\\d{0,}?))(.[\\d]+)?$");
        }

        // Verify that the input string is a positive or negative number with a decimal point
        // <param name="str">Enter character</param>
        // <returns>Returns a value of type bool</returns>
        public static bool IsNumberic(this string str)
        {
            return Regex.IsMatch(str, "^-?\\d+$|^(-?\\d+)(\\.\\d+)?$");
        }
        #endregion

        #region Verify that the phone format is valid, the format is 010-85849685
        // Verify that the phone format is valid, the format is 010-85849685
        // <param name="str">Enter character</param>
        // <returns>Returns a value of type bool</returns>
        public static bool IsTelephone(this string str)
        {
            return Regex.IsMatch(str, @"^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0- 9]{1,4})?$", RegexOptions.IgnoreCase);
        }
        #endregion

        #region Verify that the input string is a phone number
        // Verify that the input string is a phone number
        // <param name="str">Enter character</param>
        // <returns>Returns a value of type bool</returns>
        public static bool IsPhone(this string str)
        {
            return Regex.IsMatch(str, @"(^(\d{2,4}[-_－—]?)?\d{3,8}([-_－—]?\d{3,8} )?([-_－—]?\d{1,7})?$)|(^0?1[35]\d{9}$)");
            //Weaker validation: @"\d{3,4}-\d{7,8}"
        }
        #endregion

        #region Verify that it is a valid fax number
        // Verify that it is a valid fax number
        // <param name="str">Enter character</param>
        // <returns>Returns a value of type bool</returns>
        public static bool IsFax(this string str)
        {
            return Regex.IsMatch(str, @"^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12}) +$");
        }
        #endregion

        #region Verify that the phone number is legal
        // Verify that the mobile phone number is legal. The number segment is 13, 14, 15, 16, 17, 18, 19 0, and the beginning of 86 will be automatically recognized
        // <param name="str">Enter character</param>
        // <returns>Returns a value of type bool</returns>
        public static bool IsMobile(this string str)
        {
            if (!str.StartsWith("1"))
            {
                str = str.TrimStart(new char[] { '8', '6', }).TrimStart('0');
            }
            return Regex.IsMatch(str, @"^(13|14|15|16|17|18|19)\d{9}$");
        }
        #endregion

        #region Verify that the ID is valid
        // Verify that the ID is valid
        // <param name="str">Enter character</param>
        // <returns>Returns a value of type bool</returns>
        public static bool IsIDCard(this string str)
        {
            switch (str.Length)
            {
                case 18:
                    {
                        return str.IsIDCard18();
                    }
                case 15:
                    {
                        return str.IsIDCard15();
                    }
                default:
                    return false;
            }
        }

        // Verify that the input string is an 18-digit ID number
        // <param name="str">Enter character</param>
        // <returns>Returns a value of type bool</returns>
        public static bool IsIDCard18(this string str)
        {
            long n = 0;
            if (long.TryParse(str.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(str.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//Digital verification
            }
            const string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(str.Remove(2), StringComparison.Ordinal) == -1)
            {
                return false;//province verification
            }
            string birth = str.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time;
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//Birthday verification
            }
            string[] arrVariifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] ai = str.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(wi[i]) * int.Parse(ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            return arrVariifyCode[y] == str.Substring(17, 1).ToLower();
        }

        // Verify that the input string is a 15-digit ID number
        // <param name="str">Enter character</param>
        // <returns>Returns a value of type bool</returns>
        public static bool IsIDCard15(this string str)
        {
            long n = 0;
            if (long.TryParse(str, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//Digital verification
            }
            const string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(str.Remove(2), StringComparison.Ordinal) == -1)
            {
                return false;//province verification
            }
            string birth = str.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time;
            return DateTime.TryParse(birth, out time) != false;
        }
        #endregion

        #region Verify if it is a valid email address
        // Verify if it is a valid email address
        // <param name="str">Enter character</param>
        // <returns>Returns a value of type bool</returns>
        public static bool IsEmail(this string str)
        {
            return Regex.IsMatch(str, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[ 0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3 })(\]?)$");
        }
        #endregion

        #region Whether there are extra characters to prevent SQL injection
        // Whether there are extra characters to prevent SQL injection
        // <param name="str">Enter character</param>
        // <returns></returns>
        public static bool IsBadString(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            //List some special strings
            const string badChars = "@,*,#,$,!,+,',=,--,%,^,&,?,(,), <,>,[,],{,},/, \\,;,:,\",\"\",delete,update,drop,alert,select";
            var arraryBadChar = badChars.Split(',');
            return arraryBadChar.Any(t => !str.Contains(t));
        }
        #endregion

        #region is a string consisting of numbers, 26 English letters or underscores
        // Whether the string consists of numbers, 26 English letters or underscores
        // <param name="str">Enter character</param>
        // <returns></returns>
        public static bool IsAlphaNumericWithUnderscore(this string str)
        {
            return Regex.Match(str, "^[0-9a-zA-Z_]+$").Success;
        }
        #endregion

        #region A string consisting of numbers and 26 English letters
        // Whether it is a string composed of numbers and 26 English letters
        // <param name="str">Enter character</param>
        // <returns></returns>
        public static bool IsAlphaNumeric(this string str)
        {
            return Regex.Match(str, @"^[0-9a-zA-Z]+$").Success;
        }
        #endregion

        #region Verify that the input string is a zip code
        // Verify that the input string is a zip code
        // <param name="str">Enter character</param>
        // <returns>Returns a value of type bool</returns>
        public static bool IsPostCode(this string str)
        {
            return Regex.IsMatch(str, @"\d{6}");
        }
        #endregion

        #region check the input length of the object
        // Check the input length of the object
        // <param name="str">Enter character</param>
        // <param name="length">Specified length</param>
        // <returns>false too long, true - too short</returns>
        public static bool CheckLength(this string str, int length)
        {
            if (str.Length > length)
            {
                return false;//The length is too long
            }
            return str.Length >= length;
        }

        #endregion

        #region Determine if user input is a date
        // Determine if the user input is a date
        // <param name="str">Enter character</param>
        // <returns>Returns a value of type bool</returns>
        // <remarks>
        // The format can be judged as follows (where - can be replaced with /, which does not affect the verification)
        // YYYY | YYYY-MM | YYYY-MM-DD | YYYY-MM-DD HH:MM:SS | YYYY-MM-DD HH:MM:SS.FFF
        // </remarks>
        public static bool IsDateTime(this string str)
        {
            if (null == str)
            {
                return false;
            }
            const string regexDate = @"[1-2]{1}[0-9]{3}((-|\/|\.){1}(([0]?[1-9]{1}) |(1[0-2]{1}))((-|\/|\.){1}((([0]?[1-9]{1})|([1-2]{ 1}[0-9]{1})|(3[0-1]{1})))( (([0-1]{1}[0-9]{1})|2[0- 3]{1}):([0-5]{1}[0-9]{1}):([0-5]{1}[0-9]{1})(\.[0- 9]{3})?)?)?)?$";
            if (Regex.IsMatch(str, regexDate))
            {
                //Verify the dates of the following months to ensure the integrity of the verification
                int indexY = -1;
                int indexM = -1;
                int indexD = -1;
                if (-1 != (indexY = str.IndexOf("-", StringComparison.Ordinal)))
                {
                    indexM = str.IndexOf("-", indexY + 1, StringComparison.Ordinal);
                    indexD = str.IndexOf(":", StringComparison.Ordinal);
                }
                else
                {
                    indexY = str.IndexOf("/", StringComparison.Ordinal);
                    indexM = str.IndexOf("/", indexY + 1, StringComparison.Ordinal);
                    indexD = str.IndexOf(":", StringComparison.Ordinal);
                }
                // Does not include the date part, return true directly
                if (-1 == indexM)
                    return true;
                if (-1 == indexD)
                {
                    indexD = str.Length + 3;
                }
                int iYear = Convert.ToInt32(str.Substring(0, indexY));
                int iMonth = Convert.ToInt32(str.Substring(indexY + 1, indexM - indexY - 1));
                int iDate = Convert.ToInt32(str.Substring(indexM + 1, indexD - indexM - 4));
                //Determine the month and date
                if ((iMonth < 8 && 1 == iMonth % 2) || (iMonth > 8 && 0 == iMonth % 2))
                {
                    if (iDate < 32)
                        return true;
                }
                else
                {
                    if (iMonth != 2)
                    {
                        if (iDate < 31)
                            return true;
                    }
                    else
                    {
                        //leap year
                        if ((0 == iYear % 400) || (0 == iYear % 4 && 0 < iYear % 100))
                        {
                            if (iDate < 30)
                                return true;
                        }
                        else
                        {
                            if (iDate < 29)
                                return true;
                        }
                    }
                }
            }
            return false;
        }
        #endregion
    }
}