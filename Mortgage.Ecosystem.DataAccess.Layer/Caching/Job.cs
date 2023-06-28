using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;

namespace Mortgage.Ecosystem.DataAccess.Layer.Caching
{
    public class Job
    {
        #region Property
        private readonly IUnitOfWork _iUnitOfWork;
        #endregion

        #region Constructor
        public Job(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        //public Job(UnitOfWork iUnitOfWork, object iUnitOfWork1)
        //{
        //    _iUnitOfWork = iUnitOfWork;
        //}
        #endregion

        #region Job Scheduler
        public async Task<bool> JobScheduler()
        {
            //await _iUnitOfWork.DayBooks.getAllDayBooks(false);
            //await _iUnitOfWork.TransDetailAccs.getAllTransDetailAccs(false);
            //await _iUnitOfWork.TransMasterAccs.getAllTransMasterAccs(false);
            return true;
        }
        #endregion
    }
}
