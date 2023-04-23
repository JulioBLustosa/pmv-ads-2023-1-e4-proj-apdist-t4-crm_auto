using CRMobil.Entities.Oficina;
using CRMobil.Entities.Veiculos;

namespace CRMobil.Interfaces
{
    public interface IVeiculosServices //: IServiceBase<Veiculos>
    {
        Task<List<Veiculos>> GetAsync();

        Task<Veiculos?> GetAsync(string id);

        Task<Veiculos?> GetCpfCnpjAsync(string descricao);

        Task CreateAsync(Veiculos createModel);

        Task UpdateAsync(string id, Veiculos updateModel);

        Task RemoveAsync(string id);
    }
}
