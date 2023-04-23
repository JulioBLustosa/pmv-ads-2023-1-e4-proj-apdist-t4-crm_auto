using CRMobil.Entities.Usuarios;

namespace CRMobil.Interfaces
{
    public interface IUserService :IServiceBase<User>
    {
        Task<string> CreateUser(User userModel);
        Task<List<User>> GetAsync();
    }
}
