using CRMobil.Entities;
using CRMobil.Entities.Oficina;
using CRMobil.Entities.ServicosOficina;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CRMobil.Services
{
    public class OficinasServices
    {
        private readonly IMongoCollection<Oficinas> _serviceCollection;

        public OficinasServices(IOptions<CnnStoreDatabaseSettings> serviceCollection)
        {

            var mongoCollection = new MongoClient(serviceCollection.Value.ConnectionString);

            var mongoDatabase = mongoCollection.GetDatabase(serviceCollection.Value.DatabaseName);

            _serviceCollection = mongoDatabase.GetCollection<Oficinas>("Oficinas");
        }

        public async Task<List<Oficinas>> GetAsync() => await _serviceCollection.Find(_ => true).ToListAsync();

        public async Task<Oficinas?> GetAsync(string id) => await _serviceCollection.Find(x => x.Id_Oficina == id).FirstOrDefaultAsync();

        public async Task<Oficinas?> GetCpfCnpjAsync(string descricao) => await _serviceCollection.Find(x => x.Nome_Oficina == descricao).FirstOrDefaultAsync();

        public async Task CreateAsync(Oficinas createModel) => await _serviceCollection.InsertOneAsync(createModel);

        public async Task UpdateAsync(string id, Oficinas updateModel) => await _serviceCollection.ReplaceOneAsync(x => x.Id_Oficina == id, updateModel);

        public async Task RemoveAsync(string id) => await _serviceCollection.DeleteOneAsync(x => x.Id_Oficina == id);
    }
}
