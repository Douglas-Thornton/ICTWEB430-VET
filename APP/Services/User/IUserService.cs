using APP.Data.Models;

namespace APP.Interfaces;

public interface IUserService
{

    public Task<List<User>> GetAllUsersList(); 
    public Task<User> LoginUser(LoginRequest loginRequest);

    public Task<ServiceResponse> CreateUserAsync(User userToCreate);

    public Task<ServiceResponse> UpdateUserAsync(User userToCreate);
    //public Task<bool> ValidateAddress(User userToCreate);
}
