using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    public class LoginDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? IsActive { get; set; }
        public string? Password { get; set; }
        public Guid RoleId { get; set; }
        public string? Username { get; set; }
        public int UserTypeId { get; set; }
        public bool Logged { get; set; }

        public UserEntity ToEntity()
        {
            return new()
            {
                UserName = Username,
                Password = Password
            };
        }

        public List<long> GetPmbMenus()
        {
            var param = new MenuListParam();
            var list = new List<long>();
            list.Add(563321487906312192);
            list.Add(563327185478225920);
            list.Add(563330762703638528);
            list.Add(563487335279235072);
            list.Add(563487650397294592);
            list.Add(563488060461813760);
            list.Add(663267838185705472);
            list.Add(660191053185290240);

            return list;

        }


        public List<long> GetEmployeeMenus()
        {
            var param = new MenuListParam();
            var list = new List<long>();
            list.Add(563321487906312192);
            list.Add(563322917484498944);
            list.Add(563323328681480192);
            list.Add(563323647595384832);
            list.Add(563324648524091392);
            list.Add(563325021112504320);
            list.Add(563325443080458240);
            list.Add(563488060461813760);
            list.Add(563330762703638528);

            return list;

        }


        public List<long> GetPmbEmployeeMenus()
        {
            var param = new MenuListParam();
            var list = new List<long>();
            list.Add(660881219264712704);
            list.Add(664553002530508800);
            list.Add(563325021112504320);
            list.Add(563327185478225920);
            list.Add(5898777445214440000);
            
            
            return list;

        }

         public List<long> GetNMRCEmployeeMenus()
        {
            var param = new MenuListParam();
            var list = new List<long>();
            list.Add(5877411145200001445);
            list.Add(5978441444236655780);
            list.Add(5987774554544522399);
            list.Add(5987447100236541478);
            
            
            return list;

        }





        public List<long> GetDeveloperMenus()
        {
            var param = new MenuListParam();
            var list = new List<long>();
            list.Add(563321487906312192);
            list.Add(563330762703638528);
            list.Add(563487335279235072);
            list.Add(563487650397294592);
            list.Add(563488060461813760);

            return list;

        }



      
    }

   
}