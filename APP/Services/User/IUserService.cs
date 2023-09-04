using APP.Data.Models;

namespace APP.Interfaces;

public interface IUserService
{

    public Task<List<User>> GetAllUsersList(); 
    public Task<User> LoginUser(LoginRequest loginRequest);
}
