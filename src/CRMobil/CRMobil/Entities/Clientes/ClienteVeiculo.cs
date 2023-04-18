using MongoDB.Bson.Serialization.Attributes;

namespace CRMobil.Entities.Clientes
{
    public class ClienteVeiculo
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("id_cliente_veiculo")]
        public string IdClienteVeiculo { get; set; }
    }
}
