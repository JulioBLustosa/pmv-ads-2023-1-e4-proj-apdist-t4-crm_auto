using MongoDB.Bson.Serialization.Attributes;

namespace CRMobil.Entities.Veiculos
{
    public class Veiculos
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("id_veiculo")]
        public string IdVeiculo { get; set; }
    }
}
