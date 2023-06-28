namespace Mortgage.Ecosystem.BusinessLogic.Layer.Helpers
{
    internal class ProcessTimerHelper : IDisposable
    {
        private readonly string _name;
        private readonly DateTime _start;

        public ProcessTimerHelper(string name)
        {
            _name = name;
            _start = DateTime.Now;
            Console.WriteLine($"Process '{_name}' started.");
        }

        public void Dispose()
        {
            var end = DateTime.Now;
            Console.WriteLine($"Process '{_name}' ended. Time taken: {(end - _start).TotalMilliseconds}ms");
        }
    }
}
