namespace Mortgage.Ecosystem.BusinessLogic.Layer.Helpers
{
    internal class CacheKeys
    {
        public const string LocalName = "LocalName";
    }

    internal class CacheHelper
    {
        internal static readonly ThreadSafeMemoryCacheHelper<string> StringCache = new();
    }
}
