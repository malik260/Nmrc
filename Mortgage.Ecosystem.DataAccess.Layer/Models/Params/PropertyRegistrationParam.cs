using Microsoft.AspNetCore.Http;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class PropertyRegistrationListParam
    {
        public long Id { get; set; }
        //public long PropertyType { get; set; }
        //public string? PropertyLocation { get; set; }
        //public string? PhoneNumber { get; set; }
        public long companyNumber { get; set; }
        public string? companyName { get; set; }
        public string? propertyType { get; set; }
        public string? propertyDescription { get; set; }
        public string? propertyLocation { get; set; }
        public string? phoneNumber { get; set; }
        public string? neighbourhood { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public string? email { get; set; }
        public List<string>? title { get; set; }
        public byte[] filedata { get; set; }

        public List<byte[]>? files { get; set; }



    }
}
