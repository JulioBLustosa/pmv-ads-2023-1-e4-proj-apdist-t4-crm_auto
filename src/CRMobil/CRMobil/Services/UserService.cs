using CRMobil.Entities.Cliente;
using CRMobil.Entities;
using CRMobil.Entities.Usuarios;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using CRMobil.Interfaces;

namespace CRMobil.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _userServiceCollection;
        protected readonly ILogger _logger;

        public UserService(IOptions<CnnStoreDatabaseSettings> serviceCollection)
        {
            var mongoCollection = new MongoClient(serviceCollection.Value.ConnectionString);

            var mongoDatabase = mongoCollection.GetDatabase(serviceCollection.Value.DatabaseName);

            _userServiceCollection = mongoDatabase.GetCollection<User>("User");
        }

        public async Task<string> CreateUser(User userModel)
        {
            try
            {
                if (userModel is null)
                {
                    return null;
                }

                var hash = SecurePasswordHasher.Hash(userModel.Senha);

                userModel.Senha = hash;

                await _userServiceCollection.InsertOneAsync(userModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha: {nameof(UserService)}; {nameof(CreateUser)}; {ex.Message};");
            }

            return $"Usuario '{userModel.Nome_Usuario}' cadastrado com sucesso";
        }

        public async Task<List<User>> GetAsync()
        {
            return await _userServiceCollection.Find(_ => true).ToListAsync();
        }

        public async Task<User?> GetAsync(string userName, string password)
        {
            var usuario = new User();
            usuario = await _userServiceCollection.Find(x => x.Nome_Usuario == userName).FirstOrDefaultAsync();

            if (SecurePasswordHasher.Verify(password, usuario.Senha))
            {
                return usuario;
            }

            return null;
        }
            
        
    }
}
