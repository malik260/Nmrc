using System.Security.Cryptography;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    // Random Helper
    public class RandomHelper
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
    }
}