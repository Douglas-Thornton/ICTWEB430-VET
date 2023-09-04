using APP.Data.Models;
using APP.Interfaces;

namespace APP.Services;

public class PetService : IPetService
{
    public Task<List<Pet>> GetAllPets()
    {
        throw new NotImplementedException();
    }
}
