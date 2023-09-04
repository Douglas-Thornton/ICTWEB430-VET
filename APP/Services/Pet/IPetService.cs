using APP.Data.Models;

namespace APP.Interfaces
{
    public interface IPetService
    {
        public Task<List<Pet>> GetAllPets();
    }
}
