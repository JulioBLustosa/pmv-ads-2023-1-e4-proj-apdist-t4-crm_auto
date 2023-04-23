using CRMobil.Entities;
using CRMobil.Entities.Cliente;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CRMobil.Services
{
    public class ClienteService
    {
        private readonly IMongoCollection<Cliente> _clienteServiceCollection;

        public ClienteService(IOptions<CnnStoreDatabaseSettings> serviceCollection)
        {
            var mongoCollection = new MongoClient(serviceCollection.Value.ConnectionString);

            var mongoDatabase = mongoCollection.GetDatabase(serviceCollection.Value.DatabaseName);

            _clienteServiceCollection = mongoDatabase.GetCollection<Cliente>("Cliente");
        }

        public async Task<List<Cliente>> GetAsync() => await _clienteServiceCollection.Find(_ => true).ToListAsync();

        public async Task<Cliente?> GetAsync(string id) => await _clienteServiceCollection.Find(x => x.Id_Cliente == id).FirstOrDefaultAsync();

        public async Task<Cliente?> GetCpfCnpjAsync(string documento) => await _clienteServiceCollection.Find(x => x.Cnpj_Cpf == documento).FirstOrDefaultAsync();

        public async Task CreateAsync(Cliente newCliente) => await _clienteServiceCollection.InsertOneAsync(newCliente);

        public async Task UpdateAsync(string id, Cliente updateCliente) => await _clienteServiceCollection.ReplaceOneAsync(x => x.Id_Cliente == id, updateCliente);

        public async Task RemoveAsync(string id) => await _clienteServiceCollection.DeleteOneAsync(x => x.Id_Cliente == id);
    }
}
