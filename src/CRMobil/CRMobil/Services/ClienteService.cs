using CRMobil.Entities;
using CRMobil.Entities.Cliente;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CRMobil.Services
{
    public class ClienteService
    {
        private readonly IMongoCollection<Clientes> _clienteServiceCollection;

        public ClienteService(IOptions<CnnStoreDatabaseSettings> serviceCollection)
        {
            var mongoCollection = new MongoClient(serviceCollection.Value.ConnectionString);

            var mongoDatabase = mongoCollection.GetDatabase(serviceCollection.Value.DatabaseName);

            _clienteServiceCollection = mongoDatabase.GetCollection<Clientes>(serviceCollection.Value.ClienteCollectionName);
        }

        public async Task<List<Clientes>> GetAsync() => await _clienteServiceCollection.Find(_ => true).ToListAsync();

        public async Task<Clientes?> GetAsync(string id) => await _clienteServiceCollection.Find(x => x.IdCliente == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Clientes newCliente) => await _clienteServiceCollection.InsertOneAsync(newCliente);

        public async Task UpdateAsync(string id, Clientes updateCliente) => await _clienteServiceCollection.ReplaceOneAsync(x => x.IdCliente == id, updateCliente);

        public async Task RemoveAsync(string id) => await _clienteServiceCollection.DeleteOneAsync(x => x.IdCliente == id);
    }
}
