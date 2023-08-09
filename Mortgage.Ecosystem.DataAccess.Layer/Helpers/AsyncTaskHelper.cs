namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    // Asynchronous thread tool
    public class AsyncTaskHelper
    {
        // Start an asynchronous task
        // <param name="action"></param>
        public static void StartTask(Action action)
        {
            try
            {
                Action newAction = () =>
                { };
                newAction += action;
                Task task = new Task(newAction);
                task.Start();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }
    }
}