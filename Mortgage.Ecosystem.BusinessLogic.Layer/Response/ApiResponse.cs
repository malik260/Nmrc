using System.Text.Json;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Response
{
    public class ApiResponse
    {
        public string? Message { get; set; }
        public object? Response { get; set; }
        public int Status { get; set; }
        public bool Boolean { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);

        private void setMessageAndStatus(string message, int status)
        {
            Message = message;
            Status = status;
        }

        private void setMessageAndState(string message, bool state)
        {
            Message = message;
            Boolean = state;
        }

        public void setError(string message, int status)
        {
            setMessageAndStatus(message, status);
            Response = null;
        }

        public void setMessage(string message, int status)
        {
            setMessageAndStatus(message, status);
            Response = null;
        }

        public void setMessage(string message, bool state)
        {
            setMessageAndState(message, state);
            Response = null;
        }

        public void setMessage(string message, int status, string response)
        {
            setMessageAndStatus(message, status);
            Response = response;
        }

        public void setMessage(object relay, string message, int status)
        {
            setMessageAndStatus(message, status);
            Response = relay;
        }

        public void loggedMessage(string token, string message, int status)
        {
            setMessageAndStatus(message, status);
            Response = token;
        }
    }
}