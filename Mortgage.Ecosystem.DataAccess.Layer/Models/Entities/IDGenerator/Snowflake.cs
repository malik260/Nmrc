using Mortgage.Ecosystem.DataAccess.Layer.Helpers;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.IDGenerator
{
    // Snowflake generation
    public class Snowflake
    {
        private const long TwEpoch = 1546272000000L;//2019-01-01 00:00:00

        private const int WorkerIdBits = 5;
        private const int DatacenterIdBits = 5;
        private const int SequenceBits = 12;
        private const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);
        private const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);

        private const int WorkerIdShift = SequenceBits;
        private const int DatacenterIdShift = SequenceBits + WorkerIdBits;
        private const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);

        private long _sequence = 0L;
        private long _lastTimestamp = -1L;

        //The high-order bits of the 10-bit data machine bits
        public long WorkerId { get; protected set; }

        // The low-order bits of the 10-bit data machine bits
        public long DatacenterId { get; protected set; }

        private readonly object _lock = new object();

        // Twitter-based snowflake algorithm
        // <param name="workerId">The high-order bits of the 10-bit data machine bits should not exceed 5 bits (5byte) by default</param>
        // <param name="datacenterId"> The low-order bits of the 10-bit data machine bits, the default should not exceed 5 bits (5byte)</param>
        // <param name="sequence">Initial sequence</param>
        public Snowflake(long workerId, long datacenterId, long sequence = 0L)
        {
            WorkerId = workerId;
            DatacenterId = datacenterId;
            _sequence = sequence;

            if (workerId > MaxWorkerId || workerId < 0)
            {
                throw new ArgumentException($"worker Id can't be greater than {MaxWorkerId} or less than 0");
            }

            if (datacenterId > MaxDatacenterId || datacenterId < 0)
            {
                throw new ArgumentException($"datacenter Id can't be greater than {MaxDatacenterId} or less than 0");
            }
        }

        public long CurrentId { get; private set; }

        // Get the next Id, this method is thread-safe
        // <returns></returns>
        public long NextId()
        {
            lock (_lock)
            {
                var timestamp = DateTimeHelper.GetUnixTimeStamp(DateTime.Now);
                if (timestamp < _lastTimestamp)
                {
                    //Can TODO consider waiting directly?
                    throw new Exception(
                        $"Clock moved backwards or wrapped around. Refusing to generate id for {_lastTimestamp - timestamp} ticks");
                }

                if (_lastTimestamp == timestamp)
                {
                    _sequence = (_sequence + 1) & SequenceMask;
                    if (_sequence == 0)
                    {
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }
                else
                {
                    _sequence = 0;
                }
                _lastTimestamp = timestamp;
                CurrentId = ((timestamp - TwEpoch) << TimestampLeftShift) |
                         (DatacenterId << DatacenterIdShift) |
                         (WorkerId << WorkerIdShift) | _sequence;

                return CurrentId;
            }
        }

        private long TilNextMillis(long lastTimestamp)
        {
            var timestamp = DateTimeHelper.GetUnixTimeStamp(DateTime.Now);
            while (timestamp <= lastTimestamp)
            {
                timestamp = DateTimeHelper.GetUnixTimeStamp(DateTime.Now);
            }
            return timestamp;
        }
    }
}