namespace Mortgage.Ecosystem.DataAccess.Layer.Caching
{
    public class CacheConfiguration
    {
        public int AbsoluteExpirationInHours { get; set; }
        public int AbsoluteExpirationInMinutes { get; set; }
        public int AbsoluteExpirationInSeconds { get; set; }
        public int SlidingExpirationInMinutes { get; set; }
        public int SlidingExpirationInSeconds { get; set; }
    }
}
