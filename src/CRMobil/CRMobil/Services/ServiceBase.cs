using CRMobil.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CRMobil.Services
{
    public class ServiceBase<TEntity> : where TEntity : class
    {
        private readonly IMongoCollection<TEntity> _serviceCollection;

        public ServiceBase(IOptions<CnnStoreDatabaseSettings> serviceCollection)
        {
            var mongoCollection = new MongoClient(serviceCollection.Value.ConnectionString);

            var mongoDatabase = mongoCollection.GetDatabase(serviceCollection.Value.DatabaseName);

            _serviceCollection = mongoDatabase.GetCollection<TEntity>(serviceCollection.Value.ClienteCollectionName);
        }
    }
}
