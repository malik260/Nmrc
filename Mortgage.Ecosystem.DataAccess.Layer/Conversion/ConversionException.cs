namespace Mortgage.Ecosystem.DataAccess.Layer.Conversion
{
    // Data conversion extension package
    public static partial class ConversionException
    {
        public static Exception GetOriginalException(this Exception ex)
        {
            if (ex.InnerException == null) return ex;

            return ex.InnerException.GetOriginalException();
        }
    }
}