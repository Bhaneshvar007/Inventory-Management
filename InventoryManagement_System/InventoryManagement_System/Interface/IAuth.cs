using InventoryManagement_System.Models;

namespace InventoryManagement_System.Interface
{
    public interface IAuth
    {
        public Task<List<Auth>> GetAuthUserAsync();
        public Task<string>  RegistrationAsync(Auth auth);

    }
}
