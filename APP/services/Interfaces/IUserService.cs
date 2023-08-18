using static APP.Data.Models.Models;

namespace APP.Data.Services;

public interface IUserService
{

    public Task<List<User>> GetAllUsersList(); 
    public Task<User> LoginUser(LoginRequest loginRequest);
}
