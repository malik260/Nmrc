namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    // Time tool
    public class DateTimeHelper
    {
        #region Milliseconds, days, minutes, seconds
        // Milliseconds turn days, hours, minutes and seconds
        // <param name="ms"></param>
        // <returns></returns>
        public static string FormatTime(long ms)
        {
            int ss = 1000;
            int mi = ss * 60;
            int hh = mi * 60;
            int dd = hh * 24;

            long day = ms / dd;
            long hour = (ms - day * dd) / hh;
            long minute = (ms - day * dd - hour * hh) / mi;
            long second = (ms - day * dd - hour * hh - minute * mi) / ss;
            long milliSecond = ms - day * dd - hour * hh - minute * mi - second * ss;

            string sDay = day < 10 ? "0" + day : "" + day; //day
            string sHour = hour < 10 ? "0" + hour : "" + hour;//hour
            string sMinute = minute < 10 ? "0" + minute : "" + minute;//minute
            string sSecond = second < 10 ? "0" + second : "" + second;//second
            string sMilliSecond = milliSecond < 10 ? "0" + milliSecond : "" + milliSecond;//milliseconds
            sMilliSecond = milliSecond < 100 ? "0" + sMilliSecond : "" + sMilliSecond;

            return string.Format("{0} days {1} hours {2} minutes {3} seconds", sDay, sHour, sMinute, sSecond);
        }
        #endregion

        #region get unix timestamp
        // Get the unix timestamp
        // <param name="dt"></param>
        // <returns></returns>
        public static long GetUnixTimeStamp(DateTime dt)
        {
            long unixTime = ((DateTimeOffset)dt).ToUnixTimeMilliseconds();
            return unixTime;
        }
        #endregion

        #region Get the minimum time of the date day
        public static DateTime GetDayMinDate(DateTime dt)
        {
            DateTime min = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
            return min;
        }
        #endregion

        #region Get the maximum time of the date day
        public static DateTime GetDayMaxDate(DateTime dt)
        {
            DateTime max = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
            return max;
        }
        #endregion

        #region Get the maximum time of the date day
        public static string FormatDateTime(DateTime? dt)
        {
            if (dt != null)
            {
                if (dt.Value.Year == DateTime.Now.Year)
                {
                    return dt.Value.ToString("MM-dd HH:mm");
                }
                else
                {
                    return dt.Value.ToString("yyyy-MM-dd HH:mm");
                }
            }
            return string.Empty;
        }
        #endregion
    }
}