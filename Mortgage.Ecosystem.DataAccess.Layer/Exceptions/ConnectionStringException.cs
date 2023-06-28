namespace Mortgage.Ecosystem.DataAccess.Layer.Exceptions
{
    public class ConnectionStringException : ConfigurationException
    {
        public ConnectionStringException(string message) : base(message)
        {
        }
    }
}
