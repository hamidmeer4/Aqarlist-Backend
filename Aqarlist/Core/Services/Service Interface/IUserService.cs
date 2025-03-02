using Aqarlist.Core.Models.Database;
using Aqarlist.Core.Models.Dto;

namespace Aqarlist.Core.Services.Service_Interface
{
    public interface IUserService
    {
        public Users[] GetAll();
        public Guid Create(UserDto model);
        public LoginRepsonse Login(string emailAddress , string password);
        public Roles[] GetAllRoles();
    }
}
