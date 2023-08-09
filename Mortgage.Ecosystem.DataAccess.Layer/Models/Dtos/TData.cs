namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    // Data transfer object
    public class TData
    {
        // Operation result, Tag is 1 for success, 0 for failure, other verification returns results, which can be set as needed
        public int Tag { get; set; }

        // Prompt information or exception information
        public string? Message { get; set; }

        // Extend Message
        public string? Description { get; set; }
    }

    // Data transfer object (DTO)
    public class TData<T> : TData
    {
        // The number of records in the list
        public int Total { get; set; }

        // data
        public T? Data { get; set; }
    }
}