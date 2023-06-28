using System.Security.Cryptography;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Helpers
{
    internal class MathHelper
    {
        public static int RandomIntGenerator(int start, int end)
        {
            var generated = 0;
            if (start < end)
            {
                generated = RandomNumberGenerator.GetInt32(start, end);
            }
            return generated;
        }

        public static long RandomLongGenerator(long start, long end)
        {
            var generated = 0L;
            if (start < end)
            {
                generated = new Random().NextInt64(start, end);
            }
            return generated;
        }

        public static double Percent(double amount, double total, int decimalPlaces)
        {
            return Math.Round((amount / total) * 100, decimalPlaces);
        }

        public static double Sum(params double[] values)
        {
            return values.Sum();
        }
    }
}
