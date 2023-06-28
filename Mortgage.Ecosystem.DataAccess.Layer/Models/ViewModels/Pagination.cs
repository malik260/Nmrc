namespace Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels
{
    // Pagination parameters
    public class Pagination
    {
        public Pagination()
        {
            Sort = "Id"; // Sort by Id by default
            SortType = " desc ";
            PageIndex = 1;
            PageSize = 10;
        }

        // Lines per page
        public int PageSize { get; set; }

        // current page
        public int PageIndex { get; set; }

        // Sort column
        public string Sort { get; set; }

        // Sort type
        public string SortType { get; set; }

        // Total
        public int TotalCount { get; set; }

        // Total pages
        public int TotalPage
        {
            get
            {
                if (TotalCount > 0)
                {
                    return TotalCount % this.PageSize == 0 ? TotalCount / this.PageSize : TotalCount / this.PageSize + 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
