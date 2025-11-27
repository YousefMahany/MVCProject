    using RouteG04.BLL.DTOS.User;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace RouteG04.BLL.Services.Interfaces
    {
        public interface IUserService
        {
            IEnumerable<UserDto> GetAllUsers(bool WithTracking);
            UserDetailsDto? GetUserById(string id);
            Task<bool> CreateUser(CreatedUserDto userDto);
            int UpdateUser(UpdatedUserDto userDto);
            bool DeleteUser(string id);
        }
    }
