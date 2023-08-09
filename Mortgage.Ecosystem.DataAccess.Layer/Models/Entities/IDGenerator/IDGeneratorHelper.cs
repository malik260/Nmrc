namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.IDGenerator
{
    // Generate database primary key Id
    public class IDGeneratorHelper
    {
        private Snowflake snowflake;

        private static readonly IDGeneratorHelper instance = new IDGeneratorHelper();

        private IDGeneratorHelper()
        {
            var snowFlakeWorkerId = GlobalContext.SystemConfig?.SnowFlakeWorkerId;
            snowflake = new Snowflake((long)snowFlakeWorkerId, 0, 0);
        }

        public static IDGeneratorHelper Instance
        {
            get
            {
                return instance;
            }
        }

        public long GetId()
        {
            return snowflake.NextId();
        }
    }
}